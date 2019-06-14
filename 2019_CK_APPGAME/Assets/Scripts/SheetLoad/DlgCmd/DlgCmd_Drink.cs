namespace DialogCommand
{
    public class DlgCmd_Drink : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().DrinkButtonOn();
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_Drink(this);
        }

        public DlgCmd_Drink()
        {
        }

        public DlgCmd_Drink(DlgCmd_Drink dlgCmd)
        {
        }

    }
}
