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
