using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Resources = UnityEngine.Resources;

public class PlayerController : Character
{
    public Agent Agent { get; set; }
    
    public GameObject MuzzleFlashObject;

    private AAgentController agentController;
    
    private float moveSpeed = 3.3f;

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

        rb = GetComponent<Rigidbody2D>();
        
        EventManager.Instance.StartListening("PlayerShoot", PlayMuzzleFlash);
    }

    private void SetupAgent()
    {
        agentController = AgentCreator.InitAgent(Agent.AgentType);
        agentController.Init(Agent);

        specialCooldown = Agent.SpecialCooldown;

        spriteRenderer.sprite = agentController.GetSprite();
    }

    public override void Update()
    {
        base.Update();

        // Handle looking at cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg);

        // Handle shooting
        agentController.ProcessPrimary(gameObject, mousePos, Time.deltaTime, InCover);
        
        // Special
        specialCooldownTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1))
        {
            if (specialCooldownTimer >= specialCooldown)
            {
                if (agentController.ProcessSpecial(gameObject, mousePos))
                    specialCooldownTimer = 0;
            }
            else
            {
                agentController.ProcessDuringSpecial(gameObject, mousePos);
            }
        }
        
        // Universal peeking behavior
        if (Input.GetKey(KeyCode.LeftShift))
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
            movementVector.y += Agent.MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x -= Agent.MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y -= Agent.MoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += Agent.MoveSpeed * Time.deltaTime;
        }
        rb.position += (movementVector);
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

    // TODO use params to modify muzzle flash
    public void PlayMuzzleFlash(EventParam e)
    {
        MuzzleFlashObject.GetComponent<ParticleSystem>().Play(true);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }

    private void OnDestroy()
    {
        EventManager.Instance.StopListening("PlayerShoot", PlayMuzzleFlash);
    }
}
