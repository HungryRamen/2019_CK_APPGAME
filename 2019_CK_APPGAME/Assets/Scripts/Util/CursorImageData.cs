using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class CursorImageData
    {
        public enum EMouseState
        {
            None,
            Down,
        }

        public static Dictionary<EMouseState, Texture2D> cursorImageDic = new Dictionary<EMouseState, Texture2D>();

        public static void SetCursor(EMouseState state)
        {
            Cursor.SetCursor(cursorImageDic[state], Vector2.zero, CursorMode.Auto);
        }
    }
}
