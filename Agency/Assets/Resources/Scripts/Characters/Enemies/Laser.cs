using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : Bullet
{
    private bool goingToHitCover = false;

    /// <summary>
    /// Checks if the bullet is going over cover
    /// </summary>
    public override void CheckPath()
    {
    }

    protected override void MyOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();

            if (Creator != collision.gameObject)
            {
                if (chr.Team != Team)
                {
                    chr.TakeDamage(1);
                    SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Steve0 }, transform.position);
                    Destroy(gameObject);
                }
            }
            else
            {
                //SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.BulletWhizz0, SoundFile.BulletWhizz1, SoundFile.BulletWhizz2 }, transform.position);
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            Speed = 0;
            ParticleManager.SpawnLaserExplosionAt(ParticleSize.SMALL, transform.position);
        }
        else if (collision.gameObject.tag == "CoverBlock")
        {
            collision.gameObject.GetComponent<CoverBlock>().ManualOnDestroy();
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
