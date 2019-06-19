using System.Collections.Generic;
using UnityEngine;
using Util;
using System.IO;
using System.Text;

namespace SheetLoad
{
    public sealed class SheetLoadMgr : MonoBehaviour
    {
        private List<SheetLoad> SheetDataTypeList = new List<SheetLoad>();
        public static bool isLoad = false;
        private void Awake()
        {

            DialogCommand.DlgCmdDictionary.Set();
            SheetDataTypeList.Add(new SheetLoad_DayEvents());
            SheetDataTypeList.Add(new SheetLoad_Trigger());
            SheetDataTypeList.Add(new SheetLoad_Char());
            SheetDataTypeList.Add(new SheetLoad_FoodData());
            SheetDataTypeList.Add(new SheetLoad_MaterialData());
            SheetDataTypeList.Add(new SheetLoad_CookData());
            SheetDataTypeList.Add(new SheetLoad_Recipe());
            SheetDataTypeList.Add(new SheetLoad_Status());
            //SheetDataTypeList.Add(new SheetLoad_Dlg());
            //SheetDataTypeList.Add(new SheetLoad_DialogReaction());
            SheetDataTypeList.Add(new SheetLoad_DialogCombination());
            SheetDataTypeList.Add(new SheetLoad_SoundEvents());
            SheetDataTypeList.Add(new SheetLoad_SoundFoodEvents());
            DataAllLoad();
            IntegrityAllCheck();
            SundryUtil.ErrorOutput();
            float[] sounds;
            SaveDataUtil.SoundLoad(out sounds);
            RunTimeData.RunTimeDataSet.SoundVolumeOn(sounds);
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }

        private void DataAllLoad()
        {
            for (int i = 0; i < SheetDataTypeList.Count; i++)
            {
                SheetDataTypeList[i].SheetDataLoad();
            }
        }

        private void IntegrityAllCheck() // 참조 무결성 검사 
        {
            for (int i = 0; i < SheetDataTypeList.Count; i++)
            {
                SheetDataTypeList[i].IntegrityCheck();
            }
        }
    }
}