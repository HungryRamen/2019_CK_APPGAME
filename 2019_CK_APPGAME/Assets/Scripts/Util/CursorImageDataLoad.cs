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
            CursorImageData.cursorTexture2DDic.Add(CursorImageData.EMouseState.None, Resources.Load<Texture2D>("UI/Mouse/None"));
            CursorImageData.cursorTexture2DDic.Add(CursorImageData.EMouseState.Down, Resources.Load<Texture2D>("UI/Mouse/Down"));


            CursorImageData.cursorSpriteDic.Add(CursorImageData.EMouseState.DK1, Resources.Load<Sprite>("UI/Dialog/Drink/DK1"));
            CursorImageData.cursorSpriteDic.Add(CursorImageData.EMouseState.DK2, Resources.Load<Sprite>("UI/Dialog/Drink/DK2"));
            CursorImageData.cursorSpriteDic.Add(CursorImageData.EMouseState.DK3, Resources.Load<Sprite>("UI/Dialog/Drink/DK3"));
            CursorImageData.cursorSpriteDic.Add(CursorImageData.EMouseState.DK4, Resources.Load<Sprite>("UI/Dialog/Drink/DK4"));
            CursorImageData.cursorSpriteDic.Add(CursorImageData.EMouseState.DK5, Resources.Load<Sprite>("UI/Dialog/Drink/DK5"));
        }
    }
}
