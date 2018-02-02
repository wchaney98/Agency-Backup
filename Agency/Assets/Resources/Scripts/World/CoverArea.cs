using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverArea : MonoBehaviour
{

    public bool InPlayerRadius = false;

    private Color previousColor;
    private SpriteRenderer spriteRenderer;

    private MouseRaycaster mouseRaycaster;
    private bool mouseIsOver = false;
    private List<GameObject> mouseOverObjects;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mouseRaycaster = GameObject.FindObjectOfType<MouseRaycaster>();
    }

    private void FixedUpdate()
    {
        if (mouseIsOver)
        {
            mouseOverObjects = mouseRaycaster.GetObjectsMouseIsOver();
            if (!mouseOverObjects.Contains(gameObject))
            {
                spriteRenderer.color = previousColor;
                mouseIsOver = false;
                return;
            }

            if (InPlayerRadius)
            {
                spriteRenderer.color = Color.blue;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //TODO: EventManager
                    GameObject.FindObjectOfType<PlayerController>().LerpToCover(transform.position);
                }
            }
        }
        else
        {
            mouseOverObjects = mouseRaycaster.GetObjectsMouseIsOver();
            if (mouseOverObjects.Contains(gameObject))
            {
                previousColor = spriteRenderer.color;
                mouseIsOver = true;
            }
        }
    }
}
