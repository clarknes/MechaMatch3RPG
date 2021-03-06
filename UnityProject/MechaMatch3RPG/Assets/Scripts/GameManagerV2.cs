﻿using System.Collections;
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
    public EventManager em;
    public bool cleanupTime;
    public Transform tileCurrentlySelected;
    public CanvasGroup cascadeScoreboard;
    public List<Text> cascadeList = new List<Text>();
    public List<Text> levelTotalList = new List<Text>();
    public List<int> cascadeScoreList = new List<int>();
    public List<int> levelScoreTotalList = new List<int>();

<<<<<<< HEAD:UnityProject/MechaMatch3RPG/Assets/Scripts/Combat/GameManagerV2.cs
    public PlayerStats player1, player2;
    public bool player1Turn;
    public bool toggleStatOverlay;
    public CanvasGroup statOverlay;
    //public Vector3 offPosition;
    //public Vector3 onPosition;

    //DeleteLater
    public EventParent attackEvent;

=======
>>>>>>> parent of 99c80b7... Players Combat loop is almost done and scene loading is implimented:UnityProject/MechaMatch3RPG/Assets/Scripts/GameManagerV2.cs
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
<<<<<<< HEAD:UnityProject/MechaMatch3RPG/Assets/Scripts/Combat/GameManagerV2.cs

        if(!cleanupTime)
        {
            Debug.Log("EventPhaseShouldHappen");
            attackEvent.AddEventToStack(em.attackPhase);
            em.ResolveAttackPhase();
        }
=======
>>>>>>> parent of 99c80b7... Players Combat loop is almost done and scene loading is implimented:UnityProject/MechaMatch3RPG/Assets/Scripts/GameManagerV2.cs
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
}
