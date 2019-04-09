using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetData;

public class SheetLoadMgr : MonoBehaviour
{
    List<ErrorDataStructer> errorList = new List<ErrorDataStructer>();
    List<SheetLoad> SheetDataTypeList = new List<SheetLoad>(); //시트데이터들을 관리하는 리스트

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

    void DataAllLoad()
    {
        for (int index = 0; index < SheetDataTypeList.Count; index++)
        {
            SheetDataTypeList[index].SheetDataLoad();
        }
    }

    private void ErrorOutput()
    {
        for (int index = 0; index < errorList.Count; index++)
        {
            Debug.LogFormat("WorkSheetName: {0}\nSheetIndex: {1}", errorList[index].WorkSheetName, errorList[index].Index);
        }
    }

    public void ErrorAdd(int listIndex,string workSheetName)
    {
        errorList.Add(new ErrorDataStructer(listIndex, workSheetName));     //읽어들이기 실패시 에러리스트에 추가
    }
}
