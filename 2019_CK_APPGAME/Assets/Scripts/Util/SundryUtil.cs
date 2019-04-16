using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SheetData;
public static class SundryUtil
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
}
