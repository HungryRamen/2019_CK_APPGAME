using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/DialogStory")]
    public static void CreateDialogStoryAssetFile()
    {
        DialogStory asset = CustomAssetUtility.CreateAsset<DialogStory>();
        asset.SheetName = "2019CKAPPGameSheet";
        asset.WorksheetName = "DialogStory";
        EditorUtility.SetDirty(asset);        
    }
    
}