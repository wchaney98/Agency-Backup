using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBlock : MonoBehaviour
{
    public void ManualOnDestroy()
    {
        Instantiate(LevelBuilder.floorPrefab, transform.position, Quaternion.identity, LevelBuilder.parent.transform);
        ParticleManager.SpawnLaserExplosionAt(ParticleSize.BIG, transform.position);
        Destroy(gameObject);
    }
}
