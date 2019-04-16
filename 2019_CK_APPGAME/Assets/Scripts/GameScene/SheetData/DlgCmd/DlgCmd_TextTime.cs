using System;
namespace GameScene
{
    public sealed class DlgCmd_TextTime : DlgCmd
    {
        private float textOutputTime;

        public override void CommandAdd(string value)
        {
            if (value != "")
            {
                textOutputTime = Convert.ToSingle(value);
            }
            else
            {
                textOutputTime = 0.07f;
            }
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.GetuiMgrSingleton().textOutputTime = textOutputTime;
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_TextTime(this);
        }

        public DlgCmd_TextTime()
        {

        }

        public DlgCmd_TextTime(DlgCmd_TextTime dlgCmd)
        {
            textOutputTime = dlgCmd.textOutputTime;
        }
    }
}