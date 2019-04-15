using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIMgrSingleton
{
    public static UIMgr uiMgrSingleton;

    public static UIMgr GetuiMgrSingleton()
    {
        if (uiMgrSingleton == null)
            uiMgrSingleton = GameObject.FindWithTag("UIMgr").GetComponent<UIMgr>();
        return uiMgrSingleton;
    }
}
