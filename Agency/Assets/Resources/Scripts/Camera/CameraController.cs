using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private bool screenShaking = false;
    private float duration = 1f;
    private float intensity = 0.7f;
    private float timer = 0f;

    private Coroutine zoomingCoroutine = null;
    
    public void ShakeScreen(EventParam e)
    {
        timer = 0f;
        intensity = e.float1;
        duration = e.float2;
        screenShaking = true;
    }

    public void ZoomSlap(EventParam e)
    {
        if (zoomingCoroutine != null)
            StopCoroutine(zoomingCoroutine);
        Camera.main.orthographicSize = 4.8f;
        zoomingCoroutine = StartCoroutine(ZoomIn());
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

    void Start()
    {
        EventManager.Instance.StartListening("ScreenShake", ShakeScreen);
        EventManager.Instance.StartListening("ZoomSlap", ZoomSlap);
        FindPlayer();
    }

    void LateUpdate()
    {
        // Done in late update so that player's position is changed before setting the camera pos
        FindPlayer();

        Vector3 temp = Player.transform.position;
        temp.z = transform.position.z;
        if (screenShaking && timer < duration)
        {
            temp += UnityEngine.Random.insideUnitSphere * intensity;
            timer += Time.deltaTime;
        }
        else if (screenShaking && timer >= duration)
        {
            screenShaking = false;
            timer = 0f;
        }
        
        transform.position = temp;
    }

    void FindPlayer()
    {
        if (Player == null)
        {
            try
            {
                Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
            }
            catch
            {
                Debug.Log("Couldn't find player, might be a scene restart...");
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.StopListening("ScreenShake", ShakeScreen);
        EventManager.Instance.StopListening("ZoomSlap", ZoomSlap);
    }
}
