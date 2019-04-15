using System;

public sealed class DlgCmd_TextTime : DlgCmd
{
    private float textOutputTime;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        if (value != "")
        {
            textOutputTime = Convert.ToSingle(value);
        }
        else
        {
            textOutputTime = 0.07f;
        }
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        UIMgrSingleton.GetuiMgrSingleton().textOutputTime = textOutputTime;
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_TextTime(this);
    }

    public DlgCmd_TextTime():
        base()
    {

    }

    public DlgCmd_TextTime(DlgCmd_TextTime dlgCmd)
        : base(dlgCmd)
    {
        textOutputTime = dlgCmd.textOutputTime;
    }
}
