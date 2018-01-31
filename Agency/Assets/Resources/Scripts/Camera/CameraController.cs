using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    void LateUpdate()
    {
        // Done in late update so that player's position is changed before setting the camera pos
        if (Player == null)
        {
            Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        }
        Vector3 temp = Player.transform.position;
        temp.z = transform.position.z;
        transform.position = temp;
    }
}
