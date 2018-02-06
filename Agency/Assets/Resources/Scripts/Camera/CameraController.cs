using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

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
        transform.position = temp;
    }
}
