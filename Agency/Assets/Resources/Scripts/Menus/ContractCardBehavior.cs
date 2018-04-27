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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.Instance.TriggerEvent("ActivateHoverInfo", new EventParam(gameObject));
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        EventManager.Instance.TriggerEvent("DeactivateHoverInfo", new EventParam(gameObject));
    }
}
