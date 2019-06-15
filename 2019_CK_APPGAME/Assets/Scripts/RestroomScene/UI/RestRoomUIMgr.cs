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
            Util.SoundMgr.SoundOnRelease(SheetData.ESoundType.Calender);
            Util.SoundMgr.SoundOnStart(SheetData.ESoundType.CalenderMusic);
            Util.SoundMgr.SoundOnStart(SheetData.ESoundType.CalenderAmb);
            int day = System.Convert.ToInt32(RunTimeData.RunTimeDataSet.day) - 1;
            for (int i = 0; i < day; i++)
            {
                checks[i].SetActive(true);
            }
        }

        public void SaveSceneLoad(bool bSaveLoad)
        {
            Util.SoundMgr.SoundClear();
            RunTimeData.RunTimeDataSet.isSaveLoad = bSaveLoad;
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("SaveScene"));
        }

        public void GameSceneLoad()
        {
            Util.SoundMgr.SoundClear();
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"));
        }
    }
}
