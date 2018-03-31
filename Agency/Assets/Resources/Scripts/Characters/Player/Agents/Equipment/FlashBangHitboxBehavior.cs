using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class FlashBangHitboxBehavior : MonoBehaviour
{
    public GameObject Creator;

    private int frame = 0;

    private void Update()
    {
        if (frame != 2)
            frame++;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            int mask = 1 << 11;
            if (!Physics2D.Linecast(transform.position, collision.gameObject.transform.position, mask))
            {
                Character chr = collision.gameObject.GetComponent<Character>();
                chr.Flash();
            }
        }
    }
}
