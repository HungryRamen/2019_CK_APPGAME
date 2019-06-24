using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace TitleScene
{

    public class TitleUIMgr : MonoBehaviour
    {
        public GameObject ButtonObj;
        public GameObject SoundObj;
        public ButtonTrigger[] Buttons;
        public Slider[] sliders;
        //x -250
        private void Awake()
        {
            sliders = GetComponentsInChildren<Slider>();
            float[] sounds;
            SaveDataUtil.SoundLoad(out sounds);
            for (int i = 0; i < sounds.Length; i++)
            {
                sliders[i].value = sounds[i];
            }
            for (int i = 0; i < sliders.Length; i++)
            {
                SoundMgr.SoundMasterValueChange((SheetData.ESoundType)i, sliders[i].value);
            }
            SoundMgr.SoundOnStart(SheetData.ESoundSet.MainAmb);
            SoundMgr.SoundOnStart(SheetData.ESoundSet.Mainmusic);
            SoundObj.SetActive(false);
        }
        public void CreditOn()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("CreditScene"));
        }

        private void Start()
        {
            Buttons = ButtonObj.GetComponentsInChildren<ButtonTrigger>();
            if (!SaveDataUtil.LoadCheckTitle())
                Buttons[1].InteractableOff();
        }

        public void AmountBGMChange()
        {
            SoundMgr.SoundMasterValueChange(SheetData.ESoundType.BGM, sliders[0].value);
        }

        public void AmountAMBChange()
        {
            SoundMgr.SoundMasterValueChange(SheetData.ESoundType.AMB, sliders[1].value);
        }

        public void AmountSFXChange()
        {
            SoundMgr.SoundMasterValueChange(SheetData.ESoundType.SFX, sliders[2].value);
        }

        public void SoundSetting()
        {
            ButtonObj.transform.localPosition = new Vector2(-250.0f, 0.0f);
            SoundObj.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].InteractableOff();
            }
            Buttons[Buttons.Length - 1].gameObject.SetActive(false);
        }

        public void SoundSettingExit()
        {
            ButtonObj.transform.localPosition = Vector2.zero;
            SoundObj.SetActive(false);
            Buttons[Buttons.Length - 1].gameObject.SetActive(true);
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].InteractableOn();
            }
            if (!SaveDataUtil.LoadCheckTitle())
                Buttons[1].InteractableOff();
            ConfigChage();
        }
        public void GameSceneLoad()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("OpeningScene"));
        }

        public void SaveSceneLoad()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            RunTimeData.RunTimeDataSet.sceneChange = "TitleScene";
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("SaveScene"));
        }

        public void Quit()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => Exit());

        }
        private void ConfigChage()
        {
            float[] sounds = new float[3] {
                sliders[0].value,
                sliders[1].value,
                sliders[2].value };
            SaveDataUtil.SoundSave(sounds);
        }

        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
