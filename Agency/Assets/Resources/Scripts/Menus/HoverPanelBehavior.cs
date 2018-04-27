using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPanelBehavior : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }
}
