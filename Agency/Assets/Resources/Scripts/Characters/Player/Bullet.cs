using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Team
{
    Player,
    Enemy
}
public class Bullet : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public GameObject Creator { get; set; }
    public Team Team { get; set; }
    public LayerMask LayerMask;

    private float timer = 0f;
    private bool passedThroughCover = false;
    private RaycastHit2D[] hitPoint = new RaycastHit2D[10];

    /// <summary>
    /// Checks if the bullet is going over cover
    /// </summary>
    public void CheckPath()
    {
        Ray2D ray = new Ray2D(Creator.transform.position, Direction);

        hitPoint = Physics2D.RaycastAll(Creator.transform.position, Direction, Mathf.Infinity, LayerMask);
        foreach (RaycastHit2D hit in hitPoint)
        {
            if (hit.collider.gameObject.tag == "CoverBlock")
            {
                passedThroughCover = true;
            }
        }
    }

    void Start()
    {
        GetComponent<TrailRenderer>().sortingLayerName = "FX";
        Creator = null;
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        transform.position += (Vector3)Direction.normalized * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();

            if (Creator != collision.gameObject && ((chr.InCover && !passedThroughCover) || !chr.InCover))
            {
                if (chr.Team == Team.Player)
                    SceneManager.LoadScene("MainGame");
                if (chr.Team != Team)
                {
                    chr.TakeDamage(1);
                    SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Steve0 }, transform.position);
                    Destroy(gameObject);
                }
            }
            else
            {
                SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.BulletWhizz0, SoundFile.BulletWhizz1, SoundFile.BulletWhizz2 }, transform.position);
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            Speed = 0;
        }
    }
}
