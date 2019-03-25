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
[CustomEditor(typeof(CharImage))]
public class CharImageEditor : BaseGoogleEditor<CharImage>
{	    
    public override bool Load()
    {        
        CharImage targetData = target as CharImage;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<CharImageData>(targetData.WorksheetName) ?? db.CreateTable<CharImageData>(targetData.WorksheetName);
        
        List<CharImageData> myDataList = new List<CharImageData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            CharImageData data = new CharImageData();
            
            data = Cloner.DeepCopy<CharImageData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
