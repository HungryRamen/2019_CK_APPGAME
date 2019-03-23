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
[CustomEditor(typeof(DialogStory))]
public class DialogStoryEditor : BaseGoogleEditor<DialogStory>
{	    
    public override bool Load()
    {        
        DialogStory targetData = target as DialogStory;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<DialogStoryData>(targetData.WorksheetName) ?? db.CreateTable<DialogStoryData>(targetData.WorksheetName);
        
        List<DialogStoryData> myDataList = new List<DialogStoryData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            DialogStoryData data = new DialogStoryData();
            
            data = Cloner.DeepCopy<DialogStoryData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
