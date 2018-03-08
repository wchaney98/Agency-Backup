using UnityEngine;

public abstract class AAgentController
{
    // TODO change sprites here
    public virtual void Init(Agent agent)
    {
    }

    public virtual void ProcessPrimary(GameObject go, Vector3 mousePos)
    {
        if (go == null)
            return;
    }

    public virtual void ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        if (go == null)
            return;
    }
}