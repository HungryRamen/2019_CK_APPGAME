using SheetData;
using Util;
using LitJson;
using System.Collections.Generic;
using System;

namespace SheetLoad
{

    public sealed class SheetLoad_DayEvents : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.DayEventsDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/DayEvents");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["Day"].ToString();
                if (!DataJsonSet.DayEventsDictionary.ContainsKey(key))
                {
                    DataJsonSet.DayEventsDictionary.Add(key, new List<DayEventsType>());
                }
                DataJsonSet.DayEventsDictionary[key].Add(new DayEventsType(Convert.ToInt32(jsonData[i]["StoryState"].ToString()), jsonData[i]["CharID"].ToString(), jsonData[i]["DialogID"].ToString(), jsonData[i]["TriggerID"].ToString()));
            }
        }
        public override void IntegrityCheck()
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/DayEvents");
            for (int i = 0; i < jsonData.Count; i++)
            {
                if (!CharData.CharDataSet.charDataDictionary.ContainsKey(jsonData[i]["CharID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "DayEvents - CharID");
                }
                if (!DataJsonSet.TextDictionary.ContainsKey(jsonData[i]["DialogID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "DayEvents - DialogID");
                }
                if (!DataJsonSet.TriggerDictionary.ContainsKey(jsonData[i]["TriggerID"].ToString()))
                {
                    SundryUtil.ErrorAdd(i, "DayEvents - TriggerID");
                }
            }
        }
    }
}