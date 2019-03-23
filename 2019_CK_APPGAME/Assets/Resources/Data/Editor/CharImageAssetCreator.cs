using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/CharImage")]
    public static void CreateCharImageAssetFile()
    {
        CharImage asset = CustomAssetUtility.CreateAsset<CharImage>();
        asset.SheetName = "2019CKAPPGameSheet";
        asset.WorksheetName = "CharImage";
        EditorUtility.SetDirty(asset);        
    }
    
}