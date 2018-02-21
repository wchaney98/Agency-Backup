using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public float activeRadius = 0.75f;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= activeRadius)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
