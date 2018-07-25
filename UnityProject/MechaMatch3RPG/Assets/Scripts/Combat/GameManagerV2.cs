using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public CanvasGroup cascadeScoreboard;
    public List<Text> cascadeList = new List<Text>();
    public List<Text> levelTotalList = new List<Text>();
    public List<int> cascadeScoreList = new List<int>();
    public List<int> levelScoreTotalList = new List<int>();

    public PlayerStats player1, player2;
    public bool player1Turn;
    public bool toggleStatOverlay;
    public CanvasGroup statOverlay;
    //public Vector3 offPosition;
    //public Vector3 onPosition;

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
            cascadeScoreboard.alpha = 1;
            if (!CheckIfTilesMoving())
            {
                CleanupStep();
            }
            else
            {
                Debug.Log("AreTilesStillMoving");
            }
        }
        else if(!cleanupTime)
        {
            for (int i = 0; i < cascadeScoreList.Count; i++)
            {
                cascadeScoreList[i] = 0;
            }
            cascadeScoreboard.alpha = 0;
            player1.UpdateCharge();
        }

        if(toggleStatOverlay)
        {
            //statOverlay.gameObject.transform.position = onPosition;
            //statOverlay.gameObject.SetActive(true);
            statOverlay.alpha = 1;
            player1Turn = false;
        }
        else
        {
            //statOverlay.gameObject.transform.position = offPosition;
            //statOverlay.gameObject.SetActive(false);
            statOverlay.alpha = 0;
            statOverlay.interactable = false;
            player1Turn = true;
        }

        UpdateScoreboard();
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
        //Change when enemies are in
        //player1Turn = true;
        Debug.Log("Cleaned Up");
        for (int i = 0; i < gridStorageObjects.Count; i++)
        {
            foreach (Transform fooTrans in gridStorageObjects[i])
            {
                fooTrans.GetComponent<TileContainerScripts>().CheckIfMatch();
                if(fooTrans.GetComponent<TileContainerScripts>().isMatched)
                {
                    //change when enemies are in
                    //player1Turn = false;
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

        if(!cleanupTime)
        {
            if(player2.shield > 0 && player2.shield <= player1.charge)
            {
                player1.charge -= player2.shield;
                player2.shield = 0;
                player2.health -= player1.charge;
                player1.charge = 0;
                player1.UpdateCharge();
            }
            else if(player2.shield > 0)
            {
                player2.shield -= player1.charge;
                player1.charge = 0;
                player1.UpdateCharge();
            }
            else
            {
                player2.health -= player1.charge;
                player1.charge = 0;
                player1.UpdateCharge();
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

    public void UpdateScoreboard()
    {
        for(int i = 0; i < levelTotalList.Count; i++)
        {
            levelTotalList[i].text = levelScoreTotalList[i].ToString();
            cascadeList[i].text = cascadeScoreList[i].ToString();
        }
    }

    public void ToggleOverlayButton()
    {
        if(toggleStatOverlay)
        {
            toggleStatOverlay = false;
        }
        else
        {
            toggleStatOverlay = true;
        }
    }
}
