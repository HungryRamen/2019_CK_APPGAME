using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_TextColor : DlgCmd
{
    public override void CommandClass(TextSet textSet, string value)
    {
        base.CommandClass(textSet, value);
        if (value != "")                   //Default 확인
            textSet.TextColor = value;
        else
            textSet.TextColor = "black";
    }
}
