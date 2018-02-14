using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenShake
{
    static Transform camTransform;

    static float intensity = 0.7f;
    static float duration = 1f;

    static float timer;

    public static void Init()
    {
        camTransform = Camera.main.gameObject.transform;
    }

    public static IEnumerator ShakeRoutine(float intensity, float duration)
    {
        Debug.Log("yeet");
        Vector3 origPosition = camTransform.position;
        while (timer < duration)
        {
            camTransform.position = origPosition + Random.insideUnitSphere * intensity;
            timer += Time.deltaTime;
            yield return null;
        }
        camTransform.position = origPosition;
    }
}
