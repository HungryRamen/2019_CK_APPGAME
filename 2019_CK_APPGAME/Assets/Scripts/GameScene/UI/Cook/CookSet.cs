using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{

    public class CookSet : MonoBehaviour
    {
        private void Start()
        {
            UIMgr.GetUIMgr().CookChangeSelect("C1");
        }
    }
}
