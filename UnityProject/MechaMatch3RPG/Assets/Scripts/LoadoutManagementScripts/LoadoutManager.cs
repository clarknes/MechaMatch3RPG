using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadoutManager : MonoBehaviour {

    //Ship Select Screen
    public List<ShipData> playerShipTypes = new List<ShipData>();
    public int currentSelectedShip;

    //UI Variables
    public List<Text> attackStatText = new List<Text>();
    public Text shipNameText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UpdateSelectScreenText();
        }
	}

    public void UpdateSelectScreenText()
    {
        shipNameText.text = playerShipTypes[currentSelectedShip].name;

        attackStatText[0].text = playerShipTypes[currentSelectedShip].blueAttack.ToString();
        attackStatText[1].text = playerShipTypes[currentSelectedShip].purpleAttack.ToString();
        attackStatText[2].text = playerShipTypes[currentSelectedShip].greenAttack.ToString();
        attackStatText[3].text = playerShipTypes[currentSelectedShip].yellowAttack.ToString();
        attackStatText[4].text = playerShipTypes[currentSelectedShip].redAttack.ToString();
    }

    public void IncreaseCurrentSelectedShip(int amountToIncreaseBy)
    {
        if(currentSelectedShip < playerShipTypes.Count - 1 && amountToIncreaseBy > 0)
        {
            Debug.Log("Case 1");
            currentSelectedShip++;
        }
        else if(currentSelectedShip > 0 && amountToIncreaseBy < 0)
        {
            Debug.Log("Case 2");
            currentSelectedShip--;
        }
        else if(currentSelectedShip >= playerShipTypes.Count - 1 && amountToIncreaseBy > 0)
        {
            Debug.Log("Case 3");
            currentSelectedShip = 0;
        }
        else if(currentSelectedShip <= 0 && amountToIncreaseBy < 0)
        {
            Debug.Log("Case 4");
            currentSelectedShip = playerShipTypes.Count - 1;
        }
        else
        {
            Debug.Log("Case 5");
        }
    }
}
