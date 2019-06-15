using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameScene;

namespace Util
{

    public class PreferencesTrigger : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Instantiate(Resources.Load<GameObject>("Prefebs/BlackBackGround"));
        }
    }

}