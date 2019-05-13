namespace DialogCommand
{

    public class DlgCmd_ScreenShakeOff : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }

        public override void CommandPerform(bool bPass)
        {
            Util.ObjectShake.isScreenShake = false;
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_ScreenShakeOff(this);
        }

        public DlgCmd_ScreenShakeOff()
        {

        }

        public DlgCmd_ScreenShakeOff(DlgCmd_ScreenShakeOff dlgCmd)
        {
        }
    }
}
