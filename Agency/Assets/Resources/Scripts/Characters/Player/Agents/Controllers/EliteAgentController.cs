using UnityEngine;

public class EliteAgentController : AAgentController
{
    public override void ProcessPrimary(GameObject go, Vector3 mousePos, float delta, bool inCover)
    {
    }

    public override bool ProcessSpecial(GameObject go, Vector3 mousePos)
    {
        return false;
    }
}