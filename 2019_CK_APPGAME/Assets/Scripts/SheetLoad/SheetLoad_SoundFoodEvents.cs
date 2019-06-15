using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;
namespace SheetLoad
{

    public class SheetLoad_SoundFoodEvents : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.SoundFoodDataDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/SoundFDEvents");
            for (int i = 0; i < jsonData.Count; i++)
            {
                try
                {
                    int index = Convert.ToInt32(jsonData[i]["SoundEventsIndex"].ToString());
                    if(index == 19 || index == 22 || index == 36)
                        DataJsonSet.SoundFoodDataDictionary.Add(jsonData[i]["ID"].ToString(),  new SoundFoodDataType( index,true));
                    else
                        DataJsonSet.SoundFoodDataDictionary.Add(jsonData[i]["ID"].ToString(), new SoundFoodDataType(index, false));
                }
                catch (Exception e)
                {
                    SundryUtil.ErrorAdd(i, "SoundFDEvents", e);
                }
            }
        }
        public override void IntegrityCheck()
        {
            int listindex = 0;
            foreach(SoundFoodDataType data in DataJsonSet.SoundFoodDataDictionary.Values)
            {
                if(DataJsonSet.SoundDataDictionary.Count <= data.Index)
                {
                    SundryUtil.ErrorAdd(listindex, "SoundFDEvents");
                }
                listindex++;
            }
        }
    }
}
