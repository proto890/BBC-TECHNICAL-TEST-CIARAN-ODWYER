using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is used to create a board to render the game of life on
public class CellBoard : MonoBehaviour {
    [Header("Game Objects")]
    public GameObject mainCamera;
    public GameObject cell;
    public GameObject[,] cellMatrix;

    [Header("Variable Declerations")]
    public int row;
    public int col;
    public float spawn;
    Transform gridHolder;
    CellBehaviour cellBe;

    // Use this for initialization
    void Awake () {
        cellMatrix = new GameObject[col, row];//Declares the grid of cells
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");//Declares what camera to use to display the scene
	}

    public void AdjustSpawn(float newSpawn)
    {
        spawn = newSpawn;
    }

    public void Restart()
    {
        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }
	
	//Function that is used to create the scene
	public void SetupScene () {
        gridHolder = new GameObject("Grid").transform;//Creates a grid to store and display all the cells

        mainCamera.transform.position = new Vector3(col / 2, row / 2, -10f);//Sets the position of the main camera to match the grid
        mainCamera.GetComponent<Camera>().orthographicSize = (col > row ? (col / 2 + 1) : (row / 2 + 1));//Sets the camera style to match the grid size

        Vector3 cellPos = new Vector3(0f, 0f, 0f);//Sets the position of the grid to the centre of the screen
        
        //Scrolls through each of the defined x and y values of the grid
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                cellPos.x = x;
                cellPos.y = y;
                GameObject cellInstant = Instantiate(cell, cellPos, Quaternion.identity) as GameObject;//Instantiates a cell for each position
                cellInstant.transform.SetParent(gridHolder);//Sets the position of each cell

                cellBe = cellInstant.GetComponent<CellBehaviour>();
                cellBe.isAlive = (Random.value <= spawn ? true : false);//Sets the spawn rate of cells

                cellBe.SetPosition(x, y);
                cellMatrix[x, y] = cellInstant;
            }
        }
               

	}
}
