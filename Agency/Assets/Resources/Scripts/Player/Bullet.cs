using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }

    private float timer = 0f;

    void Start()
    {
        GetComponent<TrailRenderer>().sortingLayerName = "FX";
    }

    void Update()
    {
        transform.position += (Vector3)Direction.normalized * Speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            Destroy(gameObject);
        }
    }
}
