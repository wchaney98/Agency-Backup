using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerCoverArea : MonoBehaviour
{
    public PlayerController Player;

    private void Update()
    {
        transform.position = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            collision.gameObject.GetComponent<CoverArea>().InPlayerRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            collision.gameObject.GetComponent<CoverArea>().InPlayerRadius = false;
        }
    }
}
