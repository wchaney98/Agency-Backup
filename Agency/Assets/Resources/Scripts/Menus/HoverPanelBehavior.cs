using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverPanelBehavior : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        if (image != null)
            image.enabled = true;
    }

    private void OnDisable()
    {
        if (image != null)
            image.enabled = false;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }
}
