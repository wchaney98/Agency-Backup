using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public float activeRadius = 0.75f;
    public float fadeTime = 0.4f;

    GameObject player;
    SpriteRenderer spriteRenderer;
    Coroutine triggeredRoutine = null;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player != null)
        {
            if (triggeredRoutine == null)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance <= activeRadius)
                {
                    spriteRenderer.color = Color.red;
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        triggeredRoutine = StartCoroutine(FadeAndDestroy());
                    }
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }

    IEnumerator FadeAndDestroy()
    {
        float t = 0f;
        while (t <= fadeTime)
        {
            Color temp = spriteRenderer.color;
            temp.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            spriteRenderer.color = temp;
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
