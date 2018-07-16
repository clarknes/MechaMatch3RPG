using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainerScripts : MonoBehaviour {

    //These variables help access the tile contained in this one.
    public Vector2 coordinates;
    public Transform tileContained;
    public GameManagerV2 gm;

    public List<Transform> tiles = new List<Transform>();

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
            Debug.Log("TopOfGrid");
        }
    }
}
