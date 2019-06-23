using SheetData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{

    public class PreferencesSound : MonoBehaviour
    {
        public Slider[] sliders;
        private void Awake()
        {
            transform.SetParent(GameObject.FindWithTag("UIMgr").transform);
            transform.localPosition = Vector2.zero;
            transform.localScale = Vector2.one;
            SoundMgr.SoundOnStart(ESoundSet.PopUp);
            sliders = GetComponentsInChildren<Slider>();
            float[] sounds;
            SaveDataUtil.SoundLoad(out sounds);
            for(int i =0; i <sounds.Length;i++)
            {
                sliders[i].value = sounds[i];
            }
            AmountSync();
        }

        private void AmountSync()
        {
            for (int i = 0; i < sliders.Length; i++)
            {
                SoundMgr.SoundMasterValueChange((ESoundType)i, sliders[i].value);
            }
        }

        public void AmountBGMChange()
        {
            SoundMgr.SoundMasterValueChange(ESoundType.BGM, sliders[0].value);
        }

        public void AmountAMBChange()
        {
            SoundMgr.SoundMasterValueChange(ESoundType.AMB, sliders[1].value);
        }

        public void AmountSFXChange()
        {
            SoundMgr.SoundMasterValueChange(ESoundType.SFX, sliders[2].value);
        }

        private void ConfigChage()
        {
            float[] sounds = new float[3] {
                sliders[0].value,
                sliders[1].value,
                sliders[2].value };
            SaveDataUtil.SoundSave(sounds);
        }

        public void PreferencesOff()
        {
            ConfigChage();
            SoundMgr.Stop(ESoundSet.PopUp);
            Destroy(gameObject);
        }

        public void GameEnd()
        {
            ConfigChage();
            SoundMgr.Stop(ESoundSet.PopUp);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => Quit());
        }

        public void MainGo()
        {
            ConfigChage();
            SoundMgr.Stop(ESoundSet.PopUp);
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene"));
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
