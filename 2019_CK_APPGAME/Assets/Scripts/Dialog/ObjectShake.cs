using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{

    public void StartShake(float time, float offSet)
    {
        StartCoroutine(Shake(time, offSet));
    }

    public IEnumerator Shake(float time, float offSet)
    {
        float elapsedTime = 0.0f;
        Vector2 tempPos = transform.position;
        bool bCheck = false;
        while (elapsedTime <= time)
        {
            elapsedTime += 0.1f;
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
                transform.position = new Vector2(tempPos.x + randX, tempPos.y + randY);
            }
            else
            {
                transform.position = tempPos;
            }
            bCheck = !bCheck;
            yield return new WaitForSeconds(0.1f);
        }
        transform.position = tempPos;
    }
}
