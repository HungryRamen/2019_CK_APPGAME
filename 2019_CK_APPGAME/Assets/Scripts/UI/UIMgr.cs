using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public GameObject cookUI;
    public GameObject dialogUI;
    public GameObject objCookBtn;
    private void Awake()
    {
        cookUI = GameObject.FindWithTag("CookUI");
        dialogUI = GameObject.FindWithTag("DlgUI");
        objCookBtn = GameObject.FindWithTag("CookBtn");
        cookUI.SetActive(!cookUI.activeSelf);
    }

    public void ChangeUI()
    {
        if(cookUI.activeSelf)
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
            cookUI.SetActive(!cookUI.activeSelf);
            objCookBtn.SetActive(!objCookBtn.activeSelf);
        }
        else
        {
            dialogUI.SetActive(!dialogUI.activeSelf);
            cookUI.SetActive(!cookUI.activeSelf);
        }
    }
}
