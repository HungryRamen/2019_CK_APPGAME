namespace DialogCommand
{
    public class DlgCmd_End : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }
        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().End();
        }
        public override DlgCmd Copy()
        {
            return new DlgCmd_End(this);
        }
        public DlgCmd_End()
        {

        }
        public DlgCmd_End(DlgCmd_End dlgCmd)
        {

        }
    }
}