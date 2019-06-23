using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Util
{

    public static class ActionDelay
    {
        public static IEnumerator Delay(float delayTime,System.Action func)
        {
            yield return new WaitForSeconds(delayTime);
            func.Invoke();
        }
    }
}
