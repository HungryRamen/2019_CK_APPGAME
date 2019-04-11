using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_CharImg : DlgCmd
{
    public override void CommandClass(TextSet textSet, string value)
    {
        base.CommandClass(textSet, value);
        //if (value == "")
        //{
        //    sheetManager.ErrorAdd(listIndex, workSheetName);
        //    break;
        //}
        int midFirstIndex = value.IndexOf("::");
        int midLastIndex = midFirstIndex + 2;
        string commandStr = value.Substring(0, midFirstIndex);
        string valueStr = value.Substring(midLastIndex, value.Length - midLastIndex);
        textSet.mCmdCharImg.ID = commandStr;
        textSet.mCmdCharImg.State = System.Convert.ToInt32(valueStr);
    }
}
