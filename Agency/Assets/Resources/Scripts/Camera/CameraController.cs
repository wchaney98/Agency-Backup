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

    public void ShakeScreen(float duration, float intensity)
    {
        timer = 0f;
        this.duration = duration;
        this.intensity = intensity;
        screenShaking = true;
    }

    void Start()
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
            temp += Random.insideUnitSphere * intensity;
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
}
