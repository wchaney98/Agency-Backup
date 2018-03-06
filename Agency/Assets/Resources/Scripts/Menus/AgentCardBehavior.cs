using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentCardBehavior : ACardBehavior
{
    public Agent Agent;
    
    protected override void Start()
    {
        base.Start();
        slot = GameObject.Find("AgentLockIn").GetComponent<CardSlotBehavior>();
    }
}
