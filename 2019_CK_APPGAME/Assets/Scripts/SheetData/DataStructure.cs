using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SheetData
{
    public static class DataSheetSet
    {
        public static Dictionary<string, List<TextType>> TextDictionary = new Dictionary<string, List<TextType>>(); //모든장면을 관리하는 딕셔너리
        public static Dictionary<string, List<CharImageType>> CharImageDictionary = new Dictionary<string, List<CharImageType>>();  //캐릭터ID & 이미지를 관리하는 딕셔너리
    }


    enum ETextCommandType
    {
        None,               //예외처리
        Text,               //문자
        TextSize,           //문자크기
        TextColor,          //문자색깔
    }


    public class ErrorDataStructer
    {
        private int mIndex;
        private string mWorkSheetName;
        public ErrorDataStructer(int index, string workSheetName)
        {
            mIndex = index;
            mWorkSheetName = workSheetName;
        }

        public int Index { get => mIndex; set => mIndex = value; }
        public string WorkSheetName { get => mWorkSheetName; set => mWorkSheetName = value; }
    }


    public class CharImageType            //캐릭터이미지 시트 데이터 자료구조
    {
        int mState;
        string mImagePath;
        string mName;

        public CharImageType(int state, string imagePath, string name)
        {
            mState = state;
            mImagePath = imagePath;
            mName = name;
        }

        public int State { get => mState; set => mState = value; }
        public string ImagePath { get => mImagePath; set => mImagePath = value; }
        public string Name { get => mName; set => mName = value; }
    }

    public class TextType              //TextListQueue 구현용 자료구조
    {
        public Queue<TextSet> mTextQueue;    //대사큐
        private string mCharID;              //캐릭터ID
        private string mTalkerName;          //대화자
        private int mIndex;                  //순서 절댓값

        public TextType()
        {
            mCharID = "";
            mTalkerName = "";
            mIndex = -1;
            mTextQueue = new Queue<TextSet>();
        }

        public string CharID { get => mCharID; set => mCharID = value; }
        public string TalkerName { get => mTalkerName; set => mTalkerName = value; }
        public int Index { get => mIndex; set => mIndex = value; }
    }

    public class CommandCharImage
    {
        private string mID;     //캐릭터 ID
        private int mState;     //캐릭터 State,표정

        public CommandCharImage(string id, int state)
        {
            mID = id;
            mState = state;
        }

        public CommandCharImage(CommandCharImage cmdCharImg)
        {
            mID = cmdCharImg.mID;
            mState = cmdCharImg.mState;
        }

        public string ID { get => mID; set => mID = value; }
        public int State { get => mState; set => mState = value; }
    }

    public class CommandScreenShake
    {
        private float mScreenShakeTime; //화면흔드는 시간
        private float mScreenShakeOffSet; //화면흔들림 크기
        private bool mbOneTime; //1회성 화면흔들림 
        public CommandScreenShake(float time,float offSet)
        {
            mScreenShakeOffSet = time;
            mScreenShakeOffSet = offSet;
            bOneTime = false;
        }

        public CommandScreenShake(CommandScreenShake cmdScreenShake)
        {
            mScreenShakeOffSet = cmdScreenShake.mScreenShakeOffSet;
            mScreenShakeTime = cmdScreenShake.mScreenShakeTime;
            bOneTime = cmdScreenShake.bOneTime;
        }

        public float ScreenShakeTime { get => mScreenShakeTime; set => mScreenShakeTime = value; }
        public float ScreenShakeOffSet { get => mScreenShakeOffSet; set => mScreenShakeOffSet = value; }
        public bool bOneTime { get => mbOneTime; set => mbOneTime = value; }
    }

    public class TextSet         //TextQueu 구현용 자료구조
    {
        private char mCh;          //문자
        private float mTextOutputTime; //문자출력속도
        private int mTextSize; //문자크기
        private string mTextColor; //문자색상
        private bool mbOrderButtonEnable;
        public CommandScreenShake mCmdScreenShake; //화면흔들기
        public CommandCharImage mCmdCharImg; //캐릭터 불러오기

        public TextSet()
        {
            mCh = ' ';
            mTextOutputTime = 0.2f;
            mTextSize = 40;
            mTextColor = "black";
            mCmdScreenShake = new CommandScreenShake(1.0f, 10.0f);
            mCmdCharImg = new CommandCharImage("CH_02", 0);
            mbOrderButtonEnable = false;
        }

        public TextSet(TextSet textSet)
        {
            mCh = textSet.Ch;
            mTextOutputTime = textSet.TextOutputTime;
            mTextSize = textSet.TextSize;
            mTextColor = textSet.TextColor;
            mCmdScreenShake = new CommandScreenShake(textSet.mCmdScreenShake);
            mCmdCharImg = new CommandCharImage(textSet.mCmdCharImg);
            mbOrderButtonEnable = textSet.IsOrderButtonEnable;
        }

        public char Ch { get => mCh; set => mCh = value; }
        public float TextOutputTime { get => mTextOutputTime; set => mTextOutputTime = value; }
        public int TextSize { get => mTextSize; set => mTextSize = value; }
        public string TextColor { get => mTextColor; set => mTextColor = value; }
        public bool IsOrderButtonEnable { get => mbOrderButtonEnable; set => mbOrderButtonEnable = value; }
    }

    public class DataStructure
    {

    }
}
