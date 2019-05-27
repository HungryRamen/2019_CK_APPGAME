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
            ButtonSoundMgr.BtState.setValue(0);
            ButtonSoundMgr.SoundOn();
            onCheck = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonSoundMgr.BtState.setValue(1);
            ButtonSoundMgr.SoundOn();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (onCheck)
            {
                ButtonSoundMgr.BtState.setValue(2);
                ButtonSoundMgr.SoundOn();
                OnEvent.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onCheck = false;
        }
    }
}
