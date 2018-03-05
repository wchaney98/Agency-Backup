using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContractCardBehavior : ACardBehavior
{
    public Contract Contract;
    
    protected override void Start()
    {
        base.Start();
        slot = GameObject.Find("ContractLockIn").GetComponent<CardSlotBehavior>();
    }

    public override void SetupCard(string title, StringBuilder description, params object[] data)
    {

    }
}
