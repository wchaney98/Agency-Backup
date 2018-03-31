using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Holds the visual effect and creates the hitbox
/// </summary>
public class FlashBang : MonoBehaviour
{
    public GameObject HitboxPrefab;

    private void Start()
    {
        Instantiate(HitboxPrefab, transform.position, Quaternion.identity);
        SoundManager.Instance.DoPlayOneShot(new SoundFile[] { SoundFile.Flashbang0 }, transform.position);
        Destroy(gameObject, 3f);
    }
}
