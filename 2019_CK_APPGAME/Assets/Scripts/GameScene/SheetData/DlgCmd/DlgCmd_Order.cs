namespace GameScene
{
    public sealed class DlgCmd_Order : DlgCmd
    {
        public override void CommandAdd(string value)
        {
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.GetuiMgrSingleton().btnCook.SetActive(true);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_Order(this);
        }

        public DlgCmd_Order()
        {

        }

        public DlgCmd_Order(DlgCmd_Order dlgCmd)
        {
        }
    }
}