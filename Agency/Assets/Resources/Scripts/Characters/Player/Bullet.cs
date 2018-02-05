using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public GameObject Creator { get; set; }
    public LayerMask LayerMask;

    private float timer = 0f;
    private bool passedThroughCover = false;
    private RaycastHit2D[] hitPoint = new RaycastHit2D[10];

    public void CheckPath()
    {
        Ray2D ray = new Ray2D(Creator.transform.position, Direction);

        hitPoint = Physics2D.RaycastAll(Creator.transform.position, Direction, Mathf.Infinity, LayerMask);
        foreach (RaycastHit2D hit in hitPoint)
        {
            if (hit.collider.gameObject.tag == "CoverBlock")
            {
                Debug.Log("Bullet hit cover");
                passedThroughCover = true;
            }
        }
    }

    void Start()
    {
        GetComponent<TrailRenderer>().sortingLayerName = "FX";
        Creator = null;
    }

    void FixedUpdate()
    {
        transform.position += (Vector3)Direction.normalized * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();
            if (Creator != collision.gameObject && (!passedThroughCover || (passedThroughCover && !chr.InCover)))
                chr.TakeDamage(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "CoverBlock")
        //{
        //    passedThroughCover = true;
        //    Debug.Log("hitcover");
        //}
    }
}
