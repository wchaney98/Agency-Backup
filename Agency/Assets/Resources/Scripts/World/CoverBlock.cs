﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBlock : MonoBehaviour
{
    public void ManualOnDestroy()
    {
        //GameObject.FindObjectOfType<CameraController>().ShakeScreen(0.35f, 0.08f);
        EventManager.Instance.TriggerEvent("ScreenShake", new EventParam(0.08f, 0.35f));
        Instantiate(LevelBuilder.floorPrefab, transform.position, Quaternion.identity, LevelBuilder.parent.transform);
        ParticleManager.SpawnLaserExplosionAt(ParticleSize.BIG, transform.position);
        Destroy(gameObject);
    }
}
