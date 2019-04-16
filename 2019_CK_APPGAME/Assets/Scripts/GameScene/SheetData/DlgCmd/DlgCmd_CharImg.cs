namespace GameScene
{
    public sealed class DlgCmd_CharImg : DlgCmd
    {
        private string id;

        private int state;

        public override void CommandAdd(string value)
        {
            int num = value.IndexOf("::");
            int num2 = num + 2;
            string text = value.Substring(0, num);
            string value2 = value.Substring(num2, value.Length - num2);
            id = text;
            state = System.Convert.ToInt32(value2);
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.GetuiMgrSingleton().NpcImageLoad(id, state);
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