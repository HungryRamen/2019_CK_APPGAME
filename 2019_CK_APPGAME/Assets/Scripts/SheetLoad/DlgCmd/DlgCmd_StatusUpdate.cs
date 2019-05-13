namespace DialogCommand
{

    public class DlgCmd_StatusUpdate : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {

        }
        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().StatusUpdate();
        }
        public override DlgCmd Copy()
        {
            return new DlgCmd_StatusUpdate(this);
        }
        public DlgCmd_StatusUpdate()
        {

        }
        public DlgCmd_StatusUpdate(DlgCmd_StatusUpdate dlgCmd)
        {
        }
    }
}
