using System.Collections.Generic;
using DialogCommand;
namespace SheetData
{
    public static class DataJsonSet
    {
        public static Dictionary<string, List<TextType>> TextDictionary = new Dictionary<string, List<TextType>>(); //스토리장면을 관리하는 딕셔너리
        public static Dictionary<string, List<CharImageType>> CharImageDictionary = new Dictionary<string, List<CharImageType>>();  //캐릭터ID & 이미지를 관리하는 딕셔너리
        public static Dictionary<string, List<DayEventsType>> DayEventsDictionary = new Dictionary<string, List<DayEventsType>>();  //캐릭터ID X일차관리 딕셔너리
        public static Dictionary<string, List<TextTypeRaction>> TextReactionDictionary = new Dictionary<string, List<TextTypeRaction>>(); //음식 리액션 장면을 관리하는 딕셔너리
        public static Dictionary<string, List<TriggerType>> TriggerDictionary = new Dictionary<string, List<TriggerType>>(); //트리거를 관리하는 딕셔너리
        public static Dictionary<string, List<FoodDataType>> FoodDataDictionary = new Dictionary<string, List<FoodDataType>>(); // 음식 데이터를 관리하는 딕셔너리
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
        int index;
        string workSheetName;
        public ErrorDataStructer(int index, string workSheetName)
        {
            this.index = index;
            this.workSheetName = workSheetName;
        }

        public int Index { get => index; set => index = value; }
        public string WorkSheetName { get => workSheetName; set => workSheetName = value; }
    }

    public class DayEventsType
    {
        int storyState;
        string charID;
        string dialogID;
        string triggerID;

        public DayEventsType(int story,string cID,string dID,string tID)
        {
            storyState = story;
            CharID = cID;
            dialogID = dID;
            triggerID = tID;
        }

        public int StoryState { get => storyState; set => storyState = value; }
        public string DialogID { get => dialogID; set => dialogID = value; }
        public string TriggerID { get => triggerID; set => triggerID = value; }
        public string CharID { get => charID; set => charID = value; }
    }


    public class CharImageType            //캐릭터이미지 시트 데이터 자료구조
    {
        int state;
        string imagePath;
        string name;

        public CharImageType(int state, string imagePath, string name)
        {
            this.state = state;
            this.imagePath = imagePath;
            this.name = name;
        }

        public int State { get => state; set => state = value; }
        public string ImagePath { get => imagePath; set => imagePath = value; }
        public string Name { get => name; set => name = value; }
    }

    public class TextType              //TextListQueue 구현용 자료구조
    {
        public Queue<DlgCmd> textQueue;    //대사큐
        string id;              // 접근 ID
        string talkerName;          //대화자
        int index;                  //순서 절댓값

        public TextType()
        {
            id = "";
            talkerName = "";
            index = -1;
            textQueue = new Queue<DlgCmd>();
        }

        public string ID { get => id; set => id = value; }
        public string TalkerName { get => talkerName; set => talkerName = value; }
        public int Index { get => index; set => index = value; }
    }

    public class TextTypeRaction : TextType
    {
        int storyState;
        public TextTypeRaction() : base()
        {
            storyState = 1;
        }

        public int StoryState { get => storyState; set => storyState = value; }
    }

    public class TextStackType
    {
        public List<TextType> textTypeList;
        int textTypeIndex;
        public TextStackType()
        {
            textTypeList = new List<TextType>();
            textTypeIndex = 0;
        }

        public int TextTypeIndex { get => textTypeIndex; set => textTypeIndex = value; }
    }

    public class TriggerType
    {
        public List<string> triggerList;
        int[] status = new int[5];
        int storyState;

        public TriggerType()
        {
            triggerList = new List<string>();
            storyState = -1;
        }

        public bool IsTrigger(string charID)
        {
            foreach(string temp in triggerList)
            {
                if (temp == "Defualt")
                    return true;
                int arrCount = System.Convert.ToInt32(temp[0]);
                if(temp[3] == 'T')
                {
                    if (CharData.CharDataSet.charDataDictionary[charID].Status[arrCount] < 50)
                        return false;
                }
                else
                {
                    if (CharData.CharDataSet.charDataDictionary[charID].Status[arrCount] > 49)
                        return false;
                }
            }
            return true;
        }

        public int[] Status { get => status; set => status = value; }
        public int StoryState { get => storyState; set => storyState = value; }
    }

    public class FoodDataType
    {
        string id;
        string imageLocation;
        string name;
        string description;
        public FoodDataType(string id,string location,string name,string description)
        {
            this.id = id;
            this.imageLocation = location;
            this.name = name;
            this.description = description;
        }

        public string Id { get => id; set => id = value; }
        public string ImageLocation { get => imageLocation; set => imageLocation = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
    }
}
