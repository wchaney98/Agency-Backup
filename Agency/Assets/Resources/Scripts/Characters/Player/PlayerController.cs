using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerController : Character
{
    public Agent Agent { get; set; }
    
    public GameObject MuzzleFlashObject;

    private float moveSpeed = 3.3f;

    private GameObject bulletPrefab;
    private GameObject specialPrefab;
    private Rigidbody2D rb;

    private Coroutine zoomingCoroutine;

    private float specialCooldownTimer = 0f;
    private float specialCooldown = 1f;

    public override void Start()
    {
        base.Start();
        Agent = PersistentData.Instance.CurrentAgent;
        SetupAgent();
        Team = Team.Player;

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        specialPrefab = Resources.Load<GameObject>("Prefabs/Laser1");
        if (bulletPrefab == null)
        {
            Debug.Log("bulletPrefab not found");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void SetupAgent()
    {
        
    }

    public override void Update()
    {
        base.Update();

        // Handle looking at cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg);

        // Handle shooting
        if (!InCover && Input.GetMouseButtonDown(0))
        {
            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            scr.Direction = mousePos - transform.position;
            scr.Speed = 35f;
            scr.Creator = gameObject;
            scr.Team = Team.Player;
            scr.LifeTime = 2f;
            scr.CheckPath();

            if (zoomingCoroutine != null)
                StopCoroutine(zoomingCoroutine);
            Camera.main.orthographicSize = 4.8f;
            zoomingCoroutine = StartCoroutine(ZoomIn());

            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.PistolShot0 }, transform.position);
            MuzzleFlashObject.GetComponent<ParticleSystem>().Play(true);
        }
        
        // Universal peeking behavior
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            peeking = true;
        }
        else
        {
            peeking = false;
        }

        // Misc
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainGame");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("ManagementScene");
        }

        // Special
        specialCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && specialCooldownTimer >= specialCooldown)
        {
            specialCooldownTimer = 0f;

            GameObject b = Instantiate(specialPrefab, transform.position, Quaternion.identity);
            Bullet scr = b.GetComponent<Bullet>();
            scr.Direction = mousePos - transform.position;
            scr.Speed = 10f;
            scr.Creator = gameObject;
            scr.Team = Team.Player;
            scr.LifeTime = 15f;

            if (zoomingCoroutine != null)
                StopCoroutine(zoomingCoroutine);
            Camera.main.orthographicSize = 4.6f;
            zoomingCoroutine = StartCoroutine(ZoomIn());

            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Laser0 }, transform.position);
            MuzzleFlashObject.GetComponent<ParticleSystem>().Play(true);
        }
    }

    public void LerpToCover(Vector3 pos)
    {
        StartCoroutine(JumpToCover(pos));
    }

    private void FixedUpdate()
    {
        // Handle WASD movement
        Vector2 movementVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += moveSpeed * Time.deltaTime;
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
            Camera[] cams = Camera.allCameras;
            for (int i = 0; i < cams.Length; i++)
            {
                cams[i].orthographicSize = Mathf.Lerp(start, goal, t / length);
            }
            yield return null;
        }
    }

    IEnumerator JumpToCover(Vector3 goal)
    {
        Vector3 start = transform.position;
        float length = 0.05f;
        float time = 0f;
        while (time <= length)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, goal, time / length);
            yield return null;
        }
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }
}
