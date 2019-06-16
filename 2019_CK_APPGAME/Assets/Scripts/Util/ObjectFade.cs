using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Util
{
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
        public static IEnumerator ObjectFadeOut(GameObject fadeObj, float totalTime = 1f)
        {
            Color tempColor = fadeObj.GetComponent<RawImage>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            GameScene.UIMgr.GetUIMgr().isTextCancel = true;
            while (tempColor.a > 0f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 0f, elapsedTime);
                fadeObj.GetComponent<RawImage>().color = tempColor;
                yield return null;
            }
            GameScene.UIMgr.GetUIMgr().NpcEntry(); //이 부분 추후 수정
            GameScene.UIMgr.GetUIMgr().isTextCancel = false;
            fadeObj.SetActive(false);
        }

        public static IEnumerator ObjectFadeOutChange(GameObject fadeObj, float totalTime = 1f)
        {
            Color tempColor = fadeObj.GetComponent<RawImage>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            while (tempColor.a > 0f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 0f, elapsedTime);
                fadeObj.GetComponent<RawImage>().color = tempColor;
                yield return null;
            }
            fadeObj.SetActive(false);
        }

        public static IEnumerator ObjectFadeIn(GameObject fadeObj, float totalTime = 1f)
        {
            fadeObj.SetActive(true);
            Color tempColor = fadeObj.GetComponent<RawImage>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            while (tempColor.a < 1f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 1f, elapsedTime);
                fadeObj.GetComponent<RawImage>().color = tempColor;
                yield return null;
            }
        }

        public static IEnumerator ObjectSpriteFadeOut(GameObject fadeObj, float totalTime = 1f)
        {
            Color tempColor = fadeObj.GetComponent<Image>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            while (tempColor.a > 0f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 0f, elapsedTime);
                fadeObj.GetComponent<Image>().color = tempColor;
                yield return null;
            }
            //fadeObj.SetActive(false);
        }

        public static IEnumerator ObjectSpriteFadeOutChange(GameObject fadeObj, float totalTime = 1f)
        {
            Color tempColor = fadeObj.GetComponent<Image>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            while (tempColor.a > 0f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 0f, elapsedTime);
                fadeObj.GetComponent<Image>().color = tempColor;
                yield return null;
            }
            //fadeObj.SetActive(false);
        }

        public static IEnumerator ObjectSpriteFadeIn(GameObject fadeObj, float totalTime = 1f)
        {
            fadeObj.SetActive(true);
            Color tempColor = fadeObj.GetComponent<Image>().color;
            float startColor = tempColor.a;
            float elapsedTime = 0f;
            while (tempColor.a < 1f)
            {
                elapsedTime += Time.deltaTime / totalTime;
                tempColor.a = Mathf.Lerp(startColor, 1f, elapsedTime);
                fadeObj.GetComponent<Image>().color = tempColor;
                yield return null;
            }
        }
    }
}