namespace DialogCommand
{
    public sealed class DlgCmd_TextSize : DlgCmd
    {
        private string textSize;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                if (value != "")
                {
                    textSize = value;
                }
                else
                {
                    textSize = "24";
                }
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().RichTextEditor("size", textSize);
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