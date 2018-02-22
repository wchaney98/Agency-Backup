using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : Character
{
    protected Transform playerTransform;

    protected const float MIN_SHOT_INTERVAL = 1f;
    protected const float MAX_SHOT_INTERVAL = 2.3f;

    protected float shotTimer = 0f;
    protected float currShotDelay = 0f;
    protected GameObject bulletPrefab;

    protected int layer = 11;
    protected int mask;

    public override void Start()
    {
        base.Start();
        Team = Team.Enemy;
        playerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        currShotDelay = Random.Range(MIN_SHOT_INTERVAL, MAX_SHOT_INTERVAL);
        mask = 1 << layer;
    }

    public override void Update()
    {
        base.Update();
    }

    protected virtual bool PlayerInVision()
    {
        return !Physics2D.Linecast(transform.position, playerTransform.position, mask);
    }
}
