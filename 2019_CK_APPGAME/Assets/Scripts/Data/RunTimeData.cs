using System;
using System.Collections.Generic;

namespace RunTimeData
{ 
    public static class RunTimeDataSet
    {
        public static string day = "1";
        public static string userName = "김성훈";

        public static List<string> lockMaterials = new List<string>();

        public static string cookID = "";

        public static bool isSaveLoad = false;

        public static Dictionary<SheetData.ESoundType,float> soundVolumeDic = new Dictionary<SheetData.ESoundType, float>();

        public static string sceneChange;
        public static void SoundVolumeOn(float[] value)
        {
            soundVolumeDic.Add(SheetData.ESoundType.None, 1.0f);
            for(int i = 0; i < value.Length;i++)
            {
                soundVolumeDic.Add((SheetData.ESoundType)i, value[i]);
            }
        }

        public static void DayPlus()
        {
            day = (Convert.ToInt32(day) + 1).ToString();
        }
        public static void DaySet(int d)
        {
            day = d.ToString();
        }
        public static void DaySet(string d)
        {
            day = d;
        }
    }
}
