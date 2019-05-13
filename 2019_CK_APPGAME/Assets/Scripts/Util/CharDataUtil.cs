using UnityEngine;
using CharData;
using LitJson;
using System.IO;
namespace Util
{
    public class CharDataUtil : MonoBehaviour
    {
        private void Awake()
        {
            if(!Load())
            {
                CharDataSet.charDataDictionary.Add("CH01", new CharDataStructure());
                CharDataSet.charDataDictionary.Add("CH02", new CharDataStructure());
                CharDataSet.charDataDictionary.Add("CH05", new CharDataStructure());
                CharDataSet.charDataDictionary.Add("CH06", new CharDataStructure());
                Save();
            }
        }

        bool Load()
        {
            CharDataSet.charDataDictionary.Clear();
            string path = Application.persistentDataPath + "/Save/001.json";
            if (!File.Exists(path))
            {
                return false;
            }
            string jsonstr = File.ReadAllText(path);
            JsonData CharJsonData = JsonMapper.ToObject(jsonstr);
            string[] s = new string[4];
            s[0] = "CH01";
            s[1] = "CH02";
            s[2] = "CH05";
            s[3] = "CH06";
            for(int i = 0; i <s.Length;i++)
            {
                CharDataSet.charDataDictionary.Add(s[i], new CharDataStructure(CharJsonData,s[i]));
            }
            return true;
        }
        void Save()
        {
            JsonData infojson = JsonMapper.ToJson(CharDataSet.charDataDictionary);
            string path = Application.persistentDataPath + "/Save/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            File.WriteAllText(path + "001.json", infojson.ToString());
        }
    }
}
