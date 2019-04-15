// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: DlgCmd
using UnityEngine;

public abstract class DlgCmd
{
    public UIMgr uiManager;

    public virtual void CommandAdd(string value)
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
        uiManager = GameObject.FindWithTag("UIMgr").GetComponent<UIMgr>();
    }

    public DlgCmd(DlgCmd dlgCmd)
    {
        uiManager = dlgCmd.uiManager;
    }
}
