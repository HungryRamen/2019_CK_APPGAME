using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CreditTrigger : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [Serializable]
    public class MyEventType : UnityEvent { } //함수 받아오기

    public MyEventType OnEvent;

    private void Awake()
    {
        transform.SetParent(GameObject.FindWithTag("UIMgr").transform);
        transform.localPosition = Vector2.zero;
        transform.localScale = Vector2.one;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnEvent.Invoke();
    }

    private void Update()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 250.0f * Time.deltaTime);
        if (transform.localPosition.y > 4320.0f)
        {
            OnEvent.Invoke();
            OnEvent = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
