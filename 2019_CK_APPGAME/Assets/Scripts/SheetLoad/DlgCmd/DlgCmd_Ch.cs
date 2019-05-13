namespace DialogCommand
{
    public sealed class DlgCmd_Ch : DlgCmd
    {
        private char ch;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                ch = value[0];
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story",e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().ChAppend(ch);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_Ch(this);
        }

        public DlgCmd_Ch()
        {
        }

        public DlgCmd_Ch(DlgCmd_Ch dlgCmd)
        {
            ch = dlgCmd.ch;
        }
    }
}