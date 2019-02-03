using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is used to control the behaviour of the cells and how they react to each other
public class CellBehaviour : MonoBehaviour {

    [Header("Sprite Settings")]
    public bool isAlive;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    [Header("Script Declarations")]
    private CellBoard cellB;
    private GameObject[,] cellMatrix;
    private SpriteRenderer spriteRenderer;

    [Header("Private Variable Declerations")]
    private int aliveNeighbours;
    private int x;
    private int y;
    private bool isSpriteSet;


    //script used upon awakening 
    private void Awake()
    {
        cellB = GameManager.instance.GetComponent<CellBoard>();//Sets an instance of the cell board
        cellMatrix = cellB.cellMatrix;//Declares a cell matrix to establish a position for each individual cell
        spriteRenderer = GetComponent<SpriteRenderer>();//Sets a sprite renderer to create visuals
    }



    // Use this for initialization
    void Start () {
        isSpriteSet = false;//Sets the sprites to false to start so all cells start correctly
	}
	
	// Update is called once per frame
	void Update () {
        if (!isSpriteSet)
        {
            //Checks state of sprite in order to use the correct sprite
            if (isAlive)
            {
                spriteRenderer.sprite = aliveSprite;
            }
            else
            {
                spriteRenderer.sprite = deadSprite;
            }
            isSpriteSet = true;
        }
    }

    //THIS CONTAINS THE MAIN LOGIC OF THE CELLS
    public void Spread()
    {
        //Sets flags so if the cell is alive and has either less than 2 neighbours or more than 3 neighbours, the cell dies
        if (isAlive && (aliveNeighbours < 2 || aliveNeighbours > 3))
        {
            isAlive = false;
        }
        //Sets flags so if the cell is alive and has either 2 or 3 neighbours exactly, it will be alive
        else if (isAlive && (aliveNeighbours == 2 || aliveNeighbours == 3))
        {
            isAlive = true;
        }
        //Sets flag so if the cell is dead and has 3 neighbours, it will come back to life
        else if (!isAlive && aliveNeighbours == 3)
        {
            isAlive = true;
        }
        isSpriteSet = false;
    }

    //Function used to set the individual positions of each of the cells
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }


    //Function used to check the neighbouring cells
    public void CheckNeighbours()
    {
        //Sets the alive neighbours to 0 to start checking the neighbours
        aliveNeighbours = 0;
        //Checks through each row and column, incrementing the value
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int v = y - 1; v <= y + 1; v++)
            {
                if (i == x && v == y) continue;

                //Checks the value and if it matches the column and row, sets the alive flag and adds one to the alive neighbours value
                if (i >= 0 && i < cellB.col && v >= 0 && v < cellB.row)
                {
                    if (cellMatrix[i, v].GetComponent<CellBehaviour>().isAlive)
                    {
                        aliveNeighbours++;
                    }

                }
            }
        }
    }




}
