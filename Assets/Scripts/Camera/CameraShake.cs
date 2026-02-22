using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private CameraController cameraController;
    private Coroutine shakeRoutine;

    void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraController.SetShakeOffset(new Vector3(x, y, 0f));

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraController.SetShakeOffset(Vector3.zero);
        shakeRoutine = null;
    }
}