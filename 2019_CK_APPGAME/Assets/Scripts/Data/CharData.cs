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
        }

        public CharDataStructure(JsonData jsonData,string key)
        {
            storyState = Convert.ToInt32(jsonData["dataDic"][key]["StoryState"].ToString());
            for (int i = 0; i < jsonData["dataDic"][key]["Status"].Count; i++)
            {
                status[i] = Convert.ToInt32(jsonData["dataDic"][key]["Status"][i].ToString());
            }
            for(int i = 0; i <jsonData["dataDic"][key]["combinationCheck"].Count; i++)
            {
                combinationCheck.Add(jsonData["dataDic"][key]["combinationCheck"][i].ToString());
            }
        }
    }
}
