using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{
    public sealed class SheetLoad_FoodData : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.FoodDataDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/FDData");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.FoodDataDictionary.ContainsKey(key))
                {
                    DataJsonSet.FoodDataDictionary.Add(key, new List<FoodDataType>());
                }
                DataJsonSet.FoodDataDictionary[key].Add(new FoodDataType(jsonData[i]["ID"].ToString(), jsonData[i]["ImageLocation"].ToString(), jsonData[i]["Name"].ToString(), jsonData[i]["Description"].ToString()));
            }
        }
    }
}
