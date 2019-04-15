// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd_Ch
public sealed class DlgCmd_Ch : DlgCmd
{
    private char ch;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        ch = value[0];
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        uiManager.ChAppend(ch);
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_Ch(this);
    }

    public DlgCmd_Ch() :
    base()
    {
    }

    public DlgCmd_Ch(DlgCmd_Ch dlgCmd)
        : base(dlgCmd)
    {
        ch = dlgCmd.ch;
    }
}
