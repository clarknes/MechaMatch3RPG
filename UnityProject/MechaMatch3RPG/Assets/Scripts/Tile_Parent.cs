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

    //DebugTools
    public bool shouldOutputDebug;

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
