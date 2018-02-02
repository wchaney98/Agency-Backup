using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Transform playerTransform;

    public override void Start()
    {
        playerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    public override void Update()
    {
        base.Update();
        Vector3 playerPos = playerTransform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg);
    }
}
