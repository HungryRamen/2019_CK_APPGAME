// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: UIMgr
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SheetData;
public class UIMgr : MonoBehaviour
{
    public int textIndex;

    public float textOutputTime;

    public bool bChAppend;

    public GameObject uiCook;

    public GameObject uiDialog;

    public GameObject btnCook;

    public GameObject imageNpcSong;

    public GameObject imageNpcJack;

    public StringBuilder textStringBuilder;

    private Dictionary<string, string> textTypeDictionary = new Dictionary<string, string>();

    private void Awake()
    {
        uiCook = GameObject.FindWithTag("CookUI");
        uiDialog = GameObject.FindWithTag("DlgUI");
        btnCook = GameObject.FindWithTag("CookBtn");
        uiCook.SetActive(!uiCook.activeSelf);
        btnCook.SetActive(!btnCook.activeSelf);
        textStringBuilder = new StringBuilder();
        textIndex = 0;
        textOutputTime = 0f;
        bChAppend = false;
        textTypeDictionary.Add("color", "size");
        textTypeDictionary.Add("size", "color");
    }

    public void ChangeUI()
    {
        if (uiCook.activeSelf)
        {
            uiDialog.SetActive(!uiDialog.activeSelf);
            uiCook.SetActive(!uiCook.activeSelf);
            btnCook.SetActive(!btnCook.activeSelf);
        }
        else
        {
            uiDialog.SetActive(!uiDialog.activeSelf);
            uiCook.SetActive(!uiCook.activeSelf);
        }
    }

    public void NpcImageLoad(string id, int state)
    {
        if (id == "CH_02")
        {
            imageNpcSong.GetComponent<RawImage>().texture = (Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture);
            imageNpcSong.SetActive(value: true);
            imageNpcJack.SetActive(value: false);
        }
        else if (id == "CH_04")
        {
            imageNpcJack.GetComponent<RawImage>().texture = (Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture);
            imageNpcJack.SetActive(value: true);
            imageNpcSong.SetActive(value: false);
        }
    }

    public void ScreenShake(float shakeTime, float shakeOffSet)
    {
        StartCoroutine(ObjectShake.Shake(uiDialog.transform, shakeTime, shakeOffSet));
    }

    public void RichTextEditor(string type, string value)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("<{0}={1}>", type, value);
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.AppendFormat("</{0}>", type);
        int num = textStringBuilder.ToString().IndexOf(stringBuilder2.ToString(), textIndex);
        if (num >= 0)
        {
            textIndex += num + stringBuilder2.Length - textIndex;
        }
        textStringBuilder.Insert(textIndex++, stringBuilder);
        textStringBuilder.Append(stringBuilder2);
        StringBuilder stringBuilder3 = new StringBuilder();
        stringBuilder3.AppendFormat("</{0}>", textTypeDictionary[type]);
        num = textStringBuilder.ToString().IndexOf(stringBuilder3.ToString(), textIndex);
        if (num >= 0)
        {
            textStringBuilder.Remove(num, stringBuilder3.Length);
            textStringBuilder.Append(stringBuilder3);
        }
        textIndex += textStringBuilder.ToString().IndexOf(">", textIndex) + 1 - textIndex;
    }

    public void ChAppend(char ch)
    {
        textStringBuilder.Insert(textIndex++, ch);
        bChAppend = true;
    }
}
