namespace GameScene
{
    public class DlgCmd_Jump : DlgCmd
    {
        int index;
        public override void CommandAdd(string value)
        {
            index = System.Convert.ToInt32(value);
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.uiMgrSingleton.IndexJump(index);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_Jump(this);
        }

        public DlgCmd_Jump()
        {
        }

        public DlgCmd_Jump(DlgCmd_Jump dlgCmd)
        {
            index = dlgCmd.index;
        }
    }

}