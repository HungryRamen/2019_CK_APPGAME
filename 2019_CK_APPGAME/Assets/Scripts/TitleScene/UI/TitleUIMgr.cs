using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIMgr : MonoBehaviour
{
    public void GameSceneLoad()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene");
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
