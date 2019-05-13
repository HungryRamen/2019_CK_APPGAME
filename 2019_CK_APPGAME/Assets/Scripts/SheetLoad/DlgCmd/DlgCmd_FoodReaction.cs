
namespace DialogCommand
{
    public sealed class DlgCmd_FoodReaction : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().FoodPopUp();
            GameScene.UIMgr.GetUIMgr().TextStackPush(SheetData.DataJsonSet.TextReactionDictionary[GameScene.UIMgr.GetUIMgr().nowEvent.CharID + "_" + RunTimeData.RunTimeDataSet.foodID]);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_FoodReaction(this);
        }

        public DlgCmd_FoodReaction()
        {

        }

        public DlgCmd_FoodReaction(DlgCmd_FoodReaction dlgCmd)
        {
        }
    }
}
