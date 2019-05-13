namespace DialogCommand
{
    public class DlgCmd_Jump : DlgCmd
    {
        int index;
        public override void CommandAdd(string value, int index)
        {
            try
            {

                this.index = System.Convert.ToInt32(value);
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().IndexJump(index);
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