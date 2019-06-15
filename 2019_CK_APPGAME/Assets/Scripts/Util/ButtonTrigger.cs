using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Util
{

    public class ButtonTrigger : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler,IPointerClickHandler
    {
        [Serializable]
        public class MyEventType : UnityEvent { } //함수 받아오기

        public MyEventType OnEvent;
        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundMgr.SoundOn(SheetData.ESoundType.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundType.PublicButton].states[0].setValue(0);
            SoundMgr.Release(SheetData.ESoundType.PublicButton);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SoundMgr.SoundOn(SheetData.ESoundType.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundType.PublicButton].states[0].setValue(1);
            SoundMgr.Release(SheetData.ESoundType.PublicButton);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SoundMgr.SoundOn(SheetData.ESoundType.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundType.PublicButton].states[0].setValue(2);
            SoundMgr.Release(SheetData.ESoundType.PublicButton);
            if(GetComponent<Button>() != null)
            {
                if(GetComponent<Button>().interactable)
                    OnEvent.Invoke();
            }
            else if(GetComponent<Button>() == null)
                OnEvent.Invoke();

        }
    }
}
