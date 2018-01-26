using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private const float MOVE_SPEED = 3f;

    private GameObject bulletPrefab;


    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        if (bulletPrefab == null)
        {
            Debug.Log("bulletPrefab not found");
        }
    }

    void Update()
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
        transform.position += (Vector3) (movementVector);

        // Handle looking at cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg);

        // Handle shooting
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            scr.Direction = mousePos - transform.position;
            scr.Speed = 30f;
        }
    }
}
