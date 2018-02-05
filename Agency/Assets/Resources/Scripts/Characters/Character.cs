using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool InCover = false;

    protected int health = 1;
    protected bool flashed = false;

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            //TODO: Death anim
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
