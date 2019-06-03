using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Util
{

    public class ButtonTrigger : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler , IPointerExitHandler
    {
        [Serializable]
        public class MyEventType : UnityEvent { }

        public MyEventType OnEvent;
        public bool onCheck = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundMgr.BtState.setValue(0);
            SoundMgr.SoundOn();
            onCheck = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundMgr.BtState.setValue(1);
            SoundMgr.SoundOn();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (onCheck)
            {
                SoundMgr.BtState.setValue(2);
                SoundMgr.SoundOn();
                OnEvent.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onCheck = false;
        }
    }
}
