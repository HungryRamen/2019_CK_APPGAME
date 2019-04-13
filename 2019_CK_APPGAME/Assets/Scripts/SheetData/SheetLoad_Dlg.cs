using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SheetData;

public class SheetLoad_Dlg : SheetLoad
{
    private OldDialogStory dialogStroyResource;
    Dictionary<string, DlgCmd> commandDictionary = new Dictionary<string, DlgCmd>();
    public override void SheetDataLoad()
    {
        base.SheetDataLoad();
        dialogStroyResource = Resources.Load<OldDialogStory>("Data/SheetData/OldDialogStory");
        commandDictionary.Add("출력속도", GetComponent<DlgCmd_TextTime>());
        commandDictionary.Add("크기", GetComponent<DlgCmd_TextSize>());
        commandDictionary.Add("색상", GetComponent<DlgCmd_TextColor>());
        commandDictionary.Add("이미지", GetComponent<DlgCmd_CharImg>());
        commandDictionary.Add("화면흔들기", GetComponent<DlgCmd_ScreenShake>());
        commandDictionary.Add("주문", GetComponent<DlgCmd_Order>());
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
        int lastIndex;
        float tempTextTime;
        string[] strTypeClass; //커맨드 타입 분별
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
                    lastIndex = str.IndexOf("}", index);   //} 끝 인덱스
                    strTypeClass = CommandSubstring(str, index, lastIndex);
                    if(commandDictionary.ContainsKey(strTypeClass[0]))
                    {
                        commandDictionary[strTypeClass[0]].CommandClass(textSet, strTypeClass[1]);
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
                    tempTextTime = textSet.TextOutputTime;
                    if (str[index] == '`')  //다음줄로 이동 인식
                    {
                        if (str[++index] == 'ㄷ')
                        {
                            textSet.Ch = '\n';
                            textSet.TextOutputTime = 0.0f;
                        }
                        else
                        {
                            --index;
                        }
                    }
                    else
                        textSet.Ch = str[index];
                    textType.mTextQueue.Enqueue(new TextSet(textSet));  //텍스트큐에 문자넣기
                    textSet.TextOutputTime = tempTextTime;
                    textSet.mCmdScreenShake.bOneTime = false;
                }
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
        return textType;
    }

    string[] CommandSubstring(string str, int index, int lastIndex)
    {
        string[] tempStr = new string[2];
        int startIndex = index + 1;
        int midFirstIndex = str.IndexOf("::", index);   //:: 중간 첫인덱스
        if (midFirstIndex == -1)
        {
            tempStr[0] = str.Substring(startIndex, lastIndex - startIndex);
            return tempStr;
        }
        int midLastIndex = midFirstIndex + 2;      //:: 중간 끝인덱스
        tempStr[0] = str.Substring(startIndex, midFirstIndex - startIndex); //커맨드 타입 분별
        tempStr[1] = str.Substring(midLastIndex, lastIndex - midLastIndex);  //값 분별
        return tempStr;
    }
}
