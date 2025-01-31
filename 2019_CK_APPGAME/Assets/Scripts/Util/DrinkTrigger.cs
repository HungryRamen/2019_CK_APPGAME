﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameScene;

namespace Util
{


    public class DrinkTrigger : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public ImageDrag imageDrag;

        private void Awake()
        {
            imageDrag = GameObject.FindWithTag("Drag").GetComponent<ImageDrag>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SoundMgr.SoundOn(SheetData.ESoundSet.DKButton);
                SoundMgr.playSoundDic[SheetData.ESoundSet.DKButton].states[0].setValue(1);
                SoundMgr.Release(SheetData.ESoundSet.DKButton);
                CursorImageData.SetCursor(name);
                imageDrag.ChangeImage(CursorImageData.cursorSpriteDic[CursorImageData.currentState], false);
            }

        }
        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SoundMgr.SoundOn(SheetData.ESoundSet.DKButton);
                SoundMgr.playSoundDic[SheetData.ESoundSet.DKButton].states[0].setValue(2);
                SoundMgr.Release(SheetData.ESoundSet.DKButton);
                UIMgr.GetUIMgr().DrinkButtonDragConfirmed();
                CursorImageData.SetCursor(CursorImageData.EMouseState.None);
                imageDrag.OffImage();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                SoundMgr.SoundOn(SheetData.ESoundSet.DKButton);
                SoundMgr.playSoundDic[SheetData.ESoundSet.DKButton].states[0].setValue(2);
                SoundMgr.Release(SheetData.ESoundSet.DKButton);
                CursorImageData.SetCursor(name);
                UIMgr.GetUIMgr().DrinkButtonConfirmed();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                SoundMgr.SoundOn(SheetData.ESoundSet.DKButton);
                SoundMgr.playSoundDic[SheetData.ESoundSet.DKButton].states[0].setValue(1);
                SoundMgr.Release(SheetData.ESoundSet.DKButton);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
