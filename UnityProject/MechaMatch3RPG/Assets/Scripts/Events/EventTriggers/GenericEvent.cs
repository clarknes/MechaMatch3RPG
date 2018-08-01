using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEvent : EventParent {



	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManagerV2>();
        em = FindObjectOfType<EventManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TriggeredEvent()
    {

    }
}
