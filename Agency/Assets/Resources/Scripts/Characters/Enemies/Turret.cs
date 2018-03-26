using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : AEnemy
{
    public float deployTime = 1f;

    new const float MIN_SHOT_INTERVAL = 0.5f;
    new const float MAX_SHOT_INTERVAL = 0.5f;

    private bool canShoot = false;
    private SpriteRenderer holeSpriteRenderer;

    private Coroutine deploying = null;

    public override void Start()
    {
        base.Start();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Laser2");
        holeSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        holeSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public override void Update()
    {
        if (health <= 0)
        {
            //TODO: Death anim
            Destroy(gameObject);
        }

        if (PlayerInVision())
        {
            // Start alpha transitions to show turret deploying
            if (canShoot)
            {
                Vector3 playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
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
                    SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.HeavyShot0 }, transform.position);
                }
            }
            else if (deploying == null)
            {
                deploying = StartCoroutine(FadeIn());
            }
        }

        InCover = false;
    }

    private IEnumerator FadeIn()
    {
        float transitionTime = deployTime / 2;
        float t = 0;
        SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.TurretDeploy }, transform.position);

        while (t < transitionTime)
        {
            holeSpriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, t / transitionTime));

            t += Time.deltaTime;
            yield return null;
        }
        t = 0;
        while (t < transitionTime)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, t / transitionTime));

            t += Time.deltaTime;
            yield return null;
        }
        canShoot = true;
    }

    public override void TakeDamage(int amount)
    {
        SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.ExplosionMedium }, transform.position);
        health -= amount;
    }
}
