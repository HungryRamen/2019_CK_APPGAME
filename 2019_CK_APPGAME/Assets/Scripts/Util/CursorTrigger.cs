using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Util
{

    public class CursorTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            CursorImageData.SetCursor(CursorImageData.EMouseState.Down);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CursorImageData.SetCursor(CursorImageData.EMouseState.None);
        }
    }
}
