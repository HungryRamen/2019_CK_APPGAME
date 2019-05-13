using SheetData;
using Util;
using LitJson;
using System.Collections.Generic;
using System;

namespace SheetLoad
{

    public class SheetLoad_DayEvents : SheetLoad
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
                DataJsonSet.DayEventsDictionary[key].Add(new DayEventsType(Convert.ToInt32(jsonData[i]["StoryState"].ToString()), jsonData[i]["CharID"].ToString(), jsonData[i]["DialogID"].ToString(),jsonData[i]["TriggerID"].ToString()));
            }
        }
    }
}