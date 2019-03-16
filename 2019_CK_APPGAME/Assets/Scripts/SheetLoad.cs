using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace SheetData
{
    enum eTextCommandType
    {
        None,               //예외처리
        Text,               //문자
        TextSize,           //문자크기
        TextColor,          //문자색깔
    }

    public struct TextSet//Text Queu 구현용
    {
        public char mCh;          //문자
        public string mColor;     //문자색깔
        public int mSize;        //문자크기

    }

    public class SheetLoad : MonoBehaviour
    {
        private Char_Script mCharScript;
        private List<Queue<TextSet>> mTextQueueList;
        private List<int> mErrorList;
        private TextSet mTextSet;
        private int mCharIndex = 0;
        private int mDynamicTextSize;
        private StringBuilder mDynamicColor;
        void Awake()
        {
            TextSetDefault();
            mErrorList = new List<int>();
            mTextQueueList = new List<Queue<TextSet>>();
            mCharScript = Resources.Load<Char_Script>("Data/New Char_Script");
            //for(int index = 0; index < charScript.dataArray.Length; index++)
            //{
            //    Debug.LogFormat("{0} : {1}", charScript.dataArray[index].Key, charScript.dataArray[index].Command);
            //}
            //StringBuilder stringBuilder = new StringBuilder("`이미지`");
            //for (int index = 0; index < stringBuilder.Length; index++)
            //{
            //    Debug.Log(stringBuilder[index]);
            //
            //}
            //Chat(0);
        }

        //Sheet ErrorChecking
        //public bool ErrorCheck()
        //{
        //    bool bCheck = true;
        //    for (int index = 0; index < mCharScript.dataArray.Length; index++)
        //    {
        //        if (!Chat(index))
        //        {
        //            mErrorList.Add(index);
        //            bCheck = false;
        //        }
        //    }
        //    return bCheck;
        //}
        public bool TextCopyQueue(int index)
        {
            if (index < mTextQueueList.Count && index >= 0)
            {
                //if(Dialog.TextQueue != null)
                //{
                //
                //}
                //Dialog.TextQueue = new Queue<TextSet>(mTextQueueList[index]); 얕은복사가 되버린다면 이걸로 바꾸고 메모리해체 하는 방법찾기
                //DaialogSystem::Dialog.TextQueue = mTextQueueList[index];
                return true;
            }
            return false;
        }
        //dialog main
        public bool TextAllLoad()
        {
            StringBuilder stringBuilder = new StringBuilder();
            mCharIndex = 0;
            for(int index = 0; index < mCharScript.dataArray.Length; index++)
            {
                stringBuilder.Append(mCharScript.dataArray[index].Command);
                TextSetDefault();
                while (mCharIndex < stringBuilder.Length)
                {
                    switch (CommandTypeCheck(stringBuilder))
                    {
                        case eTextCommandType.Text:
                            //Dialog.TextQueue.Enqueue(mTextSet);
                            break;
                        case eTextCommandType.TextSize:
                            
                            break;
                        case eTextCommandType.TextColor:
                            break;
                    }
                }
                stringBuilder.Clear();
            }
            return true;
        }

        //CommandType Checking
        private eTextCommandType CommandTypeCheck(StringBuilder stringBuilder)
        {
            int Charindex = mCharIndex;
            if (stringBuilder[Charindex] == '{')
            {
                StringBuilder tempString = new StringBuilder();
                int index;
                for (index = Charindex + 1; index < Charindex + 6; index++)
                {
                    tempString.Append(stringBuilder[index]);
                }
                if (tempString.ToString() == "텍스트크기")
                {
                    mCharIndex = index + 1;
                    return eTextCommandType.TextSize;
                }
                else if (tempString.ToString() == "텍스트색상")
                {
                    return eTextCommandType.TextColor;
                }
            }
            else if (stringBuilder[Charindex] == '`')
            {
                for (int index = Charindex + 1; index < stringBuilder.Length; index++)
                {
                    if (stringBuilder[index] == '`')
                    {
                        return eTextCommandType.Text;
                    }
                }
            }
            return eTextCommandType.None;
        }

        private void TextSetDefault()       //문장이 시작할때
        {
            mTextSet = new TextSet()  //이부분도 구글스프레드 시트에서 불러오도록 하자
            {
                mCh = ' ',
                mColor = "black",
                mSize = 21
            };
            mDynamicColor = new StringBuilder(mTextSet.mColor);
            mDynamicTextSize = mTextSet.mSize;
        }

        private void TextSetDynamic()     //총문장이 넘어가기전에 또 다음 문장이오면
        {
            mTextSet.mCh = ' ';
            mTextSet.mColor = mDynamicColor.ToString();
            mTextSet.mSize = mDynamicTextSize;
        }
    }
}
