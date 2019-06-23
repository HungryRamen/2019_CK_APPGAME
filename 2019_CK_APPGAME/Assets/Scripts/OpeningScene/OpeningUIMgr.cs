using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;


namespace OpningScene
{

    public class OpeningUIMgr : MonoBehaviour
    {
        private int index;
        private Image aniImage;
        private List<Sprite> sprites = new List<Sprite>();
        private void Awake()
        {
            index = 0;
            aniImage = GetComponentInChildren<Image>();
            for (int i = 2; i <= 3; i++)
            {
                sprites.Add(Resources.Load<Sprite>(string.Format("Scenes/Opening/{0}", i.ToString())));
            }
        }

        public void AniImageChange()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            if (index >= sprites.Count)
            {
                obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("NameScene"));
                return;
            }
            obj.GetComponent<SceneMgr>().FadeImage(1.0f, () => SpriteIndex());
        }

        public void SpriteIndex()
        {
            aniImage.sprite = sprites[index++];

        }
    }
}
