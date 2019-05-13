namespace DialogCommand
{

    public class DlgCmd_CharJoin : DlgCmd
    {
        string id;
        int state;
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
            GameScene.UIMgr.GetUIMgr().NpcJoin(id, state);
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_CharJoin(this);
        }

        public DlgCmd_CharJoin()
        {

        }

        public DlgCmd_CharJoin(DlgCmd_CharJoin dlgCmd)
        {
            id = dlgCmd.id;
            state = dlgCmd.state;
        }
    }

}