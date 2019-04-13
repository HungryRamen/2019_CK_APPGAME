using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using SheetData;

namespace DialogSystem
{
    public class Dialog : MonoBehaviour
    {
        public Text[] dialogTextUI;      //TextUI
        public GameObject objNpcImageSong;  //송아연 이미지 오브젝트 후에 정리되면 LeftObj로
        public GameObject objNpcImageJack;  //잭 이미지 오브젝트 후에 정리되면 RightObj로
        public GameObject objUIDlg;      //씬전환, 흔들기용 부모오브젝트
        public GameObject objCookBtn;   //요리버튼
        public int textListQueueIndex;    //대사 출력 순서 Index

        private List<TextType> textListQueue = null; //하나의 장면의 대사를 관리하는 리스트
        private Queue<TextSet> textQueue = null; //한대사의 문자를 관리하는 큐
        private TextSet textTypeNow = null;   //현재 출력되고 있는 문자
        private TextSet textTypeChange = null; //문자 속성이 바뀌었는지 확인
        private bool bTextFullLoad;     //대사가 모두 출력되었는가 판별
        private StringBuilder textStringBuilder; //대사용 스트링빌더
        private float textElapsedTime;      //문자출력 경과시간
        private int textIndex;            //텍스트 InsertIndex
        private string strSizeText;       //RichText 텍스트 크기용 string
        private string strColorText;      //RichText 텍스트 색상용 string
        void Awake()
        {
            strSizeText = "</size>";
            strColorText = "</color>";
            textElapsedTime = 0.0f;
            textListQueueIndex = 0;
            textIndex = 0;
            bTextFullLoad = true;
            textStringBuilder = new StringBuilder();
            objUIDlg = transform.parent.gameObject;
            dialogTextUI = GetComponentsInChildren<Text>();
            objCookBtn = GameObject.FindWithTag("CookBtn");
            objCookBtn.SetActive(!objCookBtn.activeSelf);
        }

        private void Start()
        {
            NextText();
        }

        // Update is called once per frame
        void Update()
        {
            DialogOutput();
        }

        void DialogOutput()   //다이얼로그 출력
        {
            if (bTextFullLoad && ScreenReaction() && !objCookBtn.activeSelf) //텍스트가 전부 나왔는가? + 화면에 반응을 줬는가 + 씬전환 여부
            {
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(ScreenReaction());
            }
        }

