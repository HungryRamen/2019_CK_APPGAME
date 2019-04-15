// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd_TextSize
public sealed class DlgCmd_TextSize : DlgCmd
{
    private string textSize;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        if (value != "")
        {
            textSize = value;
        }
        else
        {
            textSize = "40";
        }
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        uiManager.RichTextEditor("size", textSize);
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_TextSize(this);
    }

    public DlgCmd_TextSize() :
        base()
    {
    }

    public DlgCmd_TextSize(DlgCmd_TextSize dlgCmd)
        : base(dlgCmd)
    {
        textSize = dlgCmd.textSize;
    }
}
