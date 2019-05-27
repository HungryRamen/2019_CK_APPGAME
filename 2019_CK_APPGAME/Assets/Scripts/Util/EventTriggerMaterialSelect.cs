using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameScene;
namespace Util
{
    public class EventTriggerMaterialSelect : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
    {
        public Image mainSprite;
        public Transform materailTag;
        public ImageDrag imageDrag;
        public Sprite tempChange;

        public void OnPointerClick(PointerEventData data)
        {
            if (data.button == PointerEventData.InputButton.Right)
            {
                mainSprite.sprite = null;
                UIMgr.GetUIMgr().MaterialOff(materailTag.tag);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (imageDrag.enabled)
            {
                tempChange = mainSprite.sprite;
                mainSprite.sprite = imageDrag.GetImage();
                materailTag.gameObject.SetActive(true);
                mainSprite.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                UIMgr.GetUIMgr().MaterialEnterOn(materailTag.tag);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (imageDrag.enabled)
            {
                if(tempChange == null)
                    materailTag.gameObject.SetActive(false);

                mainSprite.sprite = tempChange;
                mainSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                UIMgr.GetUIMgr().MaterialEnterOff(materailTag.tag);
            }
        }
    }
}
