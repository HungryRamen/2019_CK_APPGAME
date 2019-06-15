using UnityEngine;
using CharData;
using LitJson;
using System.IO;
using System.Collections.Generic;

namespace Util
{
    public class SaveData
    {
        public string day;
        public string dateTime;
        public string userName;
        public List<string> lockMaterilas;
        public Dictionary<string, CharDataStructure> dataDic;

    }

    public class ConfigData
    {
        public string[] sounds = new string[3];
    }

    public static class SaveDataUtil
    {
        public static void SoundLoad(out float[] sounds)
        {
            sounds = new float[3];
            string path = Application.persistentDataPath + "/config.json";
            if(!File.Exists(path))
            {
                for(int i = 0; i< sounds.Length;i++)
                {
                    sounds[i] = 1.0f;
                    SoundSave(sounds);
                }
            }
            string jsonStr = File.ReadAllText(path);
            JsonData jsonData = JsonMapper.ToObject(jsonStr);
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i] = System.Convert.ToSingle(jsonData["sounds"][i].ToString());
            }
        }

        public static void SoundSave(float[] sounds)
        {
            ConfigData data = new ConfigData();
            for(int i=0; i<sounds.Length;i++)
            {
                data.sounds[i] = sounds[i].ToString();
            }
            string path = Application.persistentDataPath + "/config.json";
            JsonData infojson = JsonMapper.ToJson(data);
            File.WriteAllText(path, infojson.ToString());
        }
        public static bool LoadCheck(string indexPath, out string day, out string dataTime)
        {
            string path = Application.persistentDataPath + "/Save/" + indexPath + ".json";
            if (!File.Exists(path))
            {
                day = null;
                dataTime = null;
                return false;
            }
            string jsonStr = File.ReadAllText(path);
            JsonData jsonData = JsonMapper.ToObject(jsonStr);
            day = jsonData["day"].ToString();
            dataTime = jsonData["dateTime"].ToString();
            return true;
        }

        public static bool LoadCheckTitle()
        {
            string path;
            for (int i = 1; i <= 20; i++)
            {
                if (i <= 9)
                    path = Application.persistentDataPath + "/Save/Data0" + i.ToString() + ".json";
                else
                    path = Application.persistentDataPath + "/Save/Data" + i.ToString() + ".json";
                if (File.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Load(string indexPath)
        {
            RunTimeData.RunTimeDataSet.lockMaterials.Clear();
            CharDataSet.charDataDictionary.Clear();
            string path = Application.persistentDataPath + "/Save/" + indexPath + ".json";
            if (!File.Exists(path))
            {
                return false;
            }
            string jsonstr = File.ReadAllText(path);
            JsonData jsonData = JsonMapper.ToObject(jsonstr);
            RunTimeData.RunTimeDataSet.day = jsonData["day"].ToString();
            RunTimeData.RunTimeDataSet.userName = jsonData["userName"].ToString();
            for(int i = 0; i < jsonData["lockMaterilas"].Count;i++)
            {
                RunTimeData.RunTimeDataSet.lockMaterials.Add(jsonData["lockMaterilas"][i].ToString());
            }
            string[] s = new string[5];
            s[0] = "CH01";
            s[1] = "CH02";
            s[2] = "CH03";
            s[3] = "CH05";
            s[4] = "CH06";
            for (int i = 0; i < s.Length; i++)
            {
                CharDataSet.charDataDictionary.Add(s[i], new CharDataStructure(jsonData, s[i]));
            }
            return true;
        }
        public static void Save(string indexPath)
        {
            SoundMgr.SoundOnRelease(SheetData.ESoundType.Save);
            SaveData saveData = new SaveData();
            saveData.day = RunTimeData.RunTimeDataSet.day;
            saveData.dateTime = System.DateTime.Now.ToString();
            saveData.userName = RunTimeData.RunTimeDataSet.userName;
            saveData.lockMaterilas = RunTimeData.RunTimeDataSet.lockMaterials;
            saveData.dataDic = CharDataSet.charDataDictionary;

            JsonData infojson = JsonMapper.ToJson(saveData);
            string path = Application.persistentDataPath + "/Save/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.WriteAllText(path + indexPath + ".json", infojson.ToString());
        }
    }
}
