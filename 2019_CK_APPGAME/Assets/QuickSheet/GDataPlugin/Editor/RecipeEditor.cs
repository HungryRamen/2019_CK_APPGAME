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
[CustomEditor(typeof(Recipe))]
public class RecipeEditor : BaseGoogleEditor<Recipe>
{	    
    public override bool Load()
    {        
        Recipe targetData = target as Recipe;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<RecipeData>(targetData.WorksheetName) ?? db.CreateTable<RecipeData>(targetData.WorksheetName);
        
        List<RecipeData> myDataList = new List<RecipeData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            RecipeData data = new RecipeData();
            
            data = Cloner.DeepCopy<RecipeData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
