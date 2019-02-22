using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Char_Script")]
    public static void CreateChar_ScriptAssetFile()
    {
        Char_Script asset = CustomAssetUtility.CreateAsset<Char_Script>();
        asset.SheetName = "2019CKAPPGameSheet";
        asset.WorksheetName = "Char_Script";
        EditorUtility.SetDirty(asset);        
    }
    
}