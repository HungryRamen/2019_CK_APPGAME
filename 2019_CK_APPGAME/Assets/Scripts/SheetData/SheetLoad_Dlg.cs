using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SheetData;

public class SheetLoad_Dlg : SheetLoad
{
    private DialogStory dialogStroyResource;

    public override void SheetDataLoad()
    {
        base.SheetDataLoad();
        dialogStroyResource = Resources.Load<DialogStory>("Data/SheetData/DialogStory");
        for (int index = 0; index < dialogStroyResource.dataArray.Length; index++)
        {
            //텍스트딕셔너리리스트에 TextType 셋팅 
            TextType textType = TextLoad(
                dialogStroyResource.dataArray[index].Command,
                dialogStroyResource.dataArray[index].Index,
                dialogStroyResource.WorksheetName);
            textType.TalkerName = dialogStroyResource.dataArray[index].Talkername;
            textType.CharID = dialogStroyResource.dataArray[index].Charid;
            //텍스트딕셔너리에 기존 ID 값이 있는지 확인 없으면 새로 생성
            if (!DataSheetSet.TextDictionary.ContainsKey(dialogStroyResource.dataArray[index].ID))
            {
                DataSheetSet.TextDictionary.Add(dialogStroyResource.dataArray[index].ID, new List<TextType>());
            }
            DataSheetSet.TextDictionary[dialogStroyResource.dataArray[index].ID].Add(textType);
        }
    }

    //커맨드String, 워크시트인덱스, 워크시트이름
    public TextType TextLoad(string str, int listIndex, string workSheetName)
    {
        int startIndex; //시작인덱스 { = 1
        int midFirstIndex;   //:: 중간 첫인덱스
        int midLastIndex;
        int lastIndex;   //} 끝 인덱스
        string commandStr; //커맨드 타입 분별
        string valueStr;
        TextType textType = new TextType
        {
            Index = listIndex
        };
        TextSet textSet = new TextSet();
        try
        {
            for (int index = 0; index < str.Length; index++)
            {
                if (str[index] == '{')
                {
                    startIndex = index + 1; //시작인덱스 { = 1
                    midFirstIndex = str.IndexOf("::", index);   //:: 중간 첫인덱스
                    midLastIndex = midFirstIndex + 2;      //:: 중간 끝인덱스
                    lastIndex = str.IndexOf("}", index);   //} 끝 인덱스
                    commandStr = str.Substring(startIndex, midFirstIndex - startIndex); //커맨드 타입 분별
                    valueStr = str.Substring(midLastIndex, lastIndex - midLastIndex);  //값 분별
                    if (commandStr == "출력속도")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextOutputTime = System.Convert.ToSingle(valueStr);
                        else
                            textSet.TextOutputTime = 1.0f;
                    }
                    else if (commandStr == "크기")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextSize = System.Convert.ToInt32(valueStr);
                        else
                            textSet.TextSize = 40;
                    }
                    else if (commandStr == "색상")
                    {
                        if (valueStr != "")                   //Default 확인
                            textSet.TextColor = valueStr;
                        else
                            textSet.TextColor = "black";
                    }
                    else if (commandStr == "이미지")
                    {
                        if (valueStr == "")
                        {
                            sheetManager.ErrorAdd(listIndex, workSheetName);
                            break;
                        }
                        midFirstIndex = valueStr.IndexOf("::");
                        midLastIndex = midFirstIndex + 2;
                        commandStr = valueStr.Substring(0, midFirstIndex);
                        valueStr = valueStr.Substring(midLastIndex, valueStr.Length - midLastIndex);
                        textSet.mCmdCharImg.ID = commandStr;
                        textSet.mCmdCharImg.State = System.Convert.ToInt32(valueStr);
                    }
                    else
                    {
                        sheetManager.ErrorAdd(listIndex, workSheetName);
                        break;
                    }
                    index += lastIndex - index;          //인식한 커맨드 문장 건너뛰기
                }
                else
                {
                    if (str[index] == '`')  //다음줄로 이동 인식
                    {
                        if (str[++index] == 'ㄷ')
                        {
                            textSet.Ch = '\n';
                        }
                        else
                        {
                            --index;
                        }
                    }
                    else
                        textSet.Ch = str[index];
                    textType.mTextQueue.Enqueue(new TextSet(textSet));  //텍스트큐에 문자넣기
                }
            }
        }
        catch(NullReferenceException ex)
        {
            Debug.Log(ex);
        }
        return textType;
    }


}
