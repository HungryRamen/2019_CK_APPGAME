using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetData;

public class SheetLoad_Char : SheetLoad
{
    private CharImage charImageResource;

    public override void SheetDataLoad()
    {
        base.SheetDataLoad();
        charImageResource = Resources.Load<CharImage>("Data/SheetData/CharImage");
        for (int index = 0; index < charImageResource.dataArray.Length; index++)
        {
            if (!DataSheetSet.CharImageDictionary.ContainsKey(charImageResource.dataArray[index].ID))
            {
                DataSheetSet.CharImageDictionary.Add(charImageResource.dataArray[index].ID, new List<CharImageType>());
            }
            DataSheetSet.CharImageDictionary[charImageResource.dataArray[index].ID].Add(new CharImageType(
            charImageResource.dataArray[index].State,
            charImageResource.dataArray[index].Imagelocation,
            charImageResource.dataArray[index].Name));
        }
    }
}
