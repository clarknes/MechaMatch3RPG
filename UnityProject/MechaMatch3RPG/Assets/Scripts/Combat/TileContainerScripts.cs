using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainerScripts : MonoBehaviour {

    //These variables help access the tile contained in this one.
    public Vector2 coordinates;
    public Transform tileContained;
    public GameManagerV2 gm;

    public List<Transform> tiles = new List<Transform>();
    public int horizontalMatch = 1, verticalMatch = 1;
    public bool isMatched;

    public bool shouldOutputDebug;

    bool temp = true;

    // Use this for initialization
    void Start () {

        gm = FindObjectOfType<GameManagerV2>();

        Transform temp = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(coordinates.x - 4.5f, coordinates.y - 3, 0), tiles[0].rotation);

        temp.parent = transform;

        tileContained = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(tileContained == null)
        {
            GrabFromAbove();
        }
	}

    void GrabFromAbove()
    {
        if (Mathf.RoundToInt(coordinates.y) + 1 < gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)].Count)
        {
            if (gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][Mathf.RoundToInt(coordinates.y) + 1] != null)
            {
                if (gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][Mathf.RoundToInt(coordinates.y) + 1].GetComponent<TileContainerScripts>().tileContained != null)
                {
                    tileContained = gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][Mathf.RoundToInt(coordinates.y) + 1].GetComponent<TileContainerScripts>().tileContained;
                    gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][Mathf.RoundToInt(coordinates.y) + 1].GetComponent<TileContainerScripts>().tileContained = null;
                    tileContained.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log(name);
            CreateANewTile();
        }
    }

    void CreateANewTile()
    {
        //gm = FindObjectOfType<GameManagerV2>();

        Transform temp = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(coordinates.x - 4.5f, coordinates.y - 3, 0), tiles[0].rotation);

        temp.parent = transform;

        tileContained = temp;
    }

    public void CheckIfMatch()
    {
        GameManagerV2.TileColors colorToMatch = tileContained.GetComponent<TileBaseScript>().currentColor;
        if (shouldOutputDebug)
        {
            Debug.Log(colorToMatch);
        }
        //Checks Left
        if(coordinates.x > 0)
        {
            for(int x = Mathf.RoundToInt(coordinates.x); x > 0; x--)
            {
                TileContainerScripts temp = gm.gridStorageObjects[x-1][Mathf.RoundToInt(coordinates.y)].GetComponent<TileContainerScripts>();
                if (temp.tileContained.GetComponent<TileBaseScript>().currentColor == colorToMatch)
                {
                    horizontalMatch++;
                }
                else
                {
                    break;
                }
            }
        }
        if(coordinates.x < gm.amountOfColumns-1)
        {
            for (int x = Mathf.RoundToInt(coordinates.x); x < gm.amountOfColumns-1; x++)
            {
                TileContainerScripts temp = gm.gridStorageObjects[x+1][Mathf.RoundToInt(coordinates.y)].GetComponent<TileContainerScripts>();
                if (temp.tileContained.GetComponent<TileBaseScript>().currentColor == colorToMatch)
                {
                    horizontalMatch++;
                }
                else
                {
                    break;
                }
            }
        }

        if (coordinates.y > 0)
        {
            for (int y = Mathf.RoundToInt(coordinates.y); y > 0; y--)
            {
                TileContainerScripts temp = gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][y-1].GetComponent<TileContainerScripts>();
                if (temp.tileContained.GetComponent<TileBaseScript>().currentColor == colorToMatch)
                {
                    verticalMatch++;
                }
                else
                {
                    break;
                }
            }
        }
        if (coordinates.y < gm.amountOfColumns-1)
        {
            for (int y = Mathf.RoundToInt(coordinates.y); y < gm.amountOfRows-1; y++)
            {
                TileContainerScripts temp = gm.gridStorageObjects[Mathf.RoundToInt(coordinates.x)][y+1].GetComponent<TileContainerScripts>();
                if (temp.tileContained.GetComponent<TileBaseScript>().currentColor == colorToMatch)
                {
                    verticalMatch++;
                }
                else
                {
                    break;
                }
            }
        }

        if(verticalMatch >= 3 || horizontalMatch >= 3)
        {
            if(verticalMatch >= 3 && horizontalMatch >= 3)
            {
                isMatched = true;
            }
            else
            {
                isMatched = true;
            }
        }

        verticalMatch = 1;
        horizontalMatch = 1;
    }

    public void PersonalCleanup()
    {
        if(isMatched)
        {
            if(tileContained.GetComponent<TileBaseScript>().currentColor == GameManagerV2.TileColors.Blue)
            {
                gm.levelScoreTotalList[0] += gm.player1.blueAttack;
                gm.cascadeScoreList[0] += gm.player1.blueAttack;
            }
            else if (tileContained.GetComponent<TileBaseScript>().currentColor == GameManagerV2.TileColors.Purple)
            {
                gm.levelScoreTotalList[1] += gm.player1.purpleAttack;
                gm.cascadeScoreList[1] += gm.player1.purpleAttack;
            }
            else if (tileContained.GetComponent<TileBaseScript>().currentColor == GameManagerV2.TileColors.Green)
            {
                gm.levelScoreTotalList[2] += gm.player1.greenAttack;
                gm.cascadeScoreList[2] += gm.player1.greenAttack;
            }
            else if (tileContained.GetComponent<TileBaseScript>().currentColor == GameManagerV2.TileColors.Yellow)
            {
                gm.levelScoreTotalList[3] += gm.player1.yellowAttack;
                gm.cascadeScoreList[3] += gm.player1.yellowAttack;
            }
            else if (tileContained.GetComponent<TileBaseScript>().currentColor == GameManagerV2.TileColors.Red)
            {
                gm.levelScoreTotalList[4] += gm.player1.redAttack;
                gm.cascadeScoreList[4] += gm.player1.redAttack;
            }

            gm.player1.UpdateCharge();

            Transform temp = tileContained;
            tileContained = null;
            isMatched = false;
            Destroy(temp.gameObject);
        }
    }
}
