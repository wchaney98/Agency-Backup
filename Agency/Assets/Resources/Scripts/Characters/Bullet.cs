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
    public float LifeTime { get; set; }
    public LayerMask LayerMask;

    protected float timer = 0f;
    private bool passedThroughCover = false;
    protected RaycastHit2D[] hitPoint = new RaycastHit2D[10];
    protected List<GameObject> objectsEntered = new List<GameObject>();
    protected Vector2 posLastFrame;
    public int Damage = 1;

    private bool hitWall = false;

    /// <summary>
    /// Checks if the bullet is going over cover
    /// </summary>
    public virtual void CheckPath()
    {
        Ray2D ray = new Ray2D(Creator.transform.position, Direction);

        hitPoint = Physics2D.RaycastAll(Creator.transform.position, Direction, Mathf.Infinity, LayerMask);
        foreach (RaycastHit2D hit in hitPoint)
        {
            if (hit.collider.gameObject.CompareTag("CoverBlock"))
            {
                passedThroughCover = true;
                break;
            }
        }
    }

    protected virtual void Start()
    {
        GetComponent<TrailRenderer>().sortingLayerName = "FX";
        Creator = null;
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        posLastFrame = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += (Vector3)Direction.normalized * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > LifeTime)
        {
            Destroy(gameObject);
        }

        ContinuousCollisionCheck();
        foreach (RaycastHit2D hit in hitPoint)
        {
            if (hit.collider.gameObject != null)
            {
                MyOnTriggerEnter2D(hit.collider);
            }

        }
    }

    protected virtual void ContinuousCollisionCheck()
    {
        Vector2 dir = posLastFrame - (Vector2)transform.position;
        float distance = Vector2.Distance(transform.position, posLastFrame);

        hitPoint = Physics2D.RaycastAll(transform.position, dir.normalized, distance);
        posLastFrame = transform.position;
    }

    protected virtual void MyOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();

            if (Creator != collision.gameObject && ((chr.InCover && !passedThroughCover) || !chr.InCover))
            {
                if (chr.Team != Team)
                {
                    chr.TakeDamage(Damage);
                    ParticleManager.SpawnBloodFleshAt(transform.position, Direction);
                    Destroy(gameObject);
                }
            }
            else
            {
                //SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.BulletWhizz0, SoundFile.BulletWhizz1, SoundFile.BulletWhizz2 }, transform.position);
            }
        }
        if (!hitWall && collision.gameObject.tag == "Wall")
        {
            hitWall = true;
            Speed = 0;
            if (Team == Team.Player)
                ParticleManager.SpawnSparksAt(ParticleType.SMALL, transform.position, true);
            else
                ParticleManager.SpawnSparksAt(ParticleType.SMALL, transform.position);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();

            if (Creator != collision.gameObject && ((chr.InCover && !passedThroughCover) || !chr.InCover))
            {
            }
            else
            {
                SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.BulletWhizz0, SoundFile.BulletWhizz1, SoundFile.BulletWhizz2 }, transform.position);
            }
        }
    }
}
