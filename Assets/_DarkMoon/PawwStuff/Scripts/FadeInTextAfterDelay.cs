using UnityEngine;
using System.Collections;

public class FadeTextInAfterDelay : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float delay = 5f;
    public float fadeDuration = 2f;

    void Start()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
