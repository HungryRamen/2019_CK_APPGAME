using System.Collections.Generic;
using System;
using LitJson;
namespace CharData
{
    public static class CharDataSet
    {
        public static Dictionary<string, CharDataStructure> charDataDictionary = new Dictionary<string, CharDataStructure>();
    }

    public class CharDataStructure
    {
        int storyState;
        string eatFoodID;
        int[] status = new int[5];
        string drinkID;
        public List<string> combinationCheck = new List<string>();

        public int StoryState { get => storyState; set => storyState = value; }
        public int[] Status { get => status; set => status = value; }
        public string EatFoodID { get => eatFoodID; set => eatFoodID = value; }
        public string DrinkID { get => drinkID; set => drinkID = value; }

        public CharDataStructure()
        {
            storyState = 1;
            for(int i = 0; i <status.Length; i++)
            {
                status[i] = 0;
            }
            //combinationCheck.Add("FD_IDDK_ID");
            //combinationCheck.Add("FD_IDDK_ID2");
        }

        public CharDataStructure(JsonData jsonData,string key)
        {
            storyState = Convert.ToInt32(jsonData[key]["StoryState"].ToString());
            for (int i = 0; i < jsonData[key]["Status"].Count; i++)
            {
                status[i] = Convert.ToInt32(jsonData[key]["Status"][i].ToString());
            }
            for(int i = 0; i <jsonData[key]["combinationCheck"].Count; i++)
            {
                combinationCheck.Add(jsonData[key]["combinationCheck"][i].ToString());
            }
        }

        public CharDataStructure(CharDataStructure copy)
        {
            storyState = copy.storyState;
            for(int i = 0; i < copy.status.Length; i++)
            {
                status[i] = copy.status[i];
            }
            combinationCheck = new List<string>(copy.combinationCheck);
        }
    }
}
