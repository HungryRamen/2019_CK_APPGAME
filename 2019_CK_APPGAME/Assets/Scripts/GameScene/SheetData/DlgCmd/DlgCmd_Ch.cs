namespace GameScene
{
    public sealed class DlgCmd_Ch : DlgCmd
    {
        private char ch;

        public override void CommandAdd(string value)
        {
            ch = value[0];
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.GetuiMgrSingleton().ChAppend(ch);
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