using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : AEquipment
{
    private GameObject bulletPrefab;

    public Pistol()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet1");
        if (bulletPrefab == null)
        {
            Debug.Log("bulletPrefab not found");
        }
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
