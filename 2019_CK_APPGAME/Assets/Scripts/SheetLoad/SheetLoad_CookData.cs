using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{

    public class SheetLoad_CookData : SheetLoad
    {
        public override void SheetDataLoad()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/CookData");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.CookDataDictionary.ContainsKey(key))
                {
                    DataJsonSet.CookDataDictionary.Add(key, 
                        new LoadDataType(jsonData[i]["ID"].ToString(),
                        jsonData[i]["ImageLocation"].ToString(),
                        jsonData[i]["Name"].ToString(),
                        jsonData[i]["Description"].ToString()));
                }
            }
        }

        public override void IntegrityCheck()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/CookData");
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (UnityEngine.Resources.Load(jsonData[i]["ImageLocation"].ToString()) == null)
                {
                    SundryUtil.ErrorAdd(i, "CookData - Location");
                }
            }
        }
    }
}
