using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParent : MonoBehaviour
{
    public GameManagerV2 gm;
    public EventManager em;
    public bool shouldLoop;
    public int currentLoopNumber, maxLoopNumber;
    public bool removeEvent;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameManagerV2>();
        em = FindObjectOfType<EventManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void TriggeredEvent()
    {
        //Debug.Log("TriggeredGenericAttack");
    }

    public virtual void AddEventToStack(List<EventParent> eventParents)
    {
        removeEvent = true;
    }
}
