namespace DialogCommand
{
    public sealed class DlgCmd_TextColor : DlgCmd
    {
        private string textColor;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                if (value != "")
                {
                    textColor = value;
                }
                else
                {
                    textColor = "white";
                }
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().RichTextEditor("color", textColor);
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