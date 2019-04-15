// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd_Order
public sealed class DlgCmd_Order : DlgCmd
{
    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        uiManager.btnCook.SetActive(value: true);
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_Order(this);
    }

    public DlgCmd_Order():
        base()
    {

    }

    public DlgCmd_Order(DlgCmd_Order dlgCmd)
        : base(dlgCmd)
    {
    }
}
