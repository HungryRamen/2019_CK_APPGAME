﻿using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;

namespace SheetLoad
{

    public sealed class SheetLoad_Trigger : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.TriggerDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/Trigger");
            for (int i = 0; i < jsonData.Count; i++)
            {
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.TriggerDictionary.ContainsKey(key))
                {
                    DataJsonSet.TriggerDictionary.Add(key, new List<TriggerType>());
                }
                TriggerType temp = new TriggerType();
                string tempStr = jsonData[i]["Trigger"].ToString();
                int indexof = tempStr.IndexOf('\n');
                if (indexof == -1)
                    temp.triggerList.Add(tempStr);
                else
                {
                    while (indexof != -1)
                    {
                        string tempStr2 = tempStr.Substring(0, indexof);
                        temp.triggerList.Add(tempStr2);
                        tempStr = tempStr.Substring(indexof + 1);
                        indexof = tempStr.IndexOf('\n');
                    }
                    temp.triggerList.Add(tempStr);
                }
                for(int index = 0; index < temp.triggerList.Count;index++)
                {
                    if(temp.triggerList[index].Length == 4 || temp.triggerList[index].Length == 7)
                    {
                        continue;
                    }
                    SundryUtil.ErrorAdd(i, "Trigger - Trigger");
                }
                temp.Status[0] = Convert.ToInt32(jsonData[i]["Status1"].ToString());
                temp.Status[1] = Convert.ToInt32(jsonData[i]["Status2"].ToString());
                temp.Status[2] = Convert.ToInt32(jsonData[i]["Status3"].ToString());
                temp.Status[3] = Convert.ToInt32(jsonData[i]["Status4"].ToString());
                temp.Status[4] = Convert.ToInt32(jsonData[i]["Status5"].ToString());
                if (jsonData[i]["StoryState"].ToString() != "NULL")
                {
                    try
                    {
                        temp.StoryState = Convert.ToInt32(jsonData[i]["StoryState"].ToString());
                    }
                    catch(Exception e)
                    {
                        SundryUtil.ErrorAdd(i, "Trigger - Trigger", e);
                    }
                }
                DataJsonSet.TriggerDictionary[key].Add(temp);
            }

            
        }

        public override void IntegrityCheck() //참조하는게 없음
        {
        }
    }
}
