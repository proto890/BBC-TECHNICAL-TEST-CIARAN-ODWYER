using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THIS SCRIPT IS USED TO START THE CELLS AND TO MESS WITH HOW THEY WORK
public class GameManager : MonoBehaviour {

    [Header("Variable Declerations")]
    public static GameManager instance = null;
    CellBoard cellB;//Declares the behaviour script for use
    float time;



    void Awake()
    {
        //Ensureses that the cell manager has started
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        cellB = GetComponent<CellBoard>();//Gets the cell board component 
    }

    // Use this for initialization
    void Start () {
        cellB.SetupScene();//Sets up the scene for use 
	}
	
	// Update is called once per frame
	void Update () {
        NextGeneration();
    }
    //Queues the next generation of the game
    void NextGeneration()
    {
        //Runs the check neighbour script on each individual row and column, incrementing the value to check it.
        for (int i = 0; i < cellB.col; i++)
        {
            for (int v = 0; v < cellB.row; v++)
            {
                cellB.cellMatrix[i, v].GetComponent<CellBehaviour>().CheckNeighbours();
            }
        }
        //Runs the spread script on each individual row and column, incrementing the value to check it.
        for (int i = 0; i < cellB.col; i++)
        {
            for (int v = 0; v < cellB.row; v++)
            {
                cellB.cellMatrix[i, v].GetComponent<CellBehaviour>().Spread();
            }
        }
    }
}
