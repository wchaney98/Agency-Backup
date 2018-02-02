using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public GameObject Creator { get; set; }

    private float timer = 0f;

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
            if (Creator != collision.gameObject)
                collision.gameObject.GetComponent<Character>().TakeDamage(1);
        }
    }
}
