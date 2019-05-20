using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{
    public sealed class SheetLoad_Char : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.CharImageDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/CharImage");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.CharImageDictionary.ContainsKey(key))
                {
                    DataJsonSet.CharImageDictionary.Add(key, new List<CharImageType>());
                }
                DataJsonSet.CharImageDictionary[key].Add(new CharImageType(Convert.ToInt32(jsonData[i]["State"].ToString()), jsonData[i]["ImageLocation"].ToString(), jsonData[i]["Name"].ToString()));
            }
        }
        public override void IntegrityCheck()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/CharImage");
            for (int i = 0; i < jsonData.Count; i++)
            {
                if(UnityEngine.Resources.Load(jsonData[i]["ImageLocation"].ToString()) == null)
                {
                    SundryUtil.ErrorAdd(i, "CharImage - Location");
                }
            }
        }
    }

}