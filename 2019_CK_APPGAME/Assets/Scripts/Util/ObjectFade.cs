using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class ObjectFade
{
    public static IEnumerator FadeOut(RawImage fadeImg, float totalTime = 1f)
    {
        Color tempColor = fadeImg.color;
        float startColor = tempColor.a;
        float elapsedTime = 0f;
        while (tempColor.a > 0f)
        {
            elapsedTime += Time.deltaTime / totalTime;
            tempColor.a = Mathf.Lerp(startColor, 0f, elapsedTime);
            fadeImg.color = tempColor;
            yield return null;
        }
    }

    public static IEnumerator FadeIn(RawImage fadeImg, float totalTime = 1f)
    {
        Color tempColor = fadeImg.color;
        float startColor = tempColor.a;
        float elapsedTime = 0f;
        while (tempColor.a < 1f)
        {
            elapsedTime += Time.deltaTime / totalTime;
            tempColor.a = Mathf.Lerp(startColor, 1f, elapsedTime);
            fadeImg.color = tempColor;
            yield return null;
        }
    }
}
