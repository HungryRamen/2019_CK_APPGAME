namespace DialogCommand
{
    public class DlgCmd_DayCheck : DlgCmd
    {
        private string day;
        private int index;
        public override void CommandAdd(string value, int index)
        {
            try
            {

                System.Collections.Generic.Queue<string> queue = Util.SundryUtil.CommandSubstring(value);
                day = queue.Dequeue();
                this.index = System.Convert.ToInt32(queue.Dequeue());
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }
        public override void CommandPerform(bool bPass)
        {
            if (day == RunTimeData.RunTimeDataSet.day)
                GameScene.UIMgr.GetUIMgr().IndexJump(index);
        }
        public override DlgCmd Copy()
        {
            return new DlgCmd_DayCheck(this);
        }
        public DlgCmd_DayCheck()
        {

        }
        public DlgCmd_DayCheck(DlgCmd_DayCheck dlgCmd)
        {
            day = dlgCmd.day;
            index = dlgCmd.index;
        }
    }
}
