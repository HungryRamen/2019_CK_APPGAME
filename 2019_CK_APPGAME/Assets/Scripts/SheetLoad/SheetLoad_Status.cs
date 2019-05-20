using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{

    public sealed class SheetLoad_Status : SheetLoad
    {

        public override void SheetDataLoad()
        {
            string[] s = new string[5];
            s[0] = "status1";
            s[1] = "status2";
            s[2] = "status3";
            s[3] = "status4";
            s[4] = "status5";
            JsonData jsonData = SundryUtil.JsonDataLoad("/Status");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["ID"].ToString();
                int[] status = new int[5];
                for (int j = 0; j < status.Length; j++)
                {
                    status[j] = Convert.ToInt32(jsonData[i][s[j]].ToString());
                }
                if (!DataJsonSet.StatusDataDictionary.ContainsKey(key))
                {
                    DataJsonSet.StatusDataDictionary.Add(key, new StatusDataType(status));
                }
            }
        }

        public override void IntegrityCheck()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/Status");
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (!DataJsonSet.FoodDataDictionary.ContainsKey(jsonData[i]["ID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "Status - FDID");
                }
            }
        }
    }
}
