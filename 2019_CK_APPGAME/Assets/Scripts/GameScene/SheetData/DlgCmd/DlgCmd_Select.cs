
namespace GameScene
{
    public class DlgCmd_Select : DlgCmd
    {
        string buttonText;
        int index;

        public override void CommandAdd(string value)
        {
            int num = value.IndexOf("::");
            int num2 = num + 2;
            string text = value.Substring(0, num);
            string value2 = value.Substring(num2, value.Length - num2);
            buttonText = text;
            index = System.Convert.ToInt32(value2);
        }

        public override void CommandPerform(bool bPass)
        {
            MgrSingleton.uiMgrSingleton.CreateSelectBtn(buttonText, index);
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
