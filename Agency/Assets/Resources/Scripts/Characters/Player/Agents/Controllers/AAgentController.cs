using UnityEngine;

public abstract class AAgentController
{
    public virtual void Init(Agent agent)
    {
    }

    public virtual void ProcessPrimary(GameObject go, Vector3 mousePos)
    {
    }

    public virtual void ProcessSpecial(GameObject go, Vector3 mousePos)
    {
    }
}