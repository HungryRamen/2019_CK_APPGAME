using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TitleUIMgr : MonoBehaviour
{
    public GameObject ButtonObj;
    public GameObject SoundObj;
    public GameObject CreditObj;
    public Button[] Buttons;
    public Slider[] sliders;
    private Coroutine scrollCorutine;
    //x -250
    private void Awake()
    {
        CreditObj = GameObject.FindWithTag("Credit");
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
        CreditObj.SetActive(false);
    }
    public void CreditOn()
    {
        CreditObj.SetActive(true);
        scrollCorutine = StartCoroutine(ScrollCredit(CreditObj.transform, 250.0f));
    }

    public void CreditOff()
    {
        CreditObj.SetActive(false);
        if (scrollCorutine != null)
            StopCoroutine(scrollCorutine);
    }

    IEnumerator ScrollCredit(Transform t,float speed)
    {
        t.localPosition = Vector2.zero;
        while(t.localPosition.y < 4320.0f)
        {
            yield return null;
            t.localPosition = new Vector2(t.localPosition.x, t.localPosition.y + speed * Time.deltaTime);
        }
        t.gameObject.SetActive(false);
    }

    private void Start()
    {
        Buttons = ButtonObj.GetComponentsInChildren<Button>();
        Buttons[1].interactable = SaveDataUtil.LoadCheckTitle();
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
        Buttons[1].interactable = SaveDataUtil.LoadCheckTitle();
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
