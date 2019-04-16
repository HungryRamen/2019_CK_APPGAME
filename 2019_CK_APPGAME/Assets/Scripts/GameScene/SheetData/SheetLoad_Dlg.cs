using LitJson;
using SheetData;
using System;
using System.Collections.Generic;

namespace GameScene
{
    public sealed class SheetLoad_Dlg : SheetLoad
    {
        private Dictionary<string, DlgCmd> commandDictionary = new Dictionary<string, DlgCmd>();

        public override void SheetDataLoad()
        {
            DataJsonSet.TextDictionary.Clear();
            JsonData jsonData = SundryUtil.JsonDataLoad("/OldDialogStory");
            commandDictionary.Add("출력속도", new DlgCmd_TextTime());
            commandDictionary.Add("크기", new DlgCmd_TextSize());
            commandDictionary.Add("색상", new DlgCmd_TextColor());
            commandDictionary.Add("이미지", new DlgCmd_CharImg());
            commandDictionary.Add("화면흔들기", new DlgCmd_ScreenShake());
            commandDictionary.Add("주문", new DlgCmd_Order());
            commandDictionary.Add("ch", new DlgCmd_Ch());
            commandDictionary.Add("점프", new DlgCmd_Jump());
            commandDictionary.Add("선택지", new DlgCmd_Select());
            for (int i = 0; i < jsonData.Count; i++)
            {
                TextType textType = TextLoad(jsonData[i]["Command"].ToString(), Convert.ToInt32(jsonData[i]["Index"].ToString()), "DialogStroy");
                textType.TalkerName = jsonData[i]["TalkerName"].ToString();
                textType.CharID = jsonData[i]["CharID"].ToString();
                string key = jsonData[i]["ID"].ToString();
                if (!DataJsonSet.TextDictionary.ContainsKey(key))
                {
                    DataJsonSet.TextDictionary.Add(key, new List<TextType>());
                }
                DataJsonSet.TextDictionary[key].Add(textType);
            }
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
                        string[] array = CommandSubstring(str, i, num);
                        if (!commandDictionary.ContainsKey(array[0]))
                        {
                            SundryUtil.ErrorAdd(listIndex, workSheetName);
                            return textType;
                        }
                        commandDictionary[array[0]].CommandAdd(array[1]);
                        textType.mTextQueue.Enqueue(commandDictionary[array[0]].Copy());
                        i += num - i;
                    }
                    else
                    {
                            commandDictionary["ch"].CommandAdd(str[i].ToString());
                        textType.mTextQueue.Enqueue(commandDictionary["ch"].Copy());
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

        private string[] CommandSubstring(string str, int index, int lastIndex)
        {
            string[] array = new string[2];
            int num = index + 1;
            int num2 = str.IndexOf("::", index);
            if (num2 == -1)
            {
                array[0] = str.Substring(num, lastIndex - num);
                return array;
            }
            int num3 = num2 + 2;
            array[0] = str.Substring(num, num2 - num);
            array[1] = str.Substring(num3, lastIndex - num3);
            return array;
        }
    }

}