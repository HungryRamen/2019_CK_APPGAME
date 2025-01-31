﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SaveScene
{

    public class SaveUIMgr : MonoBehaviour
    {
        public GameObject[] datas;
        private bool isCheck = true;
        private void Awake()
        {
            Util.SoundMgr.SoundOnRelease(SheetData.ESoundSet.Page);
            for (int i = 0; i < datas.Length; i++)
            {
                string day;
                string dataTime;
                if (Util.SaveDataUtil.LoadCheck(datas[i].name, out day, out dataTime))
                {
                    datas[i].GetComponentInChildren<Text>().text = string.Format("DAY {0}\n{1}", day, dataTime);
                }
            }
        }

        public void SaveLoad(string indexPath)
        {
            isCheck = true;
            if (RunTimeData.RunTimeDataSet.isSaveLoad)  // true = Save
            {
                Util.SaveDataUtil.Save(indexPath);
            }
            else if (!RunTimeData.RunTimeDataSet.isSaveLoad) // false = Load
            {
                isCheck = Util.SaveDataUtil.Load(indexPath);
            }
            if (isCheck)
                Restroom();
        }

        public void Restroom()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene"));
        }

        public void Return()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene(RunTimeData.RunTimeDataSet.sceneChange));
        }
    }
}
