using System.Collections.Generic;
using GameScene;
namespace SheetData
{
    public static class DataJsonSet
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
        public Queue<DlgCmd> mTextQueue;    //대사큐
        private string mCharID;              //캐릭터ID
        private string mTalkerName;          //대화자
        private int mIndex;                  //순서 절댓값

        public TextType()
        {
            mCharID = "";
            mTalkerName = "";
            mIndex = -1;
            mTextQueue = new Queue<DlgCmd>();
        }

        public string CharID { get => mCharID; set => mCharID = value; }
        public string TalkerName { get => mTalkerName; set => mTalkerName = value; }
        public int Index { get => mIndex; set => mIndex = value; }
    }
}
