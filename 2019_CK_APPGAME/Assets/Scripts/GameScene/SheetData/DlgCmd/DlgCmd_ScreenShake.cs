namespace GameScene
{
    public sealed class DlgCmd_ScreenShake : DlgCmd
    {
        private float shakeTime;

        private float shakeOffSet;

        public override void CommandAdd(string value)
        {
            int num = value.IndexOf("::");
            int num2 = num + 2;
            string value2 = value.Substring(0, num);
            string value3 = value.Substring(num2, value.Length - num2);
            shakeTime = System.Convert.ToSingle(value2);
            shakeOffSet = System.Convert.ToSingle(value3);
        }

        public override void CommandPerform(bool bPass)
        {
            if (!bPass)
            {
                MgrSingleton.GetuiMgrSingleton().ScreenShake(shakeTime, shakeOffSet);
            }
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_ScreenShake(this);
        }

        public DlgCmd_ScreenShake()
        {

        }

        public DlgCmd_ScreenShake(DlgCmd_ScreenShake dlgCmd)
        {
            shakeTime = dlgCmd.shakeTime;
            shakeOffSet = dlgCmd.shakeOffSet;
        }
    }
}