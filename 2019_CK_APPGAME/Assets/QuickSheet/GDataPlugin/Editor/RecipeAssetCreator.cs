using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Recipe")]
    public static void CreateRecipeAssetFile()
    {
        Recipe asset = CustomAssetUtility.CreateAsset<Recipe>();
        asset.SheetName = "2019CKAPPGameSheet";
        asset.WorksheetName = "Recipe";
        EditorUtility.SetDirty(asset);        
    }
    
}