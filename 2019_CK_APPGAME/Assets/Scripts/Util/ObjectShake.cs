using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectShake
{
    public static IEnumerator Shake(Transform pos,float time, float offSet) //흔들릴 오브젝트의 transform 흔들리는시간 흔들리는 강도 
    {
        float elapsedTime = 0.0f;
        Vector2 tempPos = pos.position;
        bool bCheck = false;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            if (!bCheck)
            {
                float randX = Random.Range(0.0f, offSet + 1);
                float randY = Random.Range(0.0f, offSet + 1);
                int positiveX = Random.Range(0, 2);
                int positiveY = Random.Range(0, 2);
                if (positiveX == 0)
                {
                    randX *= -1;
                }
                if (positiveY == 0)
                {
                    randY *= -1;
                }
                pos.position = new Vector2(tempPos.x + randX, tempPos.y + randY);
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
