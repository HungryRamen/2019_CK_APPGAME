using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SheetData;
namespace Util
{
    public static class SundryUtil //기타 유틸
    {
        public static List<ErrorDataStructer> errorList = new List<ErrorDataStructer>();

        public static JsonData JsonDataLoad(string path)
        {
            string path2 = Application.dataPath + path + ".json";
            if (File.Exists(path2))
            {
                return JsonMapper.ToObject(File.ReadAllText(path2));
            }
            return JsonMapper.ToObject(Resources.Load<TextAsset>("Data/SheetData" + path).text);
        }

        public static void ErrorAdd(int listIndex, string workSheetName)
        {
            errorList.Add(new ErrorDataStructer(listIndex, workSheetName));
        }

        public static void ErrorAdd(int listIndex, string workSheetName,System.Exception e)
        {
            errorList.Add(new ErrorDataStructer(listIndex, workSheetName));
        }

        public static Queue<string> CommandSubstring(string str)
        {
            string temp = str + "::";
            Queue<string> queue = new Queue<string>();
            int num = temp.IndexOf("::");
            while (num != -1)
            {
                queue.Enqueue(temp.Substring(0, num));
                temp = temp.Substring(num + 2);
                num = temp.IndexOf("::");
            }
            return queue;
        }
    }
}