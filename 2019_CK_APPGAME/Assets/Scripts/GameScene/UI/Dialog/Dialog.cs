using SheetData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene
{
    public class Dialog : MonoBehaviour
    {
        public Text[] dialogTextUI;

        private Queue<DlgCmd> textQueue;

        private bool bTextFullLoad;

        private float elapsedTextTime;

        private void Awake()
        {
            bTextFullLoad = true;
            dialogTextUI = GetComponentsInChildren<Text>();
            MgrSingleton.GetuiMgrSingleton().textListQueueIndex = 0;
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
            if (bTextFullLoad && MgrSingleton.GetuiMgrSingleton().ScreenReaction())
            {
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(MgrSingleton.GetuiMgrSingleton().ScreenReaction());
            }
            dialogTextUI[0].text = MgrSingleton.GetuiMgrSingleton().textStringBuilder.ToString();
        }

        private void NextText()
        {
            bTextFullLoad = false;
            List<TextType> textListQueue = MgrSingleton.GetuiMgrSingleton().NextText();
            dialogTextUI[0].text = MgrSingleton.GetuiMgrSingleton().textStringBuilder.ToString();
            dialogTextUI[1].text = textListQueue[MgrSingleton.GetuiMgrSingleton().textListQueueIndex].TalkerName;
            textQueue = new Queue<DlgCmd>(textListQueue[MgrSingleton.GetuiMgrSingleton().textListQueueIndex++].mTextQueue);
            MgrSingleton.GetuiMgrSingleton().NextTextCount();
            TextDequeue();
        }

        private void PassText()
        {
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(true);
            }
            elapsedTextTime = 0f;
            MgrSingleton.GetuiMgrSingleton().bChAppend = false;
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
            if (!(MgrSingleton.GetuiMgrSingleton().textOutputTime <= elapsedTextTime) && MgrSingleton.GetuiMgrSingleton().bChAppend)
            {
                return;
            }
            TextDequeue();
            if (textQueue.Count == 0)
            {
                bTextFullLoad = true;
            }
        }

        private void TextDequeue()
        {
            elapsedTextTime = 0f;
            MgrSingleton.GetuiMgrSingleton().bChAppend = false;
            while (textQueue.Count > 0)
            {
                textQueue.Dequeue().CommandPerform(false);
                if (MgrSingleton.GetuiMgrSingleton().bChAppend)
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