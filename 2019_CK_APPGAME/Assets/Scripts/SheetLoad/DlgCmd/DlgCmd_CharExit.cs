namespace DialogCommand
{
    public class DlgCmd_CharExit : DlgCmd
    {
        string id;
        public override void CommandAdd(string value, int index)
        {
            try
            {

                id = value;
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().NpcExit(id);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_CharExit(this);
        }

        public DlgCmd_CharExit()
        {

        }

        public DlgCmd_CharExit(DlgCmd_CharExit dlgCmd)
        {
            id = dlgCmd.id;
        }
    }
}