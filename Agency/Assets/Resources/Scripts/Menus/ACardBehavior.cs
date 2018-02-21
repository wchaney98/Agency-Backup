using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ACardBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected Text textComponent;
    protected Vector3 draggingOffset;

    protected void Start()
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
        draggingOffset = Vector3.zero;
    }

    public virtual void SetupCard(string title, StringBuilder description, params object[] data)
    {

    }
}
