using UnityEngine;
using UnityEngine.UI;

public abstract class AAgentController
{
    public float specialCooldownTimer = 0f;
    public float specialCooldown;

    public virtual Sprite GetSprite()
    {
        return ResourcePaths.StandardSprite;
    }

    // TODO change sprites here
    public virtual void Init(Agent agent)
    {
    }

    public virtual void ProcessPrimary(GameObject go, Vector3 mousePos, float delta, bool inCover)
    {
    }

    public virtual bool ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        return false;
    }

    public virtual void ProcessDuringSpecial(GameObject go, Vector3 mousePos)
    {
    }
}