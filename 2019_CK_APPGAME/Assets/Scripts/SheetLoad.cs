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


    class ErrorDataStructer
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

        public CommandCharImage(string id,int state)
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

    public class TextSet         //TextQueu 구현용 자료구조
    {
        private char mCh;          //문자
        private float mTextOutputTime; //문자출력속도
        private int mTextSize; //문자크기
        private string mTextColor; //문자색상
        public CommandCharImage mCmdCharImg; //캐릭터 불러오기

        public TextSet()
        {
            mCh = ' ';
            mTextOutputTime = 0.2f;
            mTextSize = 40;
            mTextColor = "black";
            mCmdCharImg = new CommandCharImage("CH_02",0);
        }

        public TextSet(TextSet textSet)
        {
            mCh = textSet.Ch;
            mTextOutputTime = textSet.TextOutputTime;
            mTextSize = textSet.TextSize;
            mTextColor = textSet.TextColor;
            mCmdCharImg = new CommandCharImage(textSet.mCmdCharImg);
        }

        public char Ch { get => mCh; set => mCh = value; }
        public float TextOutputTime { get => mTextOutputTime; set => mTextOutputTime = value; }
        public int TextSize { get => mTextSize; set => mTextSize = value; }
        public string TextColor { get => mTextColor; set => mTextColor = value; }
    }

    public static class DataSheetSet
    {
        public static Dictionary<string, List<TextType>> TextDictionaryListQueue = new Dictionary<string, List<TextType>>(); //모든장면을 관리하는 딕셔너리
        public static Dictionary<string, List<CharImageType>> CharImageDictionary = new Dictionary<string, List<CharImageType>>();  //캐릭터ID & 이미지를 관리하는 딕셔너리
    }

    public class SheetLoad : MonoBehaviour
    {
        private List<ErrorDataStructer> ErrorList;
        private DialogStory DialogStroyResource;
        private CharImage CharImageResource;
        void Awake()
        {
            ErrorList = new List<ErrorDataStructer>();
            DialogStroyResource = Resources.Load<DialogStory>("Data/SheetData/DialogStory");
            DialogStoryDataLoad();
            CharImageResource = Resources.Load<CharImage>("Data/SheetData/CharImage");
            CharImageDataLoad();
            ErrorOutput();
        }

        private void ErrorOutput()
        {
            for(int index = 0; index < ErrorList.Count; index++)
            {
                Debug.LogFormat("WorkSheetName: {0}\nSheetIndex: {1}", ErrorList[index].WorkSheetName, ErrorList[index].Index);
            }
        }

        //다이얼로그스토리 워크시트 불러오기
        private void DialogStoryDataLoad()
        {
            for (int index = 0; index < DialogStroyResource.dataArray.Length; index++)
            {
                //텍스트딕셔너리리스트에 TextType 셋팅 
                TextType textType = TextLoad(
                    DialogStroyResource.dataArray[index].Command,
                    DialogStroyResource.dataArray[index].Index,
                    DialogStroyResource.WorksheetName);
                textType.TalkerName = DialogStroyResource.dataArray[index].Talkername;
                textType.CharID = DialogStroyResource.dataArray[index].Charid;
                //텍스트딕셔너리에 기존 ID 값이 있는지 확인 없으면 새로 생성
                if (!DataSheetSet.TextDictionaryListQueue.ContainsKey(DialogStroyResource.dataArray[index].ID))
                {
                    DataSheetSet.TextDictionaryListQueue.Add(DialogStroyResource.dataArray[index].ID, new List<TextType>());
                }
                DataSheetSet.TextDictionaryListQueue[DialogStroyResource.dataArray[index].ID].Add(textType);
            }
        }

        //커맨드String, 워크시트인덱스, 워크시트이름
        private TextType TextLoad(string str, int listIndex, string workSheetName)
        {
            int startIndex; //시작인덱스 { = 1
            int midFirstIndex;   //:: 중간 첫인덱스
            int midLastIndex;
            int lastIndex;   //} 끝 인덱스
            string commandStr; //커맨드 타입 분별
            string valueStr;
            TextType textType = new TextType
            {
                Index = listIndex
            };
            TextSet textSet = new TextSet();
            for (int index = 0; index < str.Length; index++)
            {
                if (str[index] == '{')
                {
                    startIndex = index + 1; //시작인덱스 { = 1
                    midFirstIndex = str.IndexOf("::", index);   //:: 중간 첫인덱스
                    midLastIndex = midFirstIndex + 2;      //:: 중간 끝인덱스
                    lastIndex = str.IndexOf("}", index);   //} 끝 인덱스
                    commandStr = str.Substring(startIndex, midFirstIndex - startIndex); //커맨드 타입 분별
                    valueStr = str.Substring(midLastIndex, lastIndex - midLastIndex);  //값 분별
                    if (commandStr == "출력속도")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextOutputTime = System.Convert.ToSingle(valueStr);
                        else
                            textSet.TextOutputTime = 1.0f;
                    }
                    else if (commandStr == "크기")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextSize = System.Convert.ToInt32(valueStr);
                        else
                            textSet.TextSize = 40;
                    }
                    else if (commandStr == "색상")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextColor = valueStr;
                        else
                            textSet.TextColor = "black";
                    }
                    else if (commandStr == "이미지")
                    {
                        if(valueStr == "")
                        {
                            ErrorList.Add(new ErrorDataStructer(listIndex, workSheetName));     //읽어들이기 실패시 에러리스트에 추가
                            break;
                        }
                        midFirstIndex = valueStr.IndexOf("::");
                        midLastIndex = midFirstIndex + 2;
                        commandStr = valueStr.Substring(0, midFirstIndex);
                        valueStr = valueStr.Substring(midLastIndex, valueStr.Length - midLastIndex);
                        textSet.mCmdCharImg.ID = commandStr;
                        textSet.mCmdCharImg.State = System.Convert.ToInt32(valueStr);
                    }
                    else
                    {
                        ErrorList.Add(new ErrorDataStructer(listIndex, workSheetName));     //읽어들이기 실패시 에러리스트에 추가
                        break;
                    }
                    index += lastIndex - index;          //인식한 커맨드 문장 건너뛰기
                }
                else
                {
                    if (str[index] == '`')  //다음줄로 이동 인식
                    {
                        if (str[++index] == 'ㄷ')
                        {
                            textSet.Ch = '\n';
                        }
                    }
                    else
                        textSet.Ch = str[index];
                    textType.mTextQueue.Enqueue(new TextSet(textSet));  //텍스트큐에 문자넣기
                }
            }
            return textType;
        }

        //캐릭터이미지 로드
        private void CharImageDataLoad()
        {
            for (int index = 0; index < CharImageResource.dataArray.Length; index++)
            {
                if (!DataSheetSet.CharImageDictionary.ContainsKey(CharImageResource.dataArray[index].ID))
                {
                    DataSheetSet.CharImageDictionary.Add(CharImageResource.dataArray[index].ID, new List<CharImageType>());
                }
                DataSheetSet.CharImageDictionary[CharImageResource.dataArray[index].ID].Add(new CharImageType(
                CharImageResource.dataArray[index].State,
                CharImageResource.dataArray[index].Imagelocation,
                CharImageResource.dataArray[index].Name));
            }
        }
    }
}
