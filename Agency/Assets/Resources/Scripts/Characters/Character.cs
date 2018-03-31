using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool InCover = false;
    public Team Team { get; set; }

    protected int health = 1;

    public float FlashTime = 1.2f;
    protected bool flashed = false;
    protected float flashTimer = 0f;

    protected bool peeking = false;

    protected SpriteRenderer spriteRenderer;
    protected List<Collider2D> occupiedCoverAreas = new List<Collider2D>();

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public virtual void Update()
    {
        if (flashed)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= FlashTime)
            {
                flashed = false;
                flashTimer = 0f;
            }
        }

        if (health <= 0)
        {
            //TODO: Death anim
            Destroy(gameObject);
        }

        if (occupiedCoverAreas.Count == 0 || peeking)
        {
            InCover = false;
            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }
        else
        {
            InCover = true;
            if (spriteRenderer != null)
                spriteRenderer.color = new Color(.5f, .5f, .5f, 1f);
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
        SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.HumanDeath0, SoundFile.HumanDeath1, SoundFile.HumanDeath2 }, transform.position);
        health -= amount;
    }

    public virtual void Flash()
    {
        flashed = true;
    }
}
