namespace DialogCommand
{
    public class DlgCmd_FoodBonus : DlgCmd
    {
        string foodID;
        int[] status = new int[5];
        public override void CommandAdd(string value, int index)
        {
            try
            {

                System.Collections.Generic.Queue<string> queue = Util.SundryUtil.CommandSubstring(value);
                foodID = queue.Dequeue();
                for (int i = 0; i < status.Length; i++)
                {
                    status[i] = System.Convert.ToInt32(queue.Dequeue());
                }
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }
        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().FoodBonus(foodID, status);
        }
        public override DlgCmd Copy()
        {
            return new DlgCmd_FoodBonus(this);
        }
        public DlgCmd_FoodBonus()
        {

        }
        public DlgCmd_FoodBonus(DlgCmd_FoodBonus dlgCmd)
        {
            foodID = dlgCmd.foodID;
            status = dlgCmd.status;
        }
    }
}
