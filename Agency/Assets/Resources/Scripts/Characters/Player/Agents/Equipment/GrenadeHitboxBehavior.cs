using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GrenadeHitboxBehavior : MonoBehaviour
{
    public GameObject Creator;

    private int frame = 0;

    private void Update()
    {
        if (frame != 2)
            frame++;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();
            chr.TakeDamage(6);
            ParticleManager.SpawnBloodFleshAt(collision.gameObject.transform.position, Vector2.zero);
            ParticleManager.SpawnBloodFleshAt(collision.gameObject.transform.position, Vector2.zero);
            ParticleManager.SpawnBloodFleshAt(collision.gameObject.transform.position, Vector2.zero);

            // TODO more painful sound fx... depends on robot or not etc
            //SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Steve0 }, transform.position);
        }
    }
}
