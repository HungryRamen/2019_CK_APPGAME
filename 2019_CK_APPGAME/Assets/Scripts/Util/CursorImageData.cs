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
            DK1,
            DK2,
            DK3,
            DK4,
            DK5,
        }

        public static EMouseState currentState;
        public static Dictionary<EMouseState, Texture2D> cursorTexture2DDic = new Dictionary<EMouseState, Texture2D>();
        public static Dictionary<EMouseState, Sprite> cursorSpriteDic = new Dictionary<EMouseState, Sprite>();
        public static void SetCursor(EMouseState state)
        {
            currentState = state;
            Cursor.SetCursor(cursorTexture2DDic[currentState], Vector2.zero, CursorMode.Auto);
        }

        public static string GetCursor()
        {
            string temp = "DK1";
            if (currentState == EMouseState.DK2)
                temp = "DK2";
            else if (currentState == EMouseState.DK3)
                temp = "DK3";
            else if (currentState == EMouseState.DK4)
                temp = "DK4";
            else if (currentState == EMouseState.DK5)
                temp = "DK5";
            return temp;
        }

        public static void SetCursor(string state)
        {
            currentState = EMouseState.None;
            if (state == "DK1")
                currentState = EMouseState.DK1;
            else if (state == "DK2")
                currentState = EMouseState.DK2;
            else if (state == "DK3")
                currentState = EMouseState.DK3;
            else if (state == "DK4")
                currentState = EMouseState.DK4;
            else if (state == "DK5")
                currentState = EMouseState.DK5;
        }
    }
}
