// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DialogSystem.Dialog
using SheetData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem
{
    public class Dialog : MonoBehaviour
    {
        public Text[] dialogTextUI;

        public int textListQueueIndex;

        public UIMgr uiManager;

        private List<TextType> textListQueue;

        private Queue<DlgCmd> textQueue;

        private bool bTextFullLoad;

        private float elapsedTextTime;

        private void Awake()
        {
            uiManager = GameObject.FindWithTag("UIMgr").GetComponent<UIMgr>();
            textListQueueIndex = 0;
            bTextFullLoad = true;
            dialogTextUI = GetComponentsInChildren<Text>();
            elapsedTextTime = 0f;
        }

        private void Start()
        {
            NextText();
        }

        private void Update()
        {
            DialogOutput();
        }

        private void DialogOutput()
        {
            if (bTextFullLoad && ScreenReaction() && !uiManager.btnCook.activeSelf)
            {
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(ScreenReaction());
            }
            dialogTextUI[0].text = uiManager.textStringBuilder.ToString();
        }

        private bool ScreenReaction()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                return true;
            }
            return false;
        }

        private void NextText()
        {
            bTextFullLoad = false;
            if (textListQueue == null)
            {
                textListQueue = new List<TextType>(DataJsonSet.TextDictionary["C00_D00"]);
                textListQueueIndex = 0;
            }
            uiManager.textStringBuilder.Clear();
            uiManager.textIndex = 0;
            dialogTextUI[0].text = uiManager.textStringBuilder.ToString();
            dialogTextUI[1].text = textListQueue[textListQueueIndex].TalkerName;
            textQueue = new Queue<DlgCmd>(textListQueue[textListQueueIndex++].mTextQueue);
            if (textListQueue.Count <= textListQueueIndex)
            {
                textListQueue = null;
            }
        }

        private void PassText()
        {
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(bPass: true);
            }
            elapsedTextTime = 0f;
            uiManager.bChAppend = false;
            bTextFullLoad = true;
        }

        private void TextOutput(bool bPass)
        {
            if (bPass)
            {
                PassText();
                return;
            }
            elapsedTextTime += Time.deltaTime;
            if (!(uiManager.textOutputTime <= elapsedTextTime) && uiManager.bChAppend)
            {
                return;
            }
            elapsedTextTime = 0f;
            uiManager.bChAppend = true;
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(bPass: false);
                if (uiManager.bChAppend)
                {
                    break;
                }
            }
            if (textQueue.Count == 0)
            {
                bTextFullLoad = true;
            }
        }

        private void VoiceOutput(string voice)
        {
        }
    }
}
