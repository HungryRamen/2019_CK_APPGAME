using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    public sealed class SheetLoadMgr : MonoBehaviour
    {
        private List<SheetLoad> SheetDataTypeList = new List<SheetLoad>();

        private void Awake()
        {
            SheetDataTypeList.Add(new SheetLoad_Dlg());
            SheetDataTypeList.Add(new SheetLoad_Char());
        }

        private void Start()
        {
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
                Debug.LogFormat("WorkSheetName: {0}\nSheetIndex: {1}", SundryUtil.errorList[i].WorkSheetName, SundryUtil.errorList[i].Index);
            }
        }
    }
}