namespace DialogCommand
{
    public abstract class DlgCmd
    {
        public virtual void CommandAdd(string value,int index)
        {
        }

        public virtual void CommandPerform(bool bPass)
        {
        }

        public virtual DlgCmd Copy()
        {
            return this;
        }

        public DlgCmd()
        {
        }

        public DlgCmd(DlgCmd dlgCmd)
        {
        }
    }
}