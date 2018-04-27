using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBlock : MonoBehaviour
{
    RaycastHit2D[] hitPoint = new RaycastHit2D[10];


    public void ManualOnDestroy()
    {
        EventManager.Instance.TriggerEvent("ScreenShake", new EventParam(null, 0.08f, 0.35f));
        Instantiate(LevelBuilder.floorPrefab, transform.position, Quaternion.identity, LevelBuilder.parent.transform);

        CheckForCoverArea(Vector2.up);
        CheckForCoverArea(Vector2.down);
        CheckForCoverArea(Vector2.right);
        CheckForCoverArea(Vector2.left);

        ParticleManager.SpawnLaserExplosionAt(ParticleType.BIG, transform.position);
        Destroy(gameObject);
    }

    private void CheckForCoverArea(Vector3 direction)
    {
        hitPoint = Physics2D.RaycastAll(transform.position, direction, 0.64f);
        foreach (RaycastHit2D hit in hitPoint)
        {
            if (hit.collider.gameObject.tag == "CoverArea")
            {
                Destroy(hit.collider.gameObject);
                Instantiate(LevelBuilder.floorPrefab, transform.position + direction * 0.64f, Quaternion.identity, LevelBuilder.parent.transform);
                break;
            }
        }
    }
}
