﻿using LitJson;
using SheetData;
using System;
using System.Collections.Generic;
using Util;
using DialogCommand;

namespace SheetLoad
{

    public sealed class SheetLoad_DialogReaction : SheetLoad
    {
        public override void SheetDataLoad()
        {
           //DataJsonSet.TextReactionDictionary.Clear();
           //JsonData jsonData = SundryUtil.JsonDataLoad("/DialogReaction");
           //for (int i = 0; i < jsonData.Count; i++)
           //{
           //    //TextTypeRaction textTypeReaction = TextLoad(jsonData[i]["Command"].ToString(), Convert.ToInt32(jsonData[i]["Index"].ToString()), "DialogReaction");
           //    //textTypeReaction.TalkerName = jsonData[i]["TalkerName"].ToString();
           //    //textTypeReaction.StoryState = Convert.ToInt32(jsonData[i]["StoryState"].ToString());
           //    //string key = jsonData[i]["ID"].ToString();
           //    //textTypeReaction.CharId = key.Substring(0, 4);
           //    //if (!DataJsonSet.TextReactionDictionary.ContainsKey(key))
           //    //{
           //    //    DataJsonSet.TextReactionDictionary.Add(key, new List<TextTypeRaction>());
           //    //}
           //    //DataJsonSet.TextReactionDictionary[key].Add(textTypeReaction);
           //}
        }

        public void SheetDataCharIDLoad(string charID)
        {
            JsonData jsonData = SundryUtil.JsonDataLoad("/DialogReaction");
            int startIndex, endIndex;
            SheetLoad_DialogReactionIndex temp = new SheetLoad_DialogReactionIndex();
            temp.IndexGet(charID, out startIndex, out endIndex);
            for (int i = startIndex; i <= endIndex; i++)
            {
                TextTypeRaction textTypeReaction = TextLoad(jsonData[i]["Command"].ToString(), Convert.ToInt32(jsonData[i]["Index"].ToString()), "DialogReaction");
                textTypeReaction.TalkerName = jsonData[i]["TalkerName"].ToString();
                textTypeReaction.StoryState = Convert.ToInt32(jsonData[i]["StoryState"].ToString());
                string key = jsonData[i]["ID"].ToString();
                textTypeReaction.CharId = key.Substring(0, 4);
                if (!DataJsonSet.TextReactionDictionary.ContainsKey(key))
                {
                    DataJsonSet.TextReactionDictionary.Add(key, new List<TextTypeRaction>());
                }
                DataJsonSet.TextReactionDictionary[key].Add(textTypeReaction);
            }
        }

        public override void IntegrityCheck()
        {
        }

        public TextTypeRaction TextLoad(string str, int listIndex, string workSheetName)
        {
            TextTypeRaction textTypeReaction = new TextTypeRaction
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
                            textTypeReaction.textQueue.Enqueue(DlgCmdDictionary.commandDictionary[array[0]].Copy());
                        }
                        i += num - i;
                    }
                    else
                    {
                        DlgCmdDictionary.commandDictionary["ch"].CommandAdd(str[i].ToString(),listIndex);
                        textTypeReaction.textQueue.Enqueue(DlgCmdDictionary.commandDictionary["ch"].Copy());
                    }
                }
                return textTypeReaction;
            }
            catch (NullReferenceException message)
            {
                UnityEngine.Debug.Log(message);
                return textTypeReaction;
            }
        }
    }
}
