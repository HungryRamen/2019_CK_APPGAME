using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Char_Script))]
public class Char_ScriptEditor : BaseGoogleEditor<Char_Script>
{	    
    public override bool Load()
    {        
        Char_Script targetData = target as Char_Script;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<Char_ScriptData>(targetData.WorksheetName) ?? db.CreateTable<Char_ScriptData>(targetData.WorksheetName);
        
        List<Char_ScriptData> myDataList = new List<Char_ScriptData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            Char_ScriptData data = new Char_ScriptData();
            
            data = Cloner.DeepCopy<Char_ScriptData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
