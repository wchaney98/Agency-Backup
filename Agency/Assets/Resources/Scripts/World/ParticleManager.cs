using System;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{
    SMALL,
    MEDIUM,
    BIG,
    BIG2
}

public static class ParticleManager
{
    public static GameObject BigExplosion2 = Resources.Load<GameObject>("Prefabs/World/Particle FX/ExplosionBig2");
    public static GameObject BigLaserExplosion = Resources.Load<GameObject>("Prefabs/World/Particle FX/ExplosionBig");
    public static GameObject SmallLaserExplosion = Resources.Load<GameObject>("Prefabs/World/Particle FX/ExplosionSmall");

    public static GameObject SmallSparks = Resources.Load<GameObject>("Prefabs/World/Particle FX/SparksSmall");
    public static GameObject SmallSparksBlue = Resources.Load<GameObject>("Prefabs/World/Particle FX/SparksBlueSmall");

    public static void SpawnLaserExplosionAt(ParticleType type, Vector2 position)
    {
        GameObject boom;
        switch (type)
        {
            case ParticleType.SMALL:
                boom = GameObject.Instantiate(SmallLaserExplosion, position, Quaternion.identity);
                GameObject.Destroy(boom, 3f);
                break;
            case ParticleType.MEDIUM:
                break;
            case ParticleType.BIG:
                boom = GameObject.Instantiate(BigLaserExplosion, position, Quaternion.identity);
                GameObject.Destroy(boom, 3f);
                break;
            case ParticleType.BIG2:
                boom = GameObject.Instantiate(BigExplosion2, position, Quaternion.identity);
                GameObject.Destroy(boom, 3f);
                break;
        }
    }

    public static void SpawnSparksAt(ParticleType type, Vector2 position, bool blue = false)
    {
        GameObject boom;
        switch (type)
        {
            case ParticleType.SMALL:
                if (blue)
                    boom = GameObject.Instantiate(SmallSparksBlue, position, Quaternion.identity);
                else
                    boom = GameObject.Instantiate(SmallSparks, position, Quaternion.identity);

                GameObject.Destroy(boom, 3f);
                break;
        }
    }
}
