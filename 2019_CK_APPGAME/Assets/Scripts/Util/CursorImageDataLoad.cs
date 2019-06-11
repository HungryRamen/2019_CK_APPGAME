using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class CursorImageDataLoad : MonoBehaviour
    {
        private void Awake()
        {
            CursorImageData.cursorImageDic.Add(CursorImageData.EMouseState.None, Resources.Load<Texture2D>("UI/Mouse/None"));
            CursorImageData.cursorImageDic.Add(CursorImageData.EMouseState.Down, Resources.Load<Texture2D>("UI/Mouse/Down"));
        }
    }
}
