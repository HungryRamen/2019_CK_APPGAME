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
[CustomEditor(typeof(OldDialogStory))]
public class OldDialogStoryEditor : BaseGoogleEditor<OldDialogStory>
{	    
    public override bool Load()
    {        
        OldDialogStory targetData = target as OldDialogStory;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<OldDialogStoryData>(targetData.WorksheetName) ?? db.CreateTable<OldDialogStoryData>(targetData.WorksheetName);
        
        List<OldDialogStoryData> myDataList = new List<OldDialogStoryData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            OldDialogStoryData data = new OldDialogStoryData();
            
            data = Cloner.DeepCopy<OldDialogStoryData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
