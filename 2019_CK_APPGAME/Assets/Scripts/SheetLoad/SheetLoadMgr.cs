using System.Collections.Generic;
using UnityEngine;
using Util;
using System.IO;
using System.Text;

namespace SheetLoad
{
    public sealed class SheetLoadMgr : MonoBehaviour
    {
        private List<SheetLoad> SheetDataTypeList = new List<SheetLoad>();

        private void Awake()
        {
            DialogCommand.DlgCmdDictionary.Set();
            SheetDataTypeList.Add(new SheetLoad_DayEvents());
            SheetDataTypeList.Add(new SheetLoad_Trigger());
            SheetDataTypeList.Add(new SheetLoad_Char());
            SheetDataTypeList.Add(new SheetLoad_FoodData());
            SheetDataTypeList.Add(new SheetLoad_Dlg());
            SheetDataTypeList.Add(new SheetLoad_DialogReaction());
            DataAllLoad();
            ErrorOutput();
        }

        private void DataAllLoad()
        {
            for (int i = 0; i < SheetDataTypeList.Count; i++)
            {
                SheetDataTypeList[i].SheetDataLoad();
            }
        }

        private void ErrorOutput()
        {
            for (int i = 0; i < SundryUtil.errorList.Count; i++)
            {
                #if UNITY_EDITOR
                Debug.LogFormat("WorkSheetName: {0}\nSheetIndex: {1}", SundryUtil.errorList[i].WorkSheetName, SundryUtil.errorList[i].Index);
                #else
                string path = Application.dataPath + "/Error/";
                if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
                FileStream fs = new FileStream(path + "Error.txt", FileMode.Create, FileAccess.Write);
                StreamWriter wr = new StreamWriter(fs, Encoding.UTF8);

                wr.WriteLine("WorkSheetName: {0}\nSheetIndex: {1}", SundryUtil.errorList[i].WorkSheetName, SundryUtil.errorList[i].Index);
                #endif
            }
        }
    }
}