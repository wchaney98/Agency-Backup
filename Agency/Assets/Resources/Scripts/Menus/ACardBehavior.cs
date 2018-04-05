using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ACardBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    protected CardSlotBehavior slot;

    protected Text textComponent;
    protected Vector3 draggingOffset;

    protected Vector2 startingPos;
    protected float timeSinceLastClick = 0.2f;

    protected virtual void Start()
    {
        textComponent = GetComponentInChildren<Text>();
        startingPos = transform.position;
    }

    protected void Update()
    {
        timeSinceLastClick += Time.deltaTime;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        draggingOffset = mousePos - (Vector2) transform.position;
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
            slot.LockInCard(this);
            transform.position = slot.transform.position;
        }
        else
        {
            slot.UnlockCard(this);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public virtual void SetupCard(string title, StringBuilder description, params object[] data)
    {
        if (textComponent == null) GetComponentInChildren<Text>().text = title + ": " + description;
        else textComponent.text = title + ": " + description;
    }

    // Double click stuff
    public void OnPointerDown(PointerEventData eventData)
    {
        if (timeSinceLastClick <= 0.2f)
        {
            if (slot.CardLockedIn)
            {
                slot.LockedCard.gameObject.transform.position = slot.LockedCard.startingPos;
                slot.UnlockCard(slot.LockedCard);
            }
            slot.LockInCard(this);
            transform.position = slot.transform.position;
        }
        timeSinceLastClick = 0f;
    }
}