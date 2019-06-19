using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Util
{
    public class SceneMgr : MonoBehaviour
    {
        private void Awake()
        {
            SetPos(transform);
        }
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            SetPos(transform);
            StartCoroutine(SceneFadeOut(gameObject, 1.5f));
        }
        private void SetPos(Transform t)
        {
            t.SetParent(GameObject.FindWithTag("UIMgr").transform);
            t.localPosition = Vector2.zero;
        }
        public void LoadScene(float time, System.Action func = null)
        {
            StartCoroutine(SceneFadeIn(gameObject, 1.5f, func));
        }
        IEnumerator SceneFadeIn(GameObject obj, float time, System.Action func)
        {
            Image temp = obj.GetComponent<Image>();
            Color color = temp.color;
            while (temp.color.a < 1.0f)
            {
                color.a += Time.deltaTime / time;
                if (color.a >= 1.0f)
                    color.a = 1.0f;
                temp.color = color;
                yield return null;
            }
            SoundMgr.SoundClear();
            GameObject obj2 = Instantiate(Resources.Load<GameObject>("Prefebs/FadeCopy"));
            SetPos(obj2.transform);
            obj.transform.SetParent(null);
            DontDestroyOnLoad(obj);
            func();
        }
        IEnumerator SceneFadeOut(GameObject obj, float time)
        {
            Image temp = obj.GetComponent<Image>();
            temp.color = Color.black;
            Color color = temp.color;
            while (temp.color.a > 0.0f)
            {
                color.a -= Time.deltaTime / time;
                if (color.a <= 0.0f)
                    color.a = 0.0f;
                temp.color = color;
                yield return null;
            }
            Destroy(obj);
        }
    }
}