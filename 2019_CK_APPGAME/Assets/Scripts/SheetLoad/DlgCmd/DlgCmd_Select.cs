namespace DialogCommand
{
    public class DlgCmd_Select : DlgCmd
    {
        string buttonText;
        int index;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                System.Collections.Generic.Queue<string> queue = Util.SundryUtil.CommandSubstring(value);
                buttonText = queue.Dequeue();
                this.index = System.Convert.ToInt32(queue.Dequeue());
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().CreateSelectBtn(buttonText, index);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_Select(this);
        }

        public DlgCmd_Select()
        {

        }

        public DlgCmd_Select(DlgCmd_Select DlgCmd)
        {
            buttonText = DlgCmd.buttonText;
            index = DlgCmd.index;
        }
    }
}
