using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public enum GamePhases
    {
        StartOfTurn,
        MatchPhase,
        AbilityPhase,
        AttackPhase,
        EnemyPhase,
        EndOfTurn
    }

    public GamePhases currentPhase;
    public EventParent endPhaseEvent;
    public GameManagerV2 gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManagerV2>();
    }

    //START OF TURN
    //Here is the data for the start of turn functions.
    //Start of turn handles the resetting of any extra data, resetting it back to the players turn, and handles giving player back choice

    public List<EventParent> startOfTurnEvents = new List<EventParent>();

    //MATCH PHASE
    //Here is the data for the Match Phase functions.
    //Match Phase is when players are given the ability to match tiles. StartOfMatchPhase events trigger before players get control, EndOfMatchPhase events trigger after players cascade has ended

    public List<EventParent> startOfMatchPhase = new List<EventParent>();
    public List<EventParent> endOfMatchPhase = new List<EventParent>();

    //ABILITY PHASE
    //Here is the data for the Ability Phase functions.
    //In the Ability Phase players select their ability for the round. Just like MatchPhase AbilityPhase has a start and end set of triggers for before and after players select ability

    public List<EventParent> startOfAbilityPhase = new List<EventParent>();
    public List<EventParent> endOfAbilityPhase = new List<EventParent>();

    //ATTACK PHASE
    //Here is the data for the Attack Phase functions.
    //In the Attack Phase players never get control. It is simple a phase when the players attack triggers resolve. Thus it only has one list of events

    public List<EventParent> attackPhase = new List<EventParent>();

    public void ResolveAttackPhase()
    {
        endPhaseEvent.AddEventToStack(attackPhase);

        foreach(EventParent fooEvent in attackPhase)
        {
            fooEvent.TriggeredEvent();
        }

        List<int> removeList = new List<int>();

        for(int i = 0; i < attackPhase.Count; i++)
        {
            if(attackPhase[i].removeEvent)
            {
                removeList.Add(i);
            }
        }

        foreach(int fooI in removeList)
        {
            attackPhase.Remove(attackPhase[fooI]);
        }
        removeList.Clear();
    }

    //ENEMY PHASE
    //Here is the data for the Enemy Phase functions.
    //In the Attack Phase players never get control. It is simple a phase when the enemy attack triggers resolve, and for the enemy AI to place more events. Thus it only has one list of events

    public List<EventParent> enemyPhase = new List<EventParent>();

    //END OF TURN
    //Here is a debug phase. It should normally only pass priority back to the StartOfTurn phase, if it spends more time here something likely went wrong

    public List<EventParent> endOfTurn = new List<EventParent>();

}
