using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace CreditScene
{

    public class CreditUIMgr : MonoBehaviour
    {
        private void Awake()
        {
            SoundMgr.SoundOnStart(SheetData.ESoundSet.CalenderMusic);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Credit"));
            obj.GetComponent<CreditTrigger>().OnEvent.AddListener(() => End());
        }

        public void End()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene"));
        }
    }
}