        bool ScreenReaction()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                return true;
            }
            return false;
        }

        void NextText() //다음대사
        {
            bTextFullLoad = false;
            if (textListQueue == null)
            {
                textListQueue = new List<TextType>(DataSheetSet.TextDictionary["C00_D00"]);
                textListQueueIndex = 0;
                NpcImageLoad(textListQueue[textListQueueIndex].CharID, 0);
            }
            textStringBuilder.Clear();
            dialogTextUI[0].text = textStringBuilder.ToString();
            textTypeChange = new TextSet();
            dialogTextUI[1].text = textListQueue[textListQueueIndex].TalkerName;
            textQueue = new Queue<TextSet>(textListQueue[textListQueueIndex++].mTextQueue);
            textIndex = 0;
            textTypeNow = textQueue.Dequeue();
            if (textListQueue.Count <= textListQueueIndex)
            {
                textListQueue = null;
            }
        }

        void PassText()  //대사스킵
        {
            while (textQueue.Count > 0)
            {
                RichTextMgr();
                textTypeNow = textQueue.Dequeue();
            }
            RichTextMgr();
            CookBtnActive(textTypeNow.IsOrderButtonEnable);
            NpcImageLoad(textTypeNow.mCmdCharImg.ID, textTypeNow.mCmdCharImg.State);
            textElapsedTime = 0.0f;
            bTextFullLoad = true;
        }

        void TextOutput(bool bPass) //문자출력
        {
            if (bPass)   //대사 스킵여부
            {
                PassText();
            }
            else
            {
                textElapsedTime += Time.deltaTime;
                if (textElapsedTime >= textTypeNow.TextOutputTime)
                {
                    RichTextMgr();
                    ScreenShake(textTypeNow);
                    CookBtnActive(textTypeNow.IsOrderButtonEnable);
                    NpcImageLoad(textTypeNow.mCmdCharImg.ID, textTypeNow.mCmdCharImg.State);
                    if (textQueue.Count == 0)
                    {
                        bTextFullLoad = true;
                        return;
                    }
                    textTypeNow = textQueue.Dequeue();
                    textElapsedTime = 0.0f;
                }
            }
        }

        //RichText를 위한 TextQueue의문자 정리함수
        void RichTextMgr()
        {
            if (textTypeNow.TextSize != textTypeChange.TextSize)  //텍스트 사이즈가 바뀌었는가?
            {
                textTypeChange.TextSize = textTypeNow.TextSize;
                StringBuilder strSize = new StringBuilder("<size=");
                strSize.AppendFormat("{0}>", textTypeNow.TextSize);
                RichTextIndexMgr(strSize.ToString(), strSizeText);
                int EndIndex = textStringBuilder.ToString().IndexOf(strColorText, textIndex);
                if (EndIndex >= 0)
                {
                    textStringBuilder.Remove(EndIndex, strColorText.Length);
                    textStringBuilder.Append(strColorText);
                }
                textIndex += (textStringBuilder.ToString().IndexOf(">", textIndex) + 1) - textIndex;
            }
            if (textTypeNow.TextColor != textTypeChange.TextColor) //텍스트 색상이 바뀌었는가?
            {
                textTypeChange.TextColor = textTypeNow.TextColor;
                StringBuilder strSize = new StringBuilder("<color=");
                strSize.AppendFormat("{0}>", textTypeNow.TextColor);
                RichTextIndexMgr(strSize.ToString(), strColorText);
                int EndIndex = textStringBuilder.ToString().IndexOf(strSizeText, textIndex);
                if (EndIndex >= 0)
                {
                    textStringBuilder.Remove(EndIndex, strSizeText.Length);
                    textStringBuilder.Append(strSizeText);
                }
                textIndex += (textStringBuilder.ToString().IndexOf(">", textIndex) + 1) - textIndex;
            }
            textStringBuilder.Insert(textIndex++, textTypeNow.Ch);
            dialogTextUI[0].text = textStringBuilder.ToString();
        }

        void ScreenShake(TextSet textSet)
        {
            if (textSet.mCmdScreenShake.bOneTime)
            {
                StartCoroutine(ObjectShake.Shake(objUIDlg.transform, textSet.mCmdScreenShake.ScreenShakeTime, textSet.mCmdScreenShake.ScreenShakeOffSet));
            }
        }

        void CookBtnActive(bool bEnable)
        {
            objCookBtn.SetActive(bEnable);
        }

        void RichTextIndexMgr(string strFormat, string str) //</>정리
        {
            int EndIndex = textStringBuilder.ToString().IndexOf(str, textIndex);
            if (EndIndex >= 0)
            {
                textIndex += EndIndex + str.Length - textIndex;
            }
            textStringBuilder.Insert(textIndex++, strFormat);
            textStringBuilder.Append(str);
        }

        void NpcImageLoad(string id, int state)
        {
            if (id == "CH_02")
            {
                objNpcImageSong.GetComponent<RawImage>().texture = Resources.Load(DataSheetSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                ObjectSetActive(objNpcImageSong, true);
                ObjectSetActive(objNpcImageJack, false);
            }
            else if (id == "CH_04")
            {
                objNpcImageJack.GetComponent<RawImage>().texture = Resources.Load(DataSheetSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                ObjectSetActive(objNpcImageJack, true);
                ObjectSetActive(objNpcImageSong, false);
            }
        }

        void ObjectSetActive(GameObject obj, bool bOnOff) //이건 지우자 쓸모없는 래퍼코드
        {
            obj.SetActive(bOnOff);
        }

        void VoiceOutput(string voice)
        {

        }
    }
}
