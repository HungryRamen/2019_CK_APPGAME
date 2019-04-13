using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_Order : DlgCmd
{
    public override void CommandClass(TextSet textSet, string value)
    {
        base.CommandClass(textSet, value);
        textSet.IsOrderButtonEnable = true;
    }
}
