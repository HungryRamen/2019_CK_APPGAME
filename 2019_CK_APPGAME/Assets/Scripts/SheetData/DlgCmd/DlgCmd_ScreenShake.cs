// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd_ScreenShake
using System;

public sealed class DlgCmd_ScreenShake : DlgCmd
{
    private float shakeTime;

    private float shakeOffSet;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        int num = value.IndexOf("::");
        int num2 = num + 2;
        string value2 = value.Substring(0, num);
        string value3 = value.Substring(num2, value.Length - num2);
        shakeTime = Convert.ToSingle(value2);
        shakeOffSet = Convert.ToSingle(value3);
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        if (!bPass)
        {
            uiManager.ScreenShake(shakeTime, shakeOffSet);
        }
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_ScreenShake(this);
    }

    public DlgCmd_ScreenShake()
        : base()
    {

    }

    public DlgCmd_ScreenShake(DlgCmd_ScreenShake dlgCmd)
        : base(dlgCmd)
    {
        shakeTime = dlgCmd.shakeTime;
        shakeOffSet = dlgCmd.shakeOffSet;
    }
}
