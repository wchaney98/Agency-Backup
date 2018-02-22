using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ACardBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected CardSlotBehavior slot;

    protected Text textComponent;
    protected Vector3 draggingOffset;

    protected virtual void Start()
    {
        textComponent = GetComponent<Text>();
    }

    protected void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        draggingOffset = mousePos - (Vector2)transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f) - draggingOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(slot.transform.position, transform.position) <= 1f)
        {
            slot.CardLockedIn = true;
            transform.position = slot.transform.position;
        }
        else
        {
            slot.CardLockedIn = false;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public virtual void SetupCard(string title, StringBuilder description, params object[] data)
    {

    }
}
