using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Parent : MonoBehaviour {

    //This Enum will be used to define ALL tile color systems. All children tiles will share this enum
    public enum TileColor
    {
        Blue,
        Green,
        Purple,
        Red,
        Yellow
    }

    //Neccessary Variables to cover whether this tile should be destroyed
    public GameManager gm;
    public TileColor m_tileColor;
    public bool shouldBeDestroyed;
    public Vector2 gridCords;

    //Movement Variables
    public float tileMoveSpeed;
    public Vector3 newTargetLocation;

    //DebugTools
    public bool shouldOutputDebug;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        MoveTowardsNewLocation(newTargetLocation);
	}

    public void CheckIfShouldBeDestroyed()
    {
        if (shouldOutputDebug)
        {
            Debug.Log("Test point 1" + shouldBeDestroyed);
        }

        if(CheckHorizontal() || CheckVertical())
        {
            if (shouldOutputDebug)
            {
                Debug.Log("Test point CheckH/CheckV" + shouldBeDestroyed);
            }

            shouldBeDestroyed = true;
            gm.tileKillList.Add(transform);
        }

        if (shouldOutputDebug)
        {
            Debug.Log("Test point final" + shouldBeDestroyed);
        }
    }

    public bool CheckHorizontal()
    {
        int xMinus, xPlus;
        xMinus = Mathf.RoundToInt(gridCords.x) - 1;
        xPlus = Mathf.RoundToInt(gridCords.x) + 1;

        Transform left = transform.parent.gameObject.transform.Find(xMinus + "-" + gridCords.y);
        Transform right = transform.parent.gameObject.transform.Find(xPlus + "-" + gridCords.y);

        if (shouldOutputDebug)
        {
            Debug.Log("Test point CheckHorizontal" + xMinus + " " + xPlus );
        }

        if (left && right)
        {
            if(left.GetComponent<Tile_Parent>() && right.GetComponent<Tile_Parent>())
            {
                if(left.GetComponent<Tile_Parent>().m_tileColor == m_tileColor && right.GetComponent<Tile_Parent>().m_tileColor == m_tileColor)
                {
                    left.GetComponent<Tile_Parent>().SetDestroyOverRide();
                    right.GetComponent<Tile_Parent>().SetDestroyOverRide();

                    return true;
                }
            }
        }

        return false;
    }

    public bool CheckVertical()
    {
        int yMinus, yPlus;
        yMinus = Mathf.RoundToInt(gridCords.y) - 1;
        yPlus = Mathf.RoundToInt(gridCords.y) + 1;

        Transform down = transform.parent.gameObject.transform.Find(gridCords.x + "-" + yMinus);
        Transform up = transform.parent.gameObject.transform.Find(gridCords.x + "-" + yPlus);

        if (shouldOutputDebug)
        {
            Debug.Log("Test point CheckVert" + down.name + " " + up.name);
        }

        if (down && up)
        {
            if (down.GetComponent<Tile_Parent>() && up.GetComponent<Tile_Parent>())
            {
                if (down.GetComponent<Tile_Parent>().m_tileColor == m_tileColor && up.GetComponent<Tile_Parent>().m_tileColor == m_tileColor)
                {
                    down.GetComponent<Tile_Parent>().SetDestroyOverRide();
                    up.GetComponent<Tile_Parent>().SetDestroyOverRide();

                    return true;
                }
            }
        }

        return false;
    }

    public void SetDestroyOverRide()
    {
        shouldBeDestroyed = true;
        gm.tileKillList.Add(transform);
    }

    public bool isAdjacent()
    {
        if(Mathf.Abs(gridCords.x - gm.currentSelectedTile.gridCords.x) == 1 || Mathf.Abs(gridCords.y - gm.currentSelectedTile.gridCords.y) == 1)
        {
            if(shouldOutputDebug)
            {
                Debug.Log("Positive Match");
            }
            return true;
        }

        return false;
    }

    private void OnMouseDown()
    {
        if(gm.currentSelectedTile == null && gm.isMatchingEnabled)
        {
            gm.currentSelectedTile = this;
        }
    }

    private void OnMouseOver()
    {
        if(gm.currentSelectedTile != this && Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(isAdjacent())
            {
                gm.currentSelectedTile.newTargetLocation = transform.position;
                newTargetLocation = gm.currentSelectedTile.transform.position;
                gm.DisableTileGravity();
                gm.currentSelectedTile = null;
                gm.isMatchingEnabled = false;
                
            }
            else
            {
                if(shouldOutputDebug)
                {
                    Debug.Log("Failed Match");
                }
                gm.currentSelectedTile = null;
            }
        }
    }

    public void MoveTowardsNewLocation(Vector3 newLocation)
    {
        if(transform.position != newLocation && newLocation != new Vector3(0,0,0))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
            transform.position = Vector3.Lerp(transform.position, newLocation, Time.deltaTime * tileMoveSpeed);

            if (transform.position == newLocation)
            {
                gm.UpdateGrid();
            }
        }
        else
        {
            gm.RenameGrid();
            //gm.EnableTileGravity();
            newTargetLocation = new Vector3(0, 0, 0);
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = true;
        }
    }
}
