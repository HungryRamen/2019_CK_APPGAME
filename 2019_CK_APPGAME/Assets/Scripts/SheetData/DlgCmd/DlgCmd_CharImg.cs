// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd_CharImg
using System;

public sealed class DlgCmd_CharImg : DlgCmd
{
    private string id;

    private int state;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        int num = value.IndexOf("::");
        int num2 = num + 2;
        string text = value.Substring(0, num);
        string value2 = value.Substring(num2, value.Length - num2);
        id = text;
        state = Convert.ToInt32(value2);
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        uiManager.NpcImageLoad(id, state);
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_CharImg(this);
    }

    public DlgCmd_CharImg():
        base()
    {

    }

    public DlgCmd_CharImg(DlgCmd_CharImg dlgCmd)
        : base(dlgCmd)
    {
        id = dlgCmd.id;
        state = dlgCmd.state;
    }
}
