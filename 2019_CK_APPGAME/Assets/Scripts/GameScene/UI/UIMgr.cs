using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SheetData;

namespace GameScene
{
    public class UIMgr : MonoBehaviour
    {
        public int textIndex;

        public int textListQueueIndex;

        public float textOutputTime;

        public bool bChAppend;

        public GameObject uiCook;

        public GameObject uiDialog;

        public GameObject btnCook;

        public GameObject imageNpcSong;

        public GameObject imageNpcJack;

        public StringBuilder textStringBuilder;

        private bool bBackGroundClick;

        private List<TextType> textListQueue;

        private Dictionary<string, string> textTypeDictionary = new Dictionary<string, string>();

        private List<GameObject> SelectBtnList = new List<GameObject>();

        private void Awake()
        {
            uiCook = GameObject.FindWithTag("CookUI");
            uiDialog = GameObject.FindWithTag("DlgUI");
            btnCook = GameObject.FindWithTag("CookBtn");
            uiCook.SetActive(!uiCook.activeSelf);
            btnCook.SetActive(!btnCook.activeSelf);
            textStringBuilder = new StringBuilder();
            textIndex = 0;
            textListQueueIndex = 0;
            textOutputTime = 0f;
            bChAppend = false;
            bBackGroundClick = false;
            textTypeDictionary.Add("color", "size");
            textTypeDictionary.Add("size", "color");
        }

        public void ChangeUI()
        {
            if (uiCook.activeSelf)
            {
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
                btnCook.SetActive(!btnCook.activeSelf);
            }
            else
            {
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
            }
        }

        public void RestroomSceneLoad()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene");
        }

        public void NpcImageLoad(string id, int state)
        {
            if (id == "CH_02")
            {
                imageNpcSong.GetComponent<RawImage>().texture = Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                imageNpcSong.SetActive(value: true);
                imageNpcJack.SetActive(value: false);
            }
            else if (id == "CH_04")
            {
                imageNpcJack.GetComponent<RawImage>().texture = Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                imageNpcJack.SetActive(value: true);
                imageNpcSong.SetActive(value: false);
            }
        }

        public void ScreenShake(float shakeTime, float shakeOffSet)
        {
            StartCoroutine(ObjectShake.Shake(uiDialog.transform, shakeTime, shakeOffSet));
        }

        public void RichTextEditor(string type, string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<{0}={1}>", type, value);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendFormat("</{0}>", type);
            int num = textStringBuilder.ToString().IndexOf(stringBuilder2.ToString(), textIndex);
            if (num >= 0)
            {
                textIndex += num + stringBuilder2.Length - textIndex;
            }
            textStringBuilder.Insert(textIndex++, stringBuilder);
            textStringBuilder.Append(stringBuilder2);
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.AppendFormat("</{0}>", textTypeDictionary[type]);
            num = textStringBuilder.ToString().IndexOf(stringBuilder3.ToString(), textIndex);
            if (num >= 0)
            {
                textStringBuilder.Remove(num, stringBuilder3.Length);
                textStringBuilder.Append(stringBuilder3);
            }
            textIndex += textStringBuilder.ToString().IndexOf(">", textIndex) + 1 - textIndex;
        }

        public void ChAppend(char ch)
        {
            textStringBuilder.Insert(textIndex++, ch);
            bChAppend = true;
        }

        public List<TextType> NextText()
        {
            if (textListQueue == null)
            {
                textListQueue = new List<TextType>(DataJsonSet.TextDictionary["C00_D00"]);
                textListQueueIndex = 0;
            }
            textStringBuilder.Clear();
            textIndex = 0;
            return textListQueue;
        }

        public void NextTextCount()
        {
            if (textListQueue.Count <= textListQueueIndex)
            {
                textListQueue = null;
            }
        }

        public void IndexJump(int index)
        {
            if (textListQueue.Count > index)
            {
                textListQueueIndex = index;
            }
            foreach (GameObject obj in SelectBtnList)
            {
                Destroy(obj);
            }
            SelectBtnList.Clear();
        }

        public void BackGroundClick()
        {
            if (!btnCook.activeSelf && SelectBtnList.Count <= 0)
                bBackGroundClick = true;
        }

        public void CreateSelectBtn(string btnText, int index)
        {
            SelectBtnList.Add(Instantiate(Resources.Load("Prefebs/SelectBtn")) as GameObject);
            int count = SelectBtnList.Count - 1;
            SelectBtnList[count].transform.SetParent(uiDialog.transform);
            SelectBtnList[count].transform.localPosition = new Vector2(400, 480 - (80 * count));
            SelectBtnList[count].transform.localScale = new Vector2(1, 1);
            SelectBtnList[count].GetComponentInChildren<Text>().text = btnText;
            SelectBtnList[count].GetComponent<Button>().onClick.AddListener(() => IndexJump(index));
        }

        public bool ScreenReaction()
        {
            if (bBackGroundClick)
            {
                bBackGroundClick = false;
                return true;
            }
            else if (!btnCook.activeSelf && SelectBtnList.Count <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
