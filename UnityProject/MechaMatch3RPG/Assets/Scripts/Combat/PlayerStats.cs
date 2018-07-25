using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    //General
    public GameManagerV2 gm;
    public LoadoutManager lm;

    //AttackStats
    public int blueAttack;
    public int purpleAttack;
    public int greenAttack;
    public int yellowAttack;
    public int redAttack;

    public List<Text> statOverlayText = new List<Text>();

    public float charge, maxCharge;
    public Slider chargeSlider;

    //HealthStats
    public float health, shield;
    public float maxHealth, maxShield;
    public Slider healthSlider, shieldSlider;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameManagerV2>();
        lm = FindObjectOfType<LoadoutManager>();

        if(lm != null)
        {
            GetStatsFromLoadout();
        }

        health = maxHealth;
        UpdatePlayerUI();
	}
	
	// Update is called once per frame
	void Update () {
        GameManagerV2 gm = FindObjectOfType<GameManagerV2>();
        if (gm.toggleStatOverlay)
        {
            UpdateStatText();
        }

        UpdatePlayerUI();
	}

    void UpdateStatText()
    {
        for(int i = 0; i < statOverlayText.Count; i++)
        {
            if (i == 0)
            {
                statOverlayText[i].text = blueAttack.ToString();
            }
            else if (i == 1)
            {
                statOverlayText[i].text = purpleAttack.ToString();
            }
            else if (i == 2)
            {
                statOverlayText[i].text = greenAttack.ToString();
            }
            else if (i == 3)
            {
                statOverlayText[i].text = yellowAttack.ToString();
            }
            else if (i == 4)
            {
                statOverlayText[i].text = redAttack.ToString();
            }
        }
    }

    void UpdatePlayerUI()
    {
        healthSlider.value = health;
        shieldSlider.value = shield;
        healthSlider.maxValue = maxHealth;
        shieldSlider.maxValue = maxShield;
    }

    public void UpdateCharge()
    {
        charge = 0;

        if (chargeSlider != null)
        {
            for (int i = 0; i < gm.cascadeList.Count; i++)
            {
                charge += gm.cascadeScoreList[i];
            }

            chargeSlider.value = charge;
            chargeSlider.maxValue = maxCharge;
        }
    }

    public void GetStatsFromLoadout()
    {
        blueAttack = lm.playerShipTypes[lm.currentSelectedShip].blueAttack;
        purpleAttack = lm.playerShipTypes[lm.currentSelectedShip].purpleAttack;
        greenAttack = lm.playerShipTypes[lm.currentSelectedShip].greenAttack;
        yellowAttack = lm.playerShipTypes[lm.currentSelectedShip].yellowAttack;
        redAttack = lm.playerShipTypes[lm.currentSelectedShip].redAttack;
    }
}
