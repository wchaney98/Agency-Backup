using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

class PlayerCoverArea : MonoBehaviour
{
    public PlayerController Player;

    private PostProcessVolume volume;

    private void Start()
    {
        volume = GameObject.Find("GlobalProcessingVolume").GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (Player != null)
        {
            transform.position = Player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            collision.gameObject.GetComponent<CoverArea>().InPlayerRadius = true;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.Team == Team.Enemy)
            {
                SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.BulletWhizz0, SoundFile.BulletWhizz1, SoundFile.BulletWhizz2 }, transform.position);
                //TODO: post processing fx 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CoverArea")
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            collision.gameObject.GetComponent<CoverArea>().InPlayerRadius = false;
        }
    }
}
