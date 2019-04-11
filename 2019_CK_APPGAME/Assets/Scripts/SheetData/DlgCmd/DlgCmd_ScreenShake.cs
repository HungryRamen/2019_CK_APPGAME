using System.Collections;
using System.Collections.Generic;
using SheetData;
using UnityEngine;

public class DlgCmd_ScreenShake : DlgCmd
{
    public override void CommandClass(TextSet textSet, string value)
    {
        base.CommandClass(textSet, value);
        int midFirstIndex = value.IndexOf("::");
        int midLastIndex = midFirstIndex + 2;
        string commandStr = value.Substring(0, midFirstIndex);
        string valueStr = value.Substring(midLastIndex, value.Length - midLastIndex);
        textSet.mCmdScreenShake.ScreenShakeTime = System.Convert.ToSingle(commandStr);
        textSet.mCmdScreenShake.ScreenShakeOffSet = System.Convert.ToSingle(valueStr);
        textSet.mCmdScreenShake.bOneTime = true;
    }
}
