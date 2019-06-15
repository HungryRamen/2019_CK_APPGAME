using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;
namespace SheetLoad
{

    public class SheetLoad_SoundEvents : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.SoundDataDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/SoundEvents");
            for (int i = 0; i < jsonData.Count; i++)
            {
                try
                {

                    string path = jsonData[i]["EventPath"].ToString();
                    string p1 = jsonData[i]["Parameter1"].ToString();
                    string p2 = jsonData[i]["Parameter2"].ToString();
                    string p3 = jsonData[i]["Parameter3"].ToString();

                    DataJsonSet.SoundDataDictionary.Add((ESoundType)i, new SoundDataType(path, p1, p2, p3));
                }
                catch(Exception e)
                {
                    SundryUtil.ErrorAdd(i, "SoundEvents", e);
                }
            }
        }

        public override void IntegrityCheck()
        {
        }
    }
}
