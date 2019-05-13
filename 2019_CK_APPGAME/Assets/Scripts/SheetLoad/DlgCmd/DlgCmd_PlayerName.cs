namespace DialogCommand
{
    public sealed class DlgCmd_PlayerName : DlgCmd
    {
        public override void CommandAdd(string value, int index)
        {
        }

        public override void CommandPerform(bool bPass)
        {
            for(int i = 0; i < RunTimeData.RunTimeDataSet.userName.Length; i++)
            {
                GameScene.UIMgr.GetUIMgr().ChAppend(RunTimeData.RunTimeDataSet.userName[i]);

            }
        }

        public override DlgCmd Copy()
        {
            return new DlgCmd_PlayerName(this);
        }

        public DlgCmd_PlayerName()
        {

        }

        public DlgCmd_PlayerName(DlgCmd_PlayerName dlgCmd)
        {
        }
    }
}