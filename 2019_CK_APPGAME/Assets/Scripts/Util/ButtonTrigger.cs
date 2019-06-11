using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Util
{

    public class ButtonTrigger : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler,IPointerClickHandler
    {
        [Serializable]
        public class MyEventType : UnityEvent { } //함수 받아오기

        public MyEventType OnEvent;
        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundMgr.BtState.setValue(0);
            SoundMgr.SoundOn();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundMgr.BtState.setValue(1);
            SoundMgr.SoundOn();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SoundMgr.BtState.setValue(2);
            SoundMgr.SoundOn();
            OnEvent.Invoke();
        }
    }
}
