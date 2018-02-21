using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool InCover = false;
    public Team Team { get; set; }

    protected int health = 1;
    protected bool flashed = false;

    protected SpriteRenderer spriteRenderer;
    protected List<Collider2D> occupiedCoverAreas = new List<Collider2D>();

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            else
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = new Color(.5f, .5f, .5f, 1f);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            InCover = true;
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(.5f, .5f, .5f, 1f);
            occupiedCoverAreas.Add(collision);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea" && occupiedCoverAreas.Contains(collision))
            occupiedCoverAreas.Remove(collision);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
    }
}
