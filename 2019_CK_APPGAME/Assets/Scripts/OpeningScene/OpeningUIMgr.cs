using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class OpeningUIMgr : MonoBehaviour
{
    private int index;
    private Image aniImage;
    private List<Sprite> sprites = new List<Sprite>();
    private void Awake()
    {
        index = 0;
        aniImage = GetComponentInChildren<Image>();
        for(int i =2;i<=3;i++)
        {
            sprites.Add(Resources.Load<Sprite>(string.Format("Scenes/Opening/{0}", i.ToString())));
        }
    }

    public void AniImageChange()
    {
        if(index >= sprites.Count)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("NameScene"));
            return;
        }
        aniImage.sprite = sprites[index++];
    }
}
