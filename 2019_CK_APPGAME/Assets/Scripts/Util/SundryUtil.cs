using LitJson;
using System.IO;
using UnityEngine;

public static class SundryUtil
{
    public static JsonData JsonDataLoad(string path)
    {
        string path2 = Application.dataPath + path + ".json";
        if (File.Exists(path2))
        {
            return JsonMapper.ToObject(File.ReadAllText(path2));
        }
        return JsonMapper.ToObject(Resources.Load<TextAsset>("Data/SheetData" + path).text);
    }
}
