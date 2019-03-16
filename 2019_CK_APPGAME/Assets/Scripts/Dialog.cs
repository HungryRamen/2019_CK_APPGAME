using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace DialogSystem
{
    public class TextType//Text Queu 구현용
    {
        private char mCh;          //문자
        private float mTextOutputTime; //문자출력속도

        public TextType(char ch, float textOutputTime)
        {
            mCh = ch;
            mTextOutputTime = textOutputTime;
        }

        public char Ch { get => mCh; set => mCh = value; }
        public float TextOutputTime { get => mTextOutputTime; set => mTextOutputTime = value; }
    }
    public class Dialog : MonoBehaviour
    {
        public GameObject TextObj;      //TextUI
        private Dictionary<string, List<Queue<TextType>>> mTextDictionaryListQueue; //모든장면을 관리하는 딕셔너리
        private List<Queue<TextType>> mTextListQueue; //하나의 장면의 대사를 관리하는 리스트
        private Queue<TextType> TextQueue; //한대사의 문자를 관리하는 큐
        private TextType TextTypeNow;   //현재 출력되고 있는 문자
        private bool bTextFullLoad;     //대사가 모두 출력되었는가 판별
        private StringBuilder TextStringBuilder; //대사용 스트링빌더
        private float TextElapsedTime;      //문자출력 경과시간
        private int TextListQueueIndex;    //대사 출력 순서 Index;
        void Awake()
        {
            TextElapsedTime = 0.0f;
            TextListQueueIndex = 0;
            bTextFullLoad = true;
            TextQueue = null;
            TextTypeNow = null;
            mTextDictionaryListQueue = new Dictionary<string, List<Queue<TextType>>>();
            mTextListQueue = new List<Queue<TextType>>();
            TextStringBuilder = new StringBuilder();
            Test();
        }

        void Test()
        {
            Queue<TextType> textQueue = new Queue<TextType>();
            textQueue.Enqueue(new TextType('안', 0.0f));
            textQueue.Enqueue(new TextType('녕', 1.0f));
            textQueue.Enqueue(new TextType('하', 1.0f));
            textQueue.Enqueue(new TextType('세', 1.0f));
            textQueue.Enqueue(new TextType('요', 1.0f));
            mTextListQueue.Add(textQueue);
        }

        // Update is called once per frame
        void Update()
        {
            DialogOutput();
        }

        void DialogOutput()
        {
            if (bTextFullLoad && ScreenReaction())
            {
                bTextFullLoad = false;
                NextText();
            }
            else if (!bTextFullLoad)
            {
                TextOutput(ScreenReaction());
            }
            TextObj.GetComponent<Text>().text = TextStringBuilder.ToString();
        }

        bool ScreenReaction()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                return true;
            }
            return false;
        }

        void NextText()
        {
            if (mTextListQueue.Count > TextListQueueIndex)
            {
                TextQueue = mTextListQueue[TextListQueueIndex++];
                TextTypeNow = TextQueue.Dequeue();
            }
            //else TextListQueueIndex가 끝나면 다음 것을 불러오자 mTextDictionaryListQueue는 sheetload에서 불러오고
        }

        void PassText()
        {
            while (TextQueue.Count > 0)
            {
                TextStringBuilder.Append(TextTypeNow.Ch);
                TextTypeNow = TextQueue.Dequeue();
            }
            TextStringBuilder.Append(TextTypeNow.Ch);
            TextElapsedTime = 0.0f;
            bTextFullLoad = true;
        }

        void TextOutput(bool bPass)
        {
            if (bPass)
            {
                PassText();
            }
            else
            {
                TextElapsedTime += Time.deltaTime;
                if (TextElapsedTime >= TextTypeNow.TextOutputTime)
                {
                    TextStringBuilder.Append(TextTypeNow.Ch);
                    if (TextQueue.Count == 0)
                    {
                        bTextFullLoad = true;
                        return;
                    }
                    TextTypeNow = TextQueue.Dequeue();
                    TextElapsedTime = 0.0f;
                }
            }
        }

        void VoiceOutput(string voice)
        {

        }
    }
}
