using System;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleSize
{
    SMALL,
    MEDIUM,
    BIG
}

public static class ParticleManager
{
    public static GameObject BigLaserExplosion = Resources.Load<GameObject>("Prefabs/World/Particle FX/ExplosionBig");
    public static GameObject SmallLaserExplosion = Resources.Load<GameObject>("Prefabs/World/Particle FX/ExplosionSmall");

    public static GameObject SmallSparks = Resources.Load<GameObject>("Prefabs/World/Particle FX/SparksSmall");
    public static GameObject SmallSparksBlue = Resources.Load<GameObject>("Prefabs/World/Particle FX/SparksBlueSmall");

    public static void SpawnLaserExplosionAt(ParticleSize size, Vector2 position)
    {
        GameObject boom;
        switch (size)
        {
            case ParticleSize.SMALL:
                boom = GameObject.Instantiate(SmallLaserExplosion, position, Quaternion.identity);
                GameObject.Destroy(boom, 3f);
                break;
            case ParticleSize.MEDIUM:
                break;
            case ParticleSize.BIG:
                boom = GameObject.Instantiate(BigLaserExplosion, position, Quaternion.identity);
                GameObject.Destroy(boom, 3f);
                break;
        }
    }

    public static void SpawnSparksAt(ParticleSize size, Vector2 position, bool blue = false)
    {
        GameObject boom;
        switch (size)
        {
            case ParticleSize.SMALL:
                if (blue)
                    boom = GameObject.Instantiate(SmallSparksBlue, position, Quaternion.identity);
                else
                    boom = GameObject.Instantiate(SmallSparks, position, Quaternion.identity);

                GameObject.Destroy(boom, 3f);
                break;
        }
    }
}
