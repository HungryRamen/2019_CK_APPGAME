using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIMgr : MonoBehaviour
{
    public GameObject[] datas;
    private void Awake()
    {
        Util.SoundMgr.SoundOnRelease(SheetData.ESoundType.Page);
        for (int i = 0; i < datas.Length; i++)
        {
            string day;
            string dataTime;
            if(Util.SaveDataUtil.LoadCheck(datas[i].name,out day,out dataTime))
            {
                datas[i].GetComponentInChildren<Text>().text = string.Format("DAY {0}\n{1}", day, dataTime);
            }
        }
    }

    public void SaveLoad(string indexPath)
    {
        bool bCheck = true;
        if(RunTimeData.RunTimeDataSet.isSaveLoad)  // true = Save
        {
            Util.SaveDataUtil.Save(indexPath);
        }
        else if(!RunTimeData.RunTimeDataSet.isSaveLoad) // false = Load
        {
            bCheck = Util.SaveDataUtil.Load(indexPath);
        }
        if(bCheck)
            Return();
    }

    public void Return()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
        obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene"));

    }
}
