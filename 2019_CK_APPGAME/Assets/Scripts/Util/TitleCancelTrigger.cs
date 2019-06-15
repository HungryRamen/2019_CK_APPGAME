using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Util
{

    public class TitleCancelTrigger : MonoBehaviour, IPointerClickHandler
    {
        [Serializable]
        public class MyEventType : UnityEvent { } //함수 받아오기

        public MyEventType OnEvent;
        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                OnEvent.Invoke();
            }
        }
    }
}
