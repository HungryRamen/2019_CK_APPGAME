using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Util
{
    public class CharTrigger : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
    {
        public bool isCharOn = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            isCharOn = true;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            isCharOn = false;
        }
    }

}