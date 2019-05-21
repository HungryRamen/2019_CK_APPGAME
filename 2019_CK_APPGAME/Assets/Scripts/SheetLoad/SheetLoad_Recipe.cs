using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{
    public sealed class SheetLoad_Recipe : SheetLoad
    {
        public override void SheetDataLoad()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/Recipe");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string foodID = jsonData[i]["FoodID"].ToString();
                string cookID = jsonData[i]["CookID"].ToString();
                string fmID = jsonData[i]["FoodMaterialID"].ToString();
                string fsmID = jsonData[i]["FoodSubMaterialID"].ToString();

                DataJsonSet.RecipeDataDictionary.Add(foodID, new RecipeDataType(cookID, fmID, fsmID));
                if(!DataJsonSet.RecipeDictionary.ContainsKey(cookID))
                {
                    DataJsonSet.RecipeDictionary.Add(cookID, new List<string>());
                }
                DataJsonSet.RecipeDictionary[cookID].Add(foodID);
                if (!DataJsonSet.RecipeDictionary.ContainsKey(fmID))
                {
                    DataJsonSet.RecipeDictionary.Add(fmID, new List<string>());
                }
                DataJsonSet.RecipeDictionary[fmID].Add(foodID);
                if (!DataJsonSet.RecipeDictionary.ContainsKey(fsmID))
                {
                    DataJsonSet.RecipeDictionary.Add(fsmID, new List<string>());
                }
                DataJsonSet.RecipeDictionary[fsmID].Add(foodID);

            }
        }

        public override void IntegrityCheck()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/Recipe");
            for (int i = 0; i < jsonData.Count; i++)
            {
                if(!DataJsonSet.FoodDataDictionary.ContainsKey(jsonData[i]["FoodID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "Recipe - FoodID");
                }
                if(!DataJsonSet.CookDataDictionary.ContainsKey(jsonData[i]["CookID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "Recipe - CookID");
                }
                if(!DataJsonSet.MaterialDataDictionary.ContainsKey(jsonData[i]["FoodMaterialID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "Recipe - FoodMaterialID");
                }
                if(!DataJsonSet.MaterialDataDictionary.ContainsKey(jsonData[i]["FoodSubMaterialID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "Recipe - FoodSubMaterialID");
                }
            }
        }
    }
}
