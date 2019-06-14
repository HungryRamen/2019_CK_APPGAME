using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;
using DialogCommand;

namespace SheetLoad
{
    public sealed class SheetLoad_Dlg : SheetLoad
    {
        public override void SheetDataLoad()
        {
            DataJsonSet.TextDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/DialogStory");
            for (int i = 0; i < jsonData.Count; i++)
            {
                TextType textType = TextLoad(jsonData[i]["Command"].ToString(), i, "DialogStroy");
                textType.TalkerName = jsonData[i]["TalkerName"].ToString();
                textType.CharId = jsonData[i]["CharID"].ToString();
                textType.Index = Convert.ToInt32(jsonData[i]["Index"].ToString());
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.TextDictionary.ContainsKey(key))
                {
                    DataJsonSet.TextDictionary.Add(key, new List<TextType>());
                }
                DataJsonSet.TextDictionary[key].Add(textType);
            }
        }

        public override void IntegrityCheck()
        {
        }

        public TextType TextLoad(string str, int listIndex, string workSheetName)
        {
            TextType textType = new TextType
            {
                Index = listIndex
            };
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '{')
                    {
                        int num = str.IndexOf("}", i);
                        string[] array = DlgCmdDictionary.CommandSubstring(str, i, num);
                        if (!DlgCmdDictionary.commandDictionary.ContainsKey(array[0]))
                        {
                            SundryUtil.ErrorAdd(listIndex, workSheetName);
                            //return textType;
                        }
                        else
                        {
                            DlgCmdDictionary.commandDictionary[array[0]].CommandAdd(array[1],listIndex);
                            textType.textQueue.Enqueue(DlgCmdDictionary.commandDictionary[array[0]].Copy());
                        }
                        i += num - i;
                    }
                    else
                    {
                        DlgCmdDictionary.commandDictionary["ch"].CommandAdd(str[i].ToString(),listIndex);
                        textType.textQueue.Enqueue(DlgCmdDictionary.commandDictionary["ch"].Copy());
                    }
                }
                return textType;
            }
            catch (NullReferenceException message)
            {
                UnityEngine.Debug.Log(message);
                return textType;
            }
        }
    }

}