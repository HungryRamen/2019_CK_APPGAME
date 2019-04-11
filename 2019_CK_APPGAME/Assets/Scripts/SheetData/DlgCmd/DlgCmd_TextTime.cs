using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_TextTime : DlgCmd
{
    public override void CommandClass(TextSet textSet,string value)
    {
        base.CommandClass(textSet, value);
        if (value != "")                   //Default 확인
            textSet.TextOutputTime = System.Convert.ToSingle(value);
        else
            textSet.TextOutputTime = 1.0f;
    }
}
