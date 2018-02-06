using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool InCover = false;

    protected int health = 1;
    protected bool flashed = false;

    protected SpriteRenderer spriteRenderer;
    protected List<Collider2D> occupiedCoverAreas = new List<Collider2D>();

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(gameObject.name + ": " + spriteRenderer);
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            //TODO: Death anim
            Destroy(gameObject);
        }

        if (InCover)
        {
            if (occupiedCoverAreas.Count == 0)
            {
                InCover = false;
                if (spriteRenderer != null)
                    spriteRenderer.color = Color.white;
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            InCover = true;
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(.7f, .7f, .7f, 1f);
            occupiedCoverAreas.Add(collision);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea" && occupiedCoverAreas.Contains(collision))
            occupiedCoverAreas.Remove(collision);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
