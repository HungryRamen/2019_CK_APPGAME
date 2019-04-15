// ILSpy5Preivew1 decompiler from Assembly-CSharp.dll class: SheetLoad
using UnityEngine;

public class SheetLoad : MonoBehaviour
{
    public SheetLoadMgr sheetManager;

    private void Awake()
    {
        sheetManager = GetComponent<SheetLoadMgr>();
    }

    public virtual void SheetDataLoad()
    {
    }
}
