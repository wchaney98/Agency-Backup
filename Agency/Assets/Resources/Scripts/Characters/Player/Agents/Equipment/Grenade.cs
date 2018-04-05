using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grenade : Bullet
{
    public GameObject GrenadeHitboxPrefab;
    private bool madeExplosion = false;

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 90f * Time.deltaTime));
    }

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
                if (chr.Team == Team)
                {
                    return;
                }
            }
            // Spawn grenade hitbox prefab
            SpawnGrenadeHitbox();
        }
        else if (collision.gameObject.tag == "Wall")
        {
            // Spawn grenade hitbox prefab
            SpawnGrenadeHitbox();
        }
    }

    public void ManualDetonate()
    {
        SpawnGrenadeHitbox();
    }

    private void SpawnGrenadeHitbox()
    {
        Instantiate(GrenadeHitboxPrefab, transform.position, Quaternion.identity);

        ParticleManager.SpawnLaserExplosionAt(ParticleType.BIG2, transform.position);
        SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.ExplosionMedium }, transform.position);
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
