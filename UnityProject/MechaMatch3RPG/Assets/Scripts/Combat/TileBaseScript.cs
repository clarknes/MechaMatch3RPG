using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBaseScript : MonoBehaviour {

    [SerializeField]
    float TileMoveSpeed = 5.0f;
    public TileContainerScripts currentStorageTile;
    public GameManagerV2.TileColors currentColor;
    public GameManagerV2 gm;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManagerV2>();
	}
	
	// Update is called once per frame
	void Update () {
        ResetPosition();
	}

    void ResetPosition()
    {
        currentStorageTile = transform.parent.GetComponent<TileContainerScripts>();
        transform.position = Vector3.Lerp(transform.position, currentStorageTile.transform.position, Time.deltaTime * TileMoveSpeed);
    }

    private void OnMouseDown()
    {
        if (gm.cleanupTime == false && gm.player1Turn)
        {
            if (currentStorageTile.gm.tileCurrentlySelected == null)
            {
                currentStorageTile.gm.tileCurrentlySelected = transform;
            }
            else
            {
                currentStorageTile.gm.tileCurrentlySelected = null;
            }
        }
    }

    private void OnMouseOver()
    {
        if (gm.cleanupTime == false && gm.player1Turn)
        {
            if (gm.tileCurrentlySelected != gameObject.transform && Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (gm.tileCurrentlySelected.GetComponent<TileBaseScript>().currentStorageTile.GetComponent<TileContainerScripts>() != null)
                {
                    if (IsTileAdjacent(gm.tileCurrentlySelected.GetComponent<TileBaseScript>().currentStorageTile.GetComponent<TileContainerScripts>()))
                    {
                        SwapTile(gm.tileCurrentlySelected.GetComponent<TileBaseScript>().currentStorageTile.GetComponent<TileContainerScripts>());
                        gm.tileCurrentlySelected = null;
                    }
                    else
                    {
                        gm.tileCurrentlySelected = null;
                    }
                }
                else
                {
                    gm.tileCurrentlySelected = null;
                }
            }
        }
    }

    public bool IsTileAdjacent(TileContainerScripts otherTile)
    {
        //CheckLeft
        if(otherTile.coordinates.x > -1 && otherTile.coordinates.x < gm.amountOfColumns && otherTile.coordinates.y < gm.amountOfRows && otherTile.coordinates.y > -1)
        {
            if(Mathf.Abs(otherTile.coordinates.x - currentStorageTile.coordinates.x) == 1)
            {
                return true;
            }
            else if(Mathf.Abs(otherTile.coordinates.y - currentStorageTile.coordinates.y) == 1)
            {
                return true;
            }

            Debug.Log(Mathf.Abs(otherTile.coordinates.x - currentStorageTile.coordinates.x));
            Debug.Log(otherTile.name + " " + currentStorageTile.name);
        }

        return false;
    }

    public void SwapTile(TileContainerScripts otherTile)
    {
        gm.tileCurrentlySelected.transform.parent = currentStorageTile.transform;
        gm.tileCurrentlySelected.GetComponent<TileBaseScript>().currentStorageTile = currentStorageTile.GetComponent<TileContainerScripts>();
        currentStorageTile.GetComponent<TileContainerScripts>().tileContained = gm.tileCurrentlySelected;

        transform.parent = otherTile.transform;
        currentStorageTile = otherTile;
        currentStorageTile.tileContained = transform;
        gm.cleanupTime = true;
        //gm.player1Turn = false;
    }
}
