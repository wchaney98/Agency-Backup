using System;
using System.Collections.Generic;
using UnityEngine;

class MouseRaycaster : SingletonBehavior<MouseRaycaster>
{
    const int raycastAmount = 10;
    RaycastHit2D[] result = new RaycastHit2D[10];
    int currHitCount = 0;

    public List<GameObject> GetObjectsMouseIsOver()
    {
        List<GameObject> go = new List<GameObject>();
        for (int i = 0; i < currHitCount; i++)
        {
            if (result[i].collider.gameObject != null)
            {
                go.Add(result[i].collider.gameObject);
            }
        }
        return go;
    }

    private void FixedUpdate()
    {
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currHitCount = Physics2D.RaycastNonAlloc(origin, Vector2.zero, result, 200);

        //Debug.Log(currHitCount);

        //for (int i = 0; i < currHitCount; i++)
        //{
        //    Debug.Log("Hit: " + result[i].collider.gameObject.name);
        //}
    }
}
