using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{

    public static class ObjectLerf
    {
        public static IEnumerator LocalLerpY(Transform startPos,float arriveY,float speed)
        {
            while (Mathf.Abs(arriveY - startPos.localPosition.y) > 0.2f)
            {
                startPos.localPosition = Vector3.Lerp(startPos.localPosition, new Vector3(startPos.localPosition.x, arriveY), speed * Time.deltaTime);
                yield return null; 
            }
            startPos.localPosition = new Vector3(startPos.localPosition.x, arriveY, startPos.localPosition.z);
        }

    }
}
