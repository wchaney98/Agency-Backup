using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public GameObject MuzzleFlashObject;

    private const float MOVE_SPEED = 3f;

    private GameObject bulletPrefab;
    private Rigidbody2D rb;

    private Coroutine zoomingCoroutine;

    public override void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        if (bulletPrefab == null)
        {
            Debug.Log("bulletPrefab not found");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        // Handle looking at cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg);

        // Handle shooting
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            scr.Direction = mousePos - transform.position;
            scr.Speed = 50f;

            if (zoomingCoroutine != null)
                StopCoroutine(zoomingCoroutine);
            Camera.main.orthographicSize = 4.8f;
            zoomingCoroutine = StartCoroutine(ZoomIn());

            if (!MuzzleFlashObject.activeInHierarchy)
                MuzzleFlashObject.SetActive(true);
            MuzzleFlashObject.GetComponent<ParticleSystem>().Play(true);
            Debug.Log(MuzzleFlashObject.GetComponent<ParticleSystem>().isPlaying);
        }
    }

    private void FixedUpdate()
    {
        // Handle WASD movement
        Vector2 movementVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += MOVE_SPEED * Time.deltaTime;
        }
        rb.position += (movementVector);
    }

    IEnumerator ZoomIn()
    {
        float start = Camera.main.orthographicSize;
        float goal = 5f;
        float length = 0.35f;
        float time = 0f;
        while (Camera.main.orthographicSize < 5)
        {
            time += Time.deltaTime;
            float t = Mathf.Sin(time * Mathf.PI * 0.5f);
            Camera.main.orthographicSize = Mathf.Lerp(start, goal, t / length);
            yield return null;
        }
    }
}
