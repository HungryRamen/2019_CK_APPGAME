using UnityEngine;
using GameScene;
public static class MgrSingleton
{
    public static UIMgr uiMgrSingleton;
    public static UIMgr GetuiMgrSingleton()
    {
        if (uiMgrSingleton == null)
            uiMgrSingleton = GameObject.FindWithTag("UIMgr").GetComponent<UIMgr>();
        return uiMgrSingleton;
    }
}
