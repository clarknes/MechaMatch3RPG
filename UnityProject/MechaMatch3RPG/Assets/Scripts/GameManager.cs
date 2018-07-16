using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Transform grid;
    public Button updateGridButton;

    public List<Transform> tileList = new List<Transform>();

    public List<Transform> activeGrid = new List<Transform>();

    public List<Transform> tileKillList = new List<Transform>();

    public List<int> xValues = new List<int>();

// Use this for initialization
void Start () {
        GenerateGrid();

        for (int i = 0; i < 9; i++)
        {
            xValues.Add(new int());
        }
    }
	
	// Update is called once per frame
	void Update () {
        updateGridButton.GetComponent<Button>().interactable = UpdateVelocity();

    }

    void GenerateGrid()
    {
        for(int rowNum = 0; rowNum < 9; rowNum++)
        {
            for(int colNum = 0; colNum < 9; colNum++)
            {
                int whichGem = Random.Range(0, 5);

                Transform temp = Instantiate(tileList[whichGem], new Vector3(colNum, rowNum, 0), tileList[whichGem].rotation);

                temp.gameObject.name = colNum + "-" + rowNum;

                temp.parent = grid;
                temp.GetComponent<Tile_Parent>().gridCords.x = colNum;
                temp.GetComponent<Tile_Parent>().gridCords.y = rowNum;

                activeGrid.Add(temp);
            }
        }
    }

    public void UpdateGrid()
    {
        foreach(Transform fooTrans in activeGrid)
        {
            fooTrans.GetComponent<Tile_Parent>().CheckIfShouldBeDestroyed();
        }

        foreach(Transform fooTrans in tileKillList)
        {
            activeGrid.Remove(fooTrans);
            Destroy(fooTrans.gameObject);
        }

        tileKillList.Clear();
        RestockGrid();
    }

    public bool UpdateVelocity()
    {
        foreach (Transform fooTrans in activeGrid)
        {
            if(fooTrans.gameObject.GetComponent<Rigidbody>().velocity.y != 0)
            {
                return false;
            }
        }

        return true;
    }

    void RestockGrid()
    {

        for(int i = 0; i < activeGrid.Count; i++)
        {
            switch (Mathf.RoundToInt(activeGrid[i].GetComponent<Tile_Parent>().gridCords.x))
            {
                case 0:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[0];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(0, xValues[0]);
                    xValues[0]++;
                    break;
                case 1:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[1];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(1, xValues[1]);
                    xValues[1]++;
                    break;
                case 2:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[2];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(2, xValues[3]);
                    xValues[2]++;
                    break;
                case 3:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[3];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(3, xValues[3]);
                    xValues[3]++;
                    break;
                case 4:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[4];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(4, xValues[4]);
                    xValues[4]++;
                    break;
                case 5:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[5];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(5, xValues[5]);
                    xValues[5]++;
                    break;
                case 6:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[6];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(6, xValues[6]);
                    xValues[6]++;
                    break;
                case 7:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[7];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(7, xValues[7]);
                    xValues[7]++;
                    break;
                case 8:
                    activeGrid[i].name = activeGrid[i].GetComponent<Tile_Parent>().gridCords.x + "-" + xValues[8];
                    activeGrid[i].GetComponent<Tile_Parent>().gridCords = new Vector2(8, xValues[8]);
                    xValues[8]++;
                    break;
            }
        }

        for(int i = 0; i < xValues.Count; i++)
        {
            for(int j = 8; j >= xValues[i]; j--)
            {
                int whichGem = Random.Range(0, 2);

                Transform temp = Instantiate(tileList[whichGem], new Vector3(i, j, 0), tileList[whichGem].rotation);

                temp.gameObject.name = i + "-" + j;

                temp.parent = grid;
                temp.GetComponent<Tile_Parent>().gridCords.x = i;
                temp.GetComponent<Tile_Parent>().gridCords.y = j;

                activeGrid.Add(temp);
            }
        }

        for(int i = 0; i < xValues.Count; i++)
        {
            xValues[i] = 0;
        }
    }
}
