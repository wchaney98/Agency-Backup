using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : AEnemy
{
    public float Speed = 3.5f;
    public float seekDelay = 0.5f;

    private float seekDelayCounter = 0f;
    private bool startingSeekSoundPlayed = false;

    private Vector3 velocity;

    public override void Start()
    {
        base.Start();
        health = 3;
    }

    public override void Update()
    {
        base.Update();

        if (PlayerInVision())
        {
            Vector3 playerPos = playerTransform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg);

            seekDelayCounter += Time.deltaTime;

            if (seekDelayCounter >= seekDelay)
            {
                if (!startingSeekSoundPlayed)
                {
                    startingSeekSoundPlayed = true;
                    SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.MeleeEnemySeek }, transform.position);
                }

                Vector3 direction = playerPos - transform.position;
                transform.position += direction.normalized * Speed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character" && collision.gameObject.GetComponent<MeleeEnemy>() == null)
        {
            Character chr = collision.gameObject.GetComponent<Character>();
            chr.TakeDamage(1);
            Destroy(gameObject);
            // TODO play different explosion and sound
            ParticleManager.SpawnLaserExplosionAt(ParticleType.SMALL, transform.position);
        }
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.ExplosionMedium }, transform.position);
            ParticleManager.SpawnLaserExplosionAt(ParticleType.SMALL, transform.position);
        }
        else
            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.ArmorHit0, SoundFile.FleshHit0, SoundFile.FleshHit1 }, transform.position);

    }
}
