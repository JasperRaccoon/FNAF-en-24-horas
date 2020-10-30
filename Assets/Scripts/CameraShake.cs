using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraShake
{
    static Transform trans;
    public static void CamShake(float duration, float magnitude, MonoBehaviour script)
    {
        Reset();
        script.StopCoroutine(Shake(duration, magnitude));
        script.StartCoroutine(Shake(duration, magnitude));
    }
    static IEnumerator Shake (float duration, float magnitude)
    {
        if (trans == null)
        {
            trans = Camera.main.transform;
        }
        trans.localPosition = new Vector3(0, 0, 0);
        Vector3 originalPos = trans.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude / 10;
            float y = Random.Range(-1, 1) * magnitude / 10;
            float z = Random.Range(-1, 1) * magnitude / 20;
            trans.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z); 
            elapsed += (Time.deltaTime / 3);
            magnitude -= (Time.deltaTime / 3);
            if (magnitude < 0)
            {
                magnitude = 0;
            }
            yield return null;
        }
        Reset();
    }
    public static void Reset()
    {
        Camera.main.transform.localPosition = Vector3.zero;
    }
}
