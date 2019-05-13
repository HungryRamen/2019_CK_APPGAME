using System;
namespace DialogCommand
{
    public sealed class DlgCmd_TextTime : DlgCmd
    {
        private float textOutputTime;

        public override void CommandAdd(string value, int index)
        {
            try
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
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story",e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            GameScene.UIMgr.GetUIMgr().textOutputTime = textOutputTime;
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