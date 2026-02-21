using System.Collections;
using UnityEngine;

public class InvincibilityFlash : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private Coroutine flashCoroutine;

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartFlash(float duration)
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine(duration));
    }

    public void StopFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        foreach (var sr in spriteRenderers)
            sr.enabled = true;
    }

    private IEnumerator FlashRoutine(float duration)
    {
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < duration)
        {
            visible = !visible;
            foreach (var sr in spriteRenderers)
                sr.enabled = visible;

            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        // Restore visibility
        foreach (var sr in spriteRenderers)
            sr.enabled = true;

        flashCoroutine = null;
    }
}
