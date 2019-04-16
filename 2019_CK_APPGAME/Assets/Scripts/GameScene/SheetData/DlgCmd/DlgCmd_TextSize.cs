namespace GameScene
{
    public sealed class DlgCmd_TextSize : DlgCmd
    {
        private string textSize;

        public override void CommandAdd(string value)
        {
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
            MgrSingleton.GetuiMgrSingleton().RichTextEditor("size", textSize);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_TextSize(this);
        }

        public DlgCmd_TextSize()
        {
        }

        public DlgCmd_TextSize(DlgCmd_TextSize dlgCmd)
        {
            textSize = dlgCmd.textSize;
        }
    }
}