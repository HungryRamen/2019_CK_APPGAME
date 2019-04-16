namespace GameScene
{
    public sealed class DlgCmd_TextColor : DlgCmd
    {
        private string textColor;

        public override void CommandAdd(string value)
        {
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
            MgrSingleton.GetuiMgrSingleton().RichTextEditor("color", textColor);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_TextColor(this);
        }

        public DlgCmd_TextColor()
        {

        }

        public DlgCmd_TextColor(DlgCmd_TextColor dlgCmd)
        {
            textColor = dlgCmd.textColor;
        }
    }
}