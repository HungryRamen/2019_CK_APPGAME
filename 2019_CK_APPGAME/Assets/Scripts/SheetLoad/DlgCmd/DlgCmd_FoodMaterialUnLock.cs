namespace DialogCommand
{
    public sealed class DlgCmd_FoodMaterialUnLock : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }

        public override void CommandPerform(bool bPass)
        {
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
        }
    }
}