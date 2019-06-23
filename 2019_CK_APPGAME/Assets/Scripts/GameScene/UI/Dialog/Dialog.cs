using SheetData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogCommand;
using System.Collections;

namespace GameScene
{
    public class Dialog : MonoBehaviour
    {
        public Text[] dialogTextUI;

        private Queue<DlgCmd> textQueue;

        private bool bTextFullLoad;

        private float elapsedTextTime;

        private int exceptionIndex;

        private float elapsedDelayTime;

        public float elapsedMaxTime;

        private void Awake()
        {
            bTextFullLoad = true;
            dialogTextUI = GetComponentsInChildren<Text>();
            elapsedTextTime = 0f;
            elapsedMaxTime = 0.8f;
            elapsedDelayTime = elapsedMaxTime;
        }

        private void Start()
        {
            //NextText();
        }

        private void Update()
        {
            DialogOutput();
        }

        private void DialogOutput()
        {
            if (bTextFullLoad && UIMgr.GetUIMgr().ScreenReaction() && elapsedDelayTime > elapsedMaxTime && UIMgr.GetUIMgr().statusCoroutine == null)
            {
                UIMgr.GetUIMgr().LogTextAppend(dialogTextUI[1].text, UIMgr.GetUIMgr().logStringBuilder.ToString());
                Util.SoundMgr.SoundOnRelease(ESoundSet.Next);
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(UIMgr.GetUIMgr().ScreenReaction());
            }
            dialogTextUI[0].text = UIMgr.GetUIMgr().textStringBuilder.ToString();
            elapsedDelayTime += Time.deltaTime;
        }
        private void NextText()
        {
            bTextFullLoad = false;
            TextStackType textListQueue = UIMgr.GetUIMgr().NextText();
            exceptionIndex = textListQueue.TextTypeIndex;
            dialogTextUI[0].text = UIMgr.GetUIMgr().textStringBuilder.ToString();
            dialogTextUI[1].text = textListQueue.textTypeList[textListQueue.TextTypeIndex].TalkerName;
            //textListQueue.textTypeList[textListQueue.TextTypeIndex].
            UIMgr.GetUIMgr().NpcTalkCheck(textListQueue.textTypeList[textListQueue.TextTypeIndex].TalkerName);
            textQueue = new Queue<DlgCmd>(textListQueue.textTypeList[textListQueue.TextTypeIndex++].textQueue);
            //UIMgr.GetUIMgr().NextTextCount();
            TextDequeue();
        }

        private void PassText()
        {
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(true);
            }
            elapsedTextTime = 0f;
            UIMgr.GetUIMgr().bChAppend = false;
            bTextFullLoad = true;
            elapsedDelayTime = 0.0f;
        }

        private void TextOutput(bool bPass)
        {
            try
            {
                if (UIMgr.GetUIMgr().uiLog.activeSelf == true || UIMgr.GetUIMgr().isTextCancel == true)  //로그창을 보고있으면 멈춘다.
                    return;
                if (bPass)
                {
                    PassText();
                    return;
                }
                elapsedTextTime += Time.deltaTime;
                if (!(UIMgr.GetUIMgr().textOutputTime <= elapsedTextTime) && UIMgr.GetUIMgr().bChAppend)
                {
                    return;
                }
                TextDequeue();
                if (textQueue.Count == 0)
                {
                    bTextFullLoad = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogFormat("DialogID : {0}\nRelativeIndex : {1}\nException : {2}", UIMgr.GetUIMgr().nowEvent.DialogID, exceptionIndex, e);
            }
        }

        private void TextDequeue()
        {
            elapsedTextTime = 0f;
            UIMgr.GetUIMgr().bChAppend = false;
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(false);
                if (UIMgr.GetUIMgr().bChAppend || UIMgr.GetUIMgr().isTextCancel)
                {
                    break;
                }
            }
        }

        private void VoiceOutput(string voice)
        {
        }
    }
}