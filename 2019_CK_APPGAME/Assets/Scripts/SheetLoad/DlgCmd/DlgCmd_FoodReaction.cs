
using System.Collections.Generic;

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
            string chID = GameScene.UIMgr.GetUIMgr().nowEvent.CharID;
            string foodID = CharData.CharDataSet.charDataDictionary[chID].EatFoodID;
            string drinkID = CharData.CharDataSet.charDataDictionary[chID].DrinkID;
            GameScene.UIMgr.GetUIMgr().TextStackPush(SheetData.DataJsonSet.TextReactionDictionary[chID + "_" + foodID]);
            bool isCheck = CharData.CharDataSet.charDataDictionary[chID].combinationCheck.Contains(chID + foodID + drinkID);
            string boolStr = "F";
            if (isCheck)
                boolStr = "T";
            string cbdID = string.Format("{0}_{1}_{2}{3}", chID, foodID, drinkID, boolStr);
            if (SheetData.DataJsonSet.TextReactionDictionary.ContainsKey(cbdID))
            {
                GameScene.UIMgr.GetUIMgr().TextStackPush(SheetData.DataJsonSet.TextReactionDictionary[cbdID]);
                if (!isCheck)
                    CharData.CharDataSet.charDataDictionary[chID].combinationCheck.Add(chID + foodID + drinkID);
            }
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
