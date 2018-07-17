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

    //Tile Destruction and Matching variables
    public bool cleanupTime;
    public Transform tileCurrentlySelected;

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
		if(cleanupTime && AllContainersHaveATile())
        {
            if (!CheckIfTilesMoving())
            {
                CleanupStep();
            }
            else
            {
                Debug.Log("AreTilesStillMoving");
            }
        }
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

    void CleanupStep()
    {
        cleanupTime = false;
        Debug.Log("Cleaned Up");
        for (int i = 0; i < gridStorageObjects.Count; i++)
        {
            foreach (Transform fooTrans in gridStorageObjects[i])
            {
                fooTrans.GetComponent<TileContainerScripts>().CheckIfMatch();
                if(fooTrans.GetComponent<TileContainerScripts>().isMatched)
                {
                    cleanupTime = true;
                }
            }
        }

        for (int i = 0; i < gridStorageObjects.Count; i++)
        {
            foreach (Transform fooTrans in gridStorageObjects[i])
            {
                fooTrans.GetComponent<TileContainerScripts>().PersonalCleanup();
            }
        }
    }

    bool CheckIfTilesMoving()
    {
        for (int x = 0; x < gridStorageObjects.Count; x++)
        {
            foreach (Transform fooTran in gridStorageObjects[x])
            {
                if(fooTran.GetComponent<TileContainerScripts>().tileContained.transform.position != fooTran.transform.position)
                {
                    Debug.Log(fooTran.name);
                    return true;
                }
            }
        }

        return false;
    }

    bool AllContainersHaveATile()
    {
        for (int x = 0; x < gridStorageObjects.Count; x++)
        {
            foreach (Transform fooTran in gridStorageObjects[x])
            {
                if (fooTran.GetComponent<TileContainerScripts>().tileContained == null)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
