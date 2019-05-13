namespace DialogCommand
{
    public sealed class DlgCmd_CharImg : DlgCmd
    {
        private string id;

        private int state;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                System.Collections.Generic.Queue<string> queue = Util.SundryUtil.CommandSubstring(value);
                id = queue.Dequeue();
                state = System.Convert.ToInt32(queue.Dequeue());
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().NpcImageLoad(id, state);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_CharImg(this);
        }

        public DlgCmd_CharImg()
        {

        }

        public DlgCmd_CharImg(DlgCmd_CharImg dlgCmd)
        {
            id = dlgCmd.id;
            state = dlgCmd.state;
        }
    }
}