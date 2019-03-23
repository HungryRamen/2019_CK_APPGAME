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
    }

    public class TextType              //TextListQueue 구현용
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

    public class TextSet         //TextQueu 구현용
    {
        private char mCh;          //문자
        private float mTextOutputTime; //문자출력속도
        private int mTextSize; //문자크기
        private string mTextColor; //문자색상

        public TextSet()
        {
            mCh = ' ';
            mTextOutputTime = 1.0f;
            mTextSize = 40;
            mTextColor = "black";
        }

        public TextSet(TextSet textSet)
        {
            mCh = textSet.Ch;
            mTextOutputTime = textSet.TextOutputTime;
            mTextSize = textSet.TextSize;
            mTextColor = textSet.TextColor;
        }

        public char Ch { get => mCh; set => mCh = value; }
        public float TextOutputTime { get => mTextOutputTime; set => mTextOutputTime = value; }
        public int TextSize { get => mTextSize; set => mTextSize = value; }
        public string TextColor { get => mTextColor; set => mTextColor = value; }
    }
    public static class DialogTextData
    {
        public static Dictionary<string, List<TextType>> TextDictionaryListQueue = new Dictionary<string, List<TextType>>(); //모든장면을 관리하는 딕셔너리
    }
    public class SheetLoad : MonoBehaviour
    {
        private List<ErrorDataStructer> ErrorList;
        private DialogStory DialogStroyResource;
        void Awake()
        {
            ErrorList = new List<ErrorDataStructer>();
            DialogStroyResource = Resources.Load<DialogStory>("Data/SheetData/DialogStory");
            DialogStoryDataLoad();
        }

        //다이얼로그스토리 워크시트 불러오기
        private void DialogStoryDataLoad()
        {
            for (int index = 0; index < DialogStroyResource.dataArray.Length; index++)
            {
                //텍스트딕셔너리리스트에 TextType 셋팅 
                TextType textType = TextLoad(DialogStroyResource.dataArray[index].Command,DialogStroyResource.dataArray[index].Index,DialogStroyResource.WorksheetName);
                textType.TalkerName = DialogStroyResource.dataArray[index].Talkername;
                textType.CharID = DialogStroyResource.dataArray[index].Charid;
                //텍스트딕셔너리에 기존 ID 값이 있는지 확인 없으면 새로 생성
                if (!DialogTextData.TextDictionaryListQueue.ContainsKey(DialogStroyResource.dataArray[index].ID))
                {
                    DialogTextData.TextDictionaryListQueue.Add(DialogStroyResource.dataArray[index].ID, new List<TextType>());
                }
                DialogTextData.TextDictionaryListQueue[DialogStroyResource.dataArray[index].ID].Add(textType);
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
                    else if(commandStr == "크기")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextSize = System.Convert.ToInt32(valueStr);
                        else
                            textSet.TextSize = 40;
                    }
                    else if(commandStr == "색상")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextColor = valueStr;
                        else
                            textSet.TextColor = "black";
                    }
                    else if(commandStr == "이미지")
                    {
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
    }
}
