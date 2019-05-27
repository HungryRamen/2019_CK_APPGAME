namespace DialogCommand
{
    public sealed class DlgCmd_FoodMaterialUnLock : DlgCmd
    {
        string fmID;
        public override void CommandAdd(string value, int index)
        {
            try
            {
                fmID = value;
            }
            catch(System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().MaterialUnLock(fmID);
            GameScene.UIMgr.GetUIMgr().MaterialPopUp(fmID);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_FoodMaterialUnLock(this);
        }

        public DlgCmd_FoodMaterialUnLock()
        {

        }

        public DlgCmd_FoodMaterialUnLock(DlgCmd_FoodMaterialUnLock dlgCmd)
        {
            fmID = dlgCmd.fmID;
        }
    }
}