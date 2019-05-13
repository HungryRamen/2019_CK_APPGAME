using System.Collections;
using UnityEngine;
namespace Util
{
    public static class ObjectShake
    {
        public static bool isScreenShake = false;
        public static IEnumerator ScreenShake(Transform pos, float offSet)
        {
            isScreenShake = true;
            Vector2 tempPos = pos.position;
            bool bCheck = false;
            while (isScreenShake)
            {
                if (!bCheck)
                {
                    float num = Random.Range(0f, offSet + 1f);
                    float num2 = Random.Range(0f, offSet + 1f);
                    int num3 = Random.Range(0, 2);
                    int num4 = Random.Range(0, 2);
                    if (num3 == 0)
                    {
                        num *= -1f;
                    }
                    if (num4 == 0)
                    {
                        num2 *= -1f;
                    }
                    pos.position = new Vector2(tempPos.x + num, tempPos.y + num2);
                }
                else
                {
                    pos.position = tempPos;
                }
                bCheck = !bCheck;
                yield return new WaitWhile(() => !isScreenShake);
            }
            pos.position = tempPos;
        }

        public static IEnumerator Shake(Transform pos, float time, float offSet)
        {
            float elapsedTime = 0f;
            Vector2 tempPos = pos.position;
            bool bCheck = false;
            while (elapsedTime <= time)
            {
                elapsedTime += Time.deltaTime;
                if (!bCheck)
                {
                    float num = Random.Range(0f, offSet + 1f);
                    float num2 = Random.Range(0f, offSet + 1f);
                    int num3 = Random.Range(0, 2);
                    int num4 = Random.Range(0, 2);
                    if (num3 == 0)
                    {
                        num *= -1f;
                    }
                    if (num4 == 0)
                    {
                        num2 *= -1f;
                    }
                    pos.position = new Vector2(tempPos.x + num, tempPos.y + num2);
                }
                else
                {
                    pos.position = tempPos;
                }
                bCheck = !bCheck;
                yield return null;
            }
            pos.position = tempPos;
        }
    }
}