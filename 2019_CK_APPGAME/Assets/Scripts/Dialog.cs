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
        public GameObject TextObj;      //TextUI
        public GameObject TalkerTextObj; //TalkerTextUI;
        public GameObject NpcImageObjSong;  //송아연 이미지 오브젝트 후에 정리되면 LeftObj로
        public GameObject NpcImageObjJack;  //잭 이미지 오브젝트 후에 정리되면 RightObj로

        private List<TextType> TextListQueue; //하나의 장면의 대사를 관리하는 리스트
        private Queue<TextSet> TextQueue; //한대사의 문자를 관리하는 큐
        private TextSet TextTypeNow;   //현재 출력되고 있는 문자
        private TextSet TextTypeChange; //문자 속성이 바뀌었는지 확인
        private bool bTextFullLoad;     //대사가 모두 출력되었는가 판별
        private StringBuilder TextStringBuilder; //대사용 스트링빌더
        private float TextElapsedTime;      //문자출력 경과시간
        private int TextListQueueIndex;    //대사 출력 순서 Index
        private int TextIndex;            //텍스트 InsertIndex
        private string StrSizeText;       //RichText 텍스트 크기용 string
        private string StrColorText;      //RichText 텍스트 색상용 string
        void Awake()
        {
            StrSizeText = "</size>";
            StrColorText = "</color>";
            TextElapsedTime = 0.0f;
            TextListQueueIndex = 0;
            TextIndex = 0;
            bTextFullLoad = true;
            TextQueue = null;
            TextTypeNow = null;
            TextTypeChange = null;
            TextListQueue = null;
            TextStringBuilder = new StringBuilder();
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
            if (bTextFullLoad && ScreenReaction()) //텍스트가 전부 나왔는가? + 화면에 반응을 줬는가
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
            if (TextListQueue == null)
            {
                TextListQueue = new List<TextType>(DataSheetSet.TextDictionaryListQueue["C00_D00"]);
                TextListQueueIndex = 0;
            }
            TextStringBuilder.Clear();
            TextObj.GetComponent<Text>().text = TextStringBuilder.ToString();
            TextTypeChange = new TextSet();
            TalkerTextObj.GetComponent<Text>().text = TextListQueue[TextListQueueIndex].TalkerName;
            TextQueue = new Queue<TextSet>(TextListQueue[TextListQueueIndex++].mTextQueue);
            TextIndex = 0;
            TextTypeNow = TextQueue.Dequeue();
            NpcImageLoad(TextTypeNow.mCmdCharImg.ID, TextTypeNow.mCmdCharImg.State);
            if (TextListQueue.Count <= TextListQueueIndex)
            {
                TextListQueue = null;
            }
        }

        void PassText()  //대사스킵
        {
            while (TextQueue.Count > 0)
            {
                RichTextMgr();
                TextTypeNow = TextQueue.Dequeue();
            }
            RichTextMgr();
            NpcImageLoad(TextTypeNow.mCmdCharImg.ID, TextTypeNow.mCmdCharImg.State);
            TextElapsedTime = 0.0f;
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
                TextElapsedTime += Time.deltaTime;
                if (TextElapsedTime >= TextTypeNow.TextOutputTime)
                {
                    RichTextMgr();
                    if (TextQueue.Count == 0)
                    {
                        bTextFullLoad = true;
                        return;
                    }
                    TextTypeNow = TextQueue.Dequeue();
                    if (TextTypeNow.Ch == '\n')
                    {
                        RichTextMgr();
                        TextTypeNow = TextQueue.Dequeue();
                    }
                    NpcImageLoad(TextTypeNow.mCmdCharImg.ID, TextTypeNow.mCmdCharImg.State);
                    TextElapsedTime = 0.0f;
                }
            }
        }

        //RichText를 위한 TextQueue의문자 정리함수
        void RichTextMgr()
        {
            if (TextTypeNow.TextSize != TextTypeChange.TextSize)  //텍스트 사이즈가 바뀌었는가?
            {
                TextTypeChange.TextSize = TextTypeNow.TextSize;
                StringBuilder strSize = new StringBuilder("<size=");
                strSize.AppendFormat("{0}>", TextTypeNow.TextSize);
                RichTextIndexMgr(strSize.ToString(), StrSizeText);
                int EndIndex = TextStringBuilder.ToString().IndexOf(StrColorText, TextIndex);
                if (EndIndex >= 0)
                {
                    TextStringBuilder.Remove(EndIndex, StrColorText.Length);
                    TextStringBuilder.Append(StrColorText);
                }
                TextIndex += (TextStringBuilder.ToString().IndexOf(">", TextIndex) + 1) - TextIndex;
            }
            if (TextTypeNow.TextColor != TextTypeChange.TextColor) //텍스트 색상이 바뀌었는가?
            {
                TextTypeChange.TextColor = TextTypeNow.TextColor;
                StringBuilder strSize = new StringBuilder("<color=");
                strSize.AppendFormat("{0}>", TextTypeNow.TextColor);
                RichTextIndexMgr(strSize.ToString(), StrColorText);
                int EndIndex = TextStringBuilder.ToString().IndexOf(StrSizeText, TextIndex);
                if (EndIndex >= 0)
                {
                    TextStringBuilder.Remove(EndIndex, StrSizeText.Length);
                    TextStringBuilder.Append(StrSizeText);
                }
                TextIndex += (TextStringBuilder.ToString().IndexOf(">", TextIndex) + 1) - TextIndex;
            }
            TextStringBuilder.Insert(TextIndex++, TextTypeNow.Ch);
            TextObj.GetComponent<Text>().text = TextStringBuilder.ToString();
        }

        void RichTextIndexMgr(string strFormat, string str) //</>정리
        {
            int EndIndex = TextStringBuilder.ToString().IndexOf(str, TextIndex);
            if (EndIndex >= 0)
            {
                TextIndex += EndIndex + str.Length - TextIndex;
            }
            TextStringBuilder.Insert(TextIndex++, strFormat);
            TextStringBuilder.Append(str);
        }

        void NpcImageLoad(string id, int state)
        {
            if (id == "CH_02")
            {
                NpcImageObjSong.GetComponent<RawImage>().texture = Resources.Load(DataSheetSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                ObjectSetActive(NpcImageObjSong, true);
                ObjectSetActive(NpcImageObjJack, false);
            }
            else if (id == "CH_04")
            {
                NpcImageObjJack.GetComponent<RawImage>().texture = Resources.Load(DataSheetSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                ObjectSetActive(NpcImageObjJack, true);
                ObjectSetActive(NpcImageObjSong, false);
            }
        }

        void ObjectSetActive(GameObject obj, bool bOnOff)
        {
            obj.SetActive(bOnOff);
        }

        void VoiceOutput(string voice)
        {

        }
    }
}
