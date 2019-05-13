namespace DialogCommand
{
    public sealed class DlgCmd_ScreenShakeOn : DlgCmd
    {
        private float shakeOffSet;

        public override void CommandAdd(string value, int index)
        {
            try
            {

                shakeOffSet = System.Convert.ToSingle(value);
            }
            catch (System.Exception e)
            {
                Util.SundryUtil.ErrorAdd(index, "Story", e);
            }
        }

        public override void CommandPerform(bool bPass)
        {
            if (!bPass)
            {
                GameScene.UIMgr.GetUIMgr().ScreenShake(shakeOffSet);
            }
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_ScreenShakeOn(this);
        }

        public DlgCmd_ScreenShakeOn()
        {

        }

        public DlgCmd_ScreenShakeOn(DlgCmd_ScreenShakeOn dlgCmd)
        {
            shakeOffSet = dlgCmd.shakeOffSet;
        }
    }
}