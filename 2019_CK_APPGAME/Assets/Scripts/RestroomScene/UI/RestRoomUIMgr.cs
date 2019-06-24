using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RestRoomScene
{
    public class RestRoomUIMgr : MonoBehaviour
    {
        public GameObject[] checks;

        private void Awake()
        {
            Util.SoundMgr.SoundOnRelease(SheetData.ESoundSet.Calender);
            Util.SoundMgr.SoundOnStart(SheetData.ESoundSet.CalenderMusic);
            Util.SoundMgr.SoundOnStart(SheetData.ESoundSet.CalenderAmb);
            int day = System.Convert.ToInt32(RunTimeData.RunTimeDataSet.day) - 1;
            for (int i = 0; i < day; i++)
            {
                checks[i].SetActive(true);
            }
        }

        public void SaveSceneLoad(bool bSaveLoad)
        {
            RunTimeData.RunTimeDataSet.isSaveLoad = bSaveLoad;
            RunTimeData.RunTimeDataSet.sceneChange = "RestroomScene";
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("SaveScene"));
        }

        public void GameSceneLoad()
        {
            GameObject obj;
            if (RunTimeData.RunTimeDataSet.day == "10")
            {
                obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
                obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("CreditScene"));
                return;
            }
            obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"));
        }
    }
}
