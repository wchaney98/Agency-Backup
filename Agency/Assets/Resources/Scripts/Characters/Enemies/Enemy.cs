using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected Transform playerTransform;

    const float MIN_SHOT_INTERVAL = 1f;
    const float MAX_SHOT_INTERVAL = 2.3f;

    float shotTimer = 0f;
    float currShotDelay = 0f;
    GameObject bulletPrefab;

    public override void Start()
    {
        base.Start();
        Team = Team.Enemy;

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet2");

        playerTransform = GameObject.FindObjectOfType<PlayerController>().transform;
        currShotDelay = Random.Range(MIN_SHOT_INTERVAL, MAX_SHOT_INTERVAL);
    }

    public override void Update()
    {
        base.Update();
        Vector3 playerPos = playerTransform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg);

        shotTimer += Time.deltaTime;
        if (shotTimer >= currShotDelay)
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            scr.Direction = playerPos - transform.position;
            scr.Speed = 30f;
            scr.Creator = gameObject;
            scr.Team = Team.Enemy;
            scr.CheckPath();

            shotTimer = 0f;
            currShotDelay = Random.Range(MIN_SHOT_INTERVAL, MAX_SHOT_INTERVAL);
            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.PistolShot1 }, transform.position);
        }
    }
}
