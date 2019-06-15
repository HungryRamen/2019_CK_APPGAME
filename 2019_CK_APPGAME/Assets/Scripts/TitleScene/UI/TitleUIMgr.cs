using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TitleUIMgr : MonoBehaviour
{
    public GameObject ButtonObj;
    public GameObject SoundObj;
    public Button[] Buttons;
    public Slider[] sliders;
    //x -250
    private void Awake()
    {
        sliders = GetComponentsInChildren<Slider>();
        SoundMgr.SoundOnStart(SheetData.ESoundType.MainAmb);
        SoundMgr.SoundOnStart(SheetData.ESoundType.Mainmusic);
        float[] sounds;
        SaveDataUtil.SoundLoad(out sounds);
        for (int i = 0; i < sounds.Length; i++)
        {
            sliders[i].value = sounds[i];
        }
        for (int i = 0; i < sliders.Length; i++)
        {
            SoundMgr.SoundMasterValueChange(i, sliders[i].value);
        }
        SoundObj.SetActive(false);
    }
    private void Start()
    {
        Buttons = ButtonObj.GetComponentsInChildren<Button>();
        Buttons[1].interactable = SaveDataUtil.LoadCheckTitle();
    }

    public void AmountBGMChange()
    {
        SoundMgr.SoundMasterValueChange(0, sliders[0].value);
    }

    public void AmountAMBChange()
    {
        SoundMgr.SoundMasterValueChange(1, sliders[1].value);
    }

    public void AmountSFXChange()
    {
        SoundMgr.SoundMasterValueChange(2, sliders[2].value);
    }

    public void SoundSetting()
    {
        ButtonObj.transform.localPosition = new Vector2(-250.0f, 0.0f);
        SoundObj.SetActive(true);
        for(int i =0;i<Buttons.Length;i++)
        {
            Buttons[i].interactable = false;
        }
    }

    public void SoundSettingExit()
    {
        ButtonObj.transform.localPosition = Vector2.zero;
        SoundObj.SetActive(false);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].interactable = true;
        }
        ConfigChage();
    }
    public void GameSceneLoad()
    {
        SoundMgr.SoundClear();
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
        obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("NameScene"));
    }

    public void SaveSceneLoad()
    {
        SoundMgr.SoundClear();
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
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
