public sealed class DlgCmd_TextColor : DlgCmd
{
    private string textColor;

    public override void CommandAdd(string value)
    {
        base.CommandAdd(value);
        if (value != "")
        {
            textColor = value;
        }
        else
        {
            textColor = "black";
        }
    }

    public override void CommandPerform(bool bPass)
    {
        base.CommandPerform(bPass);
        UIMgrSingleton.GetuiMgrSingleton().RichTextEditor("color",textColor);
    }

    public override DlgCmd Copy()
    {
        return new DlgCmd_TextColor(this);
    }

    public DlgCmd_TextColor():
        base()
    {

    }

    public DlgCmd_TextColor(DlgCmd_TextColor dlgCmd)
        : base(dlgCmd)
    {
        textColor = dlgCmd.textColor;
    }
}
