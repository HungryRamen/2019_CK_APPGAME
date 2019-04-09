using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class SheetLoad : MonoBehaviour
{
    public SheetLoadMgr sheetManager;
    void Awake()
    {
        sheetManager = GetComponent<SheetLoadMgr>();
    }

    public virtual void SheetDataLoad()
    {

    }
}
