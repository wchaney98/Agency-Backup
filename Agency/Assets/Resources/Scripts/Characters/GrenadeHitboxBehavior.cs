﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GrenadeHitboxBehavior : MonoBehaviour
{
    public GameObject Creator;

    private void LateUpdate()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Character chr = collision.gameObject.GetComponent<Character>();
            chr.TakeDamage(2);
            // TODO more painful sound fx
            SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Steve0 }, transform.position);
        }
    }
}