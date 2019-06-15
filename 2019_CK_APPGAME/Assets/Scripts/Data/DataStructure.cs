using System.Collections.Generic;
using DialogCommand;
using FMOD.Studio;

namespace SheetData
{
    public enum ESoundType
    {
        Narration,
        PublicButton,
        RestaurantAmb,
        RestaurantMusic,
        CH00,
        CH01,
        CH02,
        CH03,
        CH04,
        CH05,
        CH06,
        DKButton,
        DK01,
        DK02,
        DK03,
        DK04,
        DK05,
        FinishSFX,
        FMButton,
        Boiling,
        Ice,
        Shells,
        Sizzling,
        Snapping,
        Plate,
        Log,
        Next,
        Selectionln,
        Status,
        StatusUpdate,
        Fridge,
        NPCEntry,
        Unlock,
        PopUp,
        Volume,
        Sizzled,
        Poizon,
        Empty,
        Save,
        Calender,
        Page,
        Mainmusic,
        CalenderMusic,
        MainAmb,
        CalenderAmb,
        Change,
        Normal,
    }
    public static class DataJsonSet
    {
        public static Dictionary<string, List<TextType>> TextDictionary = new Dictionary<string, List<TextType>>(); //스토리장면을 관리하는 딕셔너리
        public static Dictionary<string, List<CharImageType>> CharImageDictionary = new Dictionary<string, List<CharImageType>>();  //캐릭터ID & 이미지를 관리하는 딕셔너리
        public static Dictionary<string, List<DayEventsType>> DayEventsDictionary = new Dictionary<string, List<DayEventsType>>();  //캐릭터ID X일차관리 딕셔너리
        public static Dictionary<string, List<TextTypeRaction>> TextReactionDictionary = new Dictionary<string, List<TextTypeRaction>>(); //리액션 장면을 관리하는 딕셔너리
        public static Dictionary<string, List<TextTypeRaction>> TextCombinationReactionDictionary = new Dictionary<string, List<TextTypeRaction>>(); //리액션 장면을 관리하는 딕셔너리
        public static Dictionary<string, List<TriggerType>> TriggerDictionary = new Dictionary<string, List<TriggerType>>(); //트리거를 관리하는 딕셔너리
        public static Dictionary<string, LoadDataType> FoodDataDictionary = new Dictionary<string, LoadDataType>(); // 음식 데이터를 관리하는 딕셔너리
        public static Dictionary<string, LoadDataType> CookDataDictionary = new Dictionary<string, LoadDataType>(); // 조리 방법 데이터를 관리하는 딕셔너리
        public static Dictionary<string, LoadDataType> MaterialDataDictionary = new Dictionary<string, LoadDataType>(); // 음식 재료 데이터를 관리하는 딕셔너리
        public static Dictionary<string, StatusDataType> StatusDataDictionary = new Dictionary<string, StatusDataType>(); //음식 스테이터스 딕셔너리
        public static Dictionary<string, List<string>> RecipeDictionary = new Dictionary<string, List<string>>(); // 레시피 딕셔너리
        public static Dictionary<string, RecipeDataType> RecipeDataDictionary = new Dictionary<string, RecipeDataType>(); // 레시피 조합 딕셔너리
        public static Dictionary<ESoundType, SoundDataType> SoundDataDictionary = new Dictionary<ESoundType, SoundDataType>();  // 사운드 딕셔너리
        public static Dictionary<string, SoundFoodDataType> SoundFoodDataDictionary = new Dictionary<string, SoundFoodDataType>(); // 음식 사운드 딕셔너리
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
        int startIndex;
        int endIndex;

        public DayEventsType(int story, string cID, string dID, string tID,int sIndex,int eIndex)
        {
            storyState = story;
            CharID = cID;
            dialogID = dID;
            triggerID = tID;
            startIndex = sIndex;
            endIndex = eIndex;
        }

        public int StoryState { get => storyState; set => storyState = value; }
        public string DialogID { get => dialogID; set => dialogID = value; }
        public string TriggerID { get => triggerID; set => triggerID = value; }
        public string CharID { get => charID; set => charID = value; }
        public int StartIndex { get => startIndex; set => startIndex = value; }
        public int EndIndex { get => endIndex; set => endIndex = value; }
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
        string charId;              // 접근 ID
        string talkerName;          //대화자
        int index;                  //순서 절댓값

        public TextType()
        {
            charId = "";
            talkerName = "";
            index = -1;
            textQueue = new Queue<DlgCmd>();
        }

        public string CharId { get => charId; set => charId = value; }
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
            foreach (string temp in triggerList)
            {
                if (temp == "Defualt")
                    return true;
                int arrCount = System.Convert.ToInt32(temp.Substring(0,1)) - 1;
                if (temp[3] == 'T')
                {
                    if (CharData.CharDataSet.charDataDictionary[charID].Status[arrCount] < 50)
                        return false;
                }
                else if(temp[3] == 'F')
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

    public class LoadDataType
    {
        string id;
        string imageLocation;
        string name;
        string description;
        public LoadDataType(string id, string location, string name, string description)
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

    public class StatusDataType
    {
        int[] status = new int[5];
        public StatusDataType(int[] status)
        {
            this.status = status;
        }

        public int[] Status { get => status; set => status = value; }
    }

    public class RecipeDataType
    {
        string cookID;
        string foodMaterialID;
        string foodSubMaterialID;
        public RecipeDataType(string cID, string fmID, string fsmID)
        {
            cookID = cID;
            foodMaterialID = fmID;
            foodSubMaterialID = fsmID;
        }

        public string CookID { get => cookID; set => cookID = value; }
        public string FoodMaterialID { get => foodMaterialID; set => foodMaterialID = value; }
        public string FoodSubMaterialID { get => foodSubMaterialID; set => foodSubMaterialID = value; }
    }

    public class SoundDataType
    {
        string path;
        public List<string> parameterName = new List<string>();

        public SoundDataType(string p, string t1, string t2, string t3)
        {
            path = p;
            if (t1 != "NULL")
                parameterName.Add(t1);
            if (t2 != "NULL")
                parameterName.Add(t2);
            if (t3 != "NULL")
                parameterName.Add(t3);
        }

        public string Path { get => path; set => path = value; }
    }

    public class SoundFoodDataType
    {
        bool isLoop;
        int index;
        public SoundFoodDataType(int index,bool loop)
        {
            IsLoop = loop;
            this.Index = index;
        }

        public bool IsLoop { get => isLoop; set => isLoop = value; }
        public int Index { get => index; set => index = value; }
    }
}
