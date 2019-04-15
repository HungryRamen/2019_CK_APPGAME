// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: SheetLoadMgr
using SheetData;
using System.Collections.Generic;
using UnityEngine;

public sealed class SheetLoadMgr : MonoBehaviour
{
    private List<ErrorDataStructer> errorList = new List<ErrorDataStructer>();

    private List<SheetLoad> SheetDataTypeList = new List<SheetLoad>();

    private void Awake()
    {
        SheetDataTypeList.Add(GetComponent<SheetLoad_Dlg>());
        SheetDataTypeList.Add(GetComponent<SheetLoad_Char>());
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
        for (int i = 0; i < errorList.Count; i++)
        {
            UnityEngine.Debug.LogFormat("WorkSheetName: {0}\nSheetIndex: {1}", errorList[i].WorkSheetName, errorList[i].Index);
        }
    }

    public void ErrorAdd(int listIndex, string workSheetName)
    {
        errorList.Add(new ErrorDataStructer(listIndex, workSheetName));
    }
}
