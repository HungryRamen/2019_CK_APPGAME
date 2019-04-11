using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_TextSize : DlgCmd
{
    public override void CommandClass(TextSet textSet, string value)
    {
        base.CommandClass(textSet, value);
        if (value != "")                   //Default 확인
            textSet.TextSize = System.Convert.ToInt32(value);
        else
            textSet.TextSize = 40;
    }
}
