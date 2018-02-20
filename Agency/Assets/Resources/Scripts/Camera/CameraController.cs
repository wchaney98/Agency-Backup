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

    public void ShakeScreen(EventParam e)
    {
        timer = 0f;
        intensity = e.float1;
        duration = e.float2;
        screenShaking = true;
    }

    void Start()
    {
        EventManager.Instance.StartListening("ScreenShake", ShakeScreen);
        try
        {
            Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        }
        catch
        {
            Debug.Log("Couldn't find player, might be a scene restart...");
        }
    }

    void LateUpdate()
    {
        // Done in late update so that player's position is changed before setting the camera pos
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
        Vector3 temp = Player.transform.position;
        temp.z = transform.position.z;
        if (screenShaking && timer < duration)
        {
            temp += UnityEngine.Random.insideUnitSphere * intensity;
            timer += Time.deltaTime;
            //intensity -= Mathf.Lerp(intensity, 0f, timer / duration);
        }
        else if (screenShaking && timer >= duration)
        {
            screenShaking = false;
            timer = 0f;
        }
        
        transform.position = temp;
    }

    private void OnDestroy()
    {
        EventManager.Instance.StopListening("ScreenShake", ShakeScreen);
    }
}
