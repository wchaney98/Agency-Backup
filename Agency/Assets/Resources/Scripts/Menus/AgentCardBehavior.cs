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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.Instance.TriggerEvent("ActivateHoverInfo", new EventParam(gameObject));
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        EventManager.Instance.TriggerEvent("DeactivateHoverInfo", new EventParam(gameObject));
    }
}
