using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : AEnemy
{
    new const float MIN_SHOT_INTERVAL = 2f;
    new const float MAX_SHOT_INTERVAL = 2f;

    public override void Start()
    {
        base.Start();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Laser1");
        health = 8;
    }

    public override void Update()
    {
        base.Update();

        if (PlayerInVision())
        {
            Vector3 playerPos = playerTransform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg);

            shotTimer += Time.deltaTime;
            if (shotTimer >= currShotDelay)
            {
                GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet scr = b.GetComponent<Bullet>();
                scr.Direction = playerPos - transform.position;
                scr.Speed = 10f;
                scr.Creator = gameObject;
                scr.Team = Team.Enemy;
                scr.LifeTime = 15f;

                shotTimer = 0f;
                currShotDelay = Random.Range(MIN_SHOT_INTERVAL, MAX_SHOT_INTERVAL);
                SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Laser0 }, transform.position);
            }
        }
    }
}
