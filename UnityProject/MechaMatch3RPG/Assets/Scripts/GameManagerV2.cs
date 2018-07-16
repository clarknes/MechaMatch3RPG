using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerV2 : MonoBehaviour {

    //Here is the grid lists
    public List<Transform> tiles;
    public List<List<Transform>> gridStorageObjects = new List<List<Transform>>();

    //Here are the grid variables
    public int amountOfColumns, amountOfRows;
    public Transform grid;

    public enum TileColors
    {
        Blue,
        Green,
        Purple,
        Red,
        Yellow
    }

    
	// Use this for initialization
	void Start () {
        InstantiateGrid();
        CreateBaseGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstantiateGrid()
    {
        for(int i = 0; i < amountOfColumns; i++)
        {
            gridStorageObjects.Add(new List<Transform>());
        }
    }

    void CreateBaseGrid()
    {
        for(int cols = 0; cols < amountOfRows; cols++)
        {
            for(int rows = 0; rows < amountOfColumns; rows++)
            {
                Transform temp = Instantiate(tiles[0], new Vector3(cols-4.5f, rows-3, 0), tiles[0].rotation);

                temp.gameObject.name = rows + "-" + cols;

                temp.parent = grid;
                temp.GetComponent<TileContainerScripts>().coordinates = new Vector2(cols, rows);

                gridStorageObjects[cols].Add(temp);
            }
        }
    }
}
