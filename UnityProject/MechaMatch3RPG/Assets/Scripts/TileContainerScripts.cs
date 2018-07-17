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
            CreateANewTile();
        }
    }

    void CreateANewTile()
    {
        //gm = FindObjectOfType<GameManagerV2>();

        Transform temp = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(coordinates.x - 4.5f, coordinates.y - 3, 0), tiles[0].rotation);

        temp.parent = transform;

        tileContained = transform.GetChild(0);
    }

    public void CheckIfMatch()
    {
        GameManagerV2.TileColors colorToMatch = tileContained.GetComponent<TileBaseScript>().currentColor;

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
        if(coordinates.x < 8)
        {
            for (int x = Mathf.RoundToInt(coordinates.x); x < 8; x++)
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
        if (coordinates.y < 8)
        {
            for (int y = Mathf.RoundToInt(coordinates.y); y < 8; y++)
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
            Transform temp = tileContained;
            tileContained = null;
            isMatched = false;
            Destroy(temp.gameObject);
        }
    }
}
