using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverArea : MonoBehaviour
{

    public bool InPlayerRadius = false;

    private Color previousColor;
    private SpriteRenderer spriteRenderer;

    private bool mouseIsOver = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        previousColor = spriteRenderer.color;
        mouseIsOver = true;
    }

    private void Update()
    {
        if (mouseIsOver)
        {
            Debug.Log("heyeye");
            if (InPlayerRadius)
            {
                spriteRenderer.color = Color.green;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //TODO: EventManager
                    GameObject.FindObjectOfType<PlayerController>().LerpToCover(transform.position);
                }
            }
        }
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = previousColor;
        mouseIsOver = false;
    }
}
