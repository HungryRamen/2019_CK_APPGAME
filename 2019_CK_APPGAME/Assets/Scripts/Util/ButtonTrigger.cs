using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Util
{

    public class ButtonTrigger : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler,IPointerClickHandler,IPointerExitHandler,IPointerUpHandler
    {
        [Serializable]
        public class MyEventType : UnityEvent { } //함수 받아오기

        public MyEventType OnEvent;
        private Image targetImage;
        private Sprite DefualtSprite;
        public Sprite targetGraphic;
        public Sprite HighligtedSprite;
        public Sprite PressedSprite;
        public Sprite DisabledSprite;
        public bool interactable;
        public bool isOnClick;

        private void Awake()
        {
            interactable = true;
            isOnClick = false;
            targetImage = GetComponent<Image>();
            if (targetImage == null)
                return;
            targetGraphic = targetImage.sprite;
            DefualtSprite = targetImage.sprite;
        }

        public void InteractableOn()
        {
            interactable = true;
            ChangeSprite(DefualtSprite);
        }

        public void InteractableOff()
        {
            interactable = false;
            ChangeSprite(DisabledSprite);
        }

        public void ChangeSprite(Sprite sprite)
        {
            if (sprite == null || targetGraphic == sprite)
                return;
            targetGraphic = sprite;
            targetImage.sprite = targetGraphic;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!interactable)
                return;
            ChangeSprite(HighligtedSprite);
            SoundMgr.SoundOn(SheetData.ESoundSet.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundSet.PublicButton].states[0].setValue(0);
            SoundMgr.Release(SheetData.ESoundSet.PublicButton);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable)
                return;
            ChangeSprite(PressedSprite);
            SoundMgr.SoundOn(SheetData.ESoundSet.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundSet.PublicButton].states[0].setValue(1);
            SoundMgr.Release(SheetData.ESoundSet.PublicButton);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;
            SoundMgr.SoundOn(SheetData.ESoundSet.PublicButton);
            SoundMgr.playSoundDic[SheetData.ESoundSet.PublicButton].states[0].setValue(2);
            SoundMgr.Release(SheetData.ESoundSet.PublicButton);
            isOnClick = true;
            if(GetComponent<Button>() != null)
            {
                if(GetComponent<Button>().interactable)
                    OnEvent.Invoke();
            }
            else if(GetComponent<Button>() == null)
                OnEvent.Invoke();

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable)
                return;
            ChangeSprite(DefualtSprite);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable)
                return;
            ChangeSprite(DefualtSprite);
        }
    }
}
