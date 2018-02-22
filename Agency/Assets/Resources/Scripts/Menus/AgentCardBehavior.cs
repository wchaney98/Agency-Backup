using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AgentCardBehavior : ACardBehavior
{
    protected override void Start()
    {
        base.Start();
        slot = GameObject.Find("AgentLockIn").GetComponent<CardSlotBehavior>();
    }

    public override void SetupCard(string title, StringBuilder description, params object[] data)
    {

    }

}
