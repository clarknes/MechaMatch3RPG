using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAttackEvent : EventParent {

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManagerV2>();
        em = FindObjectOfType<EventManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void AddEventToStack(List<EventParent> eventParents)
    {
        removeEvent = false;
        eventParents.Add(this);
    }

    public override void TriggeredEvent()
    {
        if (gm.player2.shield > 0 && gm.player2.shield <= gm.player1.charge)
        {
            gm.player1.charge -= gm.player2.shield;
            gm.player2.shield = 0;
            gm.player2.health -= gm.player1.charge;
            gm.player1.charge = 0;
            gm.player1.UpdateCharge();
        }
        else if (gm.player2.shield > 0)
        {
            gm.player2.shield -= gm.player1.charge;
            gm.player1.charge = 0;
            gm.player1.UpdateCharge();
        }
        else
        {
            gm.player2.health -= gm.player1.charge;
            gm.player1.charge = 0;
            gm.player1.UpdateCharge();
        }
        Debug.Log("TriggeredGenericAttack");

        if(shouldLoop)
        {
            if(currentLoopNumber < maxLoopNumber)
            {
                currentLoopNumber++;
            }
            else if(maxLoopNumber < 0)
            {
                currentLoopNumber++;
            }
            else
            {
                removeEvent = true;
                //em.attackPhase.Remove(this);
            }
        }
        else
        {
            removeEvent = true;
        }
    }
}
