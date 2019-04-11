using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/OldDialogStory")]
    public static void CreateOldDialogStoryAssetFile()
    {
        OldDialogStory asset = CustomAssetUtility.CreateAsset<OldDialogStory>();
        asset.SheetName = "2019CKAPPGameSheet";
        asset.WorksheetName = "OldDialogStory";
        EditorUtility.SetDirty(asset);        
    }
    
}