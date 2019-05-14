using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using SheetData;
using Util;
using CharData;
using System.Collections;

namespace GameScene
{
    enum CHARIMG
    {
        CENTER,
        LEFT,
        RIGHT
    }

    public class UIMgr : MonoBehaviour
    {
        public int textIndex;

        public float textOutputTime;

        public bool bChAppend;

        public GameObject uiCook;

        public GameObject uiDialog;

        public GameObject btnCook;

        public GameObject btnEnd;

        public StringBuilder textStringBuilder;

        [Range(0.01f, 1f)]
        public float fadeTime = 0.01f;

        private static UIMgr uiMgrSingleton = null;

        private bool isBackGroundClick;

        private bool isStatusLayerInteraction;

        private int[] currentStatus = new int[5];

        private float[] singleStatus = new float[5];

        private Stack<TextStackType> textStack = new Stack<TextStackType>();

        private Queue<DayEventsType> eventsQueue = new Queue<DayEventsType>();

        public DayEventsType nowEvent;

        private Dictionary<string, string> textTypeDictionary = new Dictionary<string, string>();

        private GameObject[] charImgLayer;

        private GameObject[] charImg;

        private GameObject statusLayer;

        private GameObject[] statusArr;

        private List<GameObject> SelectBtnList = new List<GameObject>();

        private Coroutine runningCoroutine;

        private GameObject uiBlack;

        private void Awake()
        {
            uiCook = GameObject.FindWithTag("CookUI");
            uiDialog = GameObject.FindWithTag("DlgUI");
            uiBlack = GameObject.FindWithTag("Black");
            btnCook = GameObject.FindWithTag("CookBtn");
            btnEnd = GameObject.FindWithTag("EndBtn");
            statusLayer = GameObject.FindWithTag("StatusLayer");
            charImg = GameObject.FindGameObjectsWithTag("CharImg");
            charImgLayer = GameObject.FindGameObjectsWithTag("CharImgLayer");
            statusArr = GameObject.FindGameObjectsWithTag("Status");
            DialogLoadClassifcation(RunTimeData.RunTimeDataSet.day);
            uiCook.SetActive(false);
            uiBlack.SetActive(false);
            btnCook.SetActive(false);
            btnEnd.SetActive(false);
            for (int i = 0; i < charImg.Length; i++)
            {
                charImg[i].SetActive(false);
            }
            textStringBuilder = new StringBuilder();
            textIndex = 0;
            textOutputTime = 0f;
            fadeTime = 0.1f;
            bChAppend = false;
            isBackGroundClick = false;
            isStatusLayerInteraction = false;
            runningCoroutine = null;
            textTypeDictionary.Add("color", "size");
            textTypeDictionary.Add("size", "color");
        }

        // NPC이미지 설정
        private void NpcImageSet(GameObject dest, string id, int state)
        {
            dest.GetComponent<RawImage>().texture = Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
            dest.name = id;
            StartCoroutine(ObjectFade.ObjectFadeIn(dest, fadeTime));
            if (id == "CH05")
            {
                dest.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 800);
            }
            else
            {
                dest.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 800);
            }
            if (id == "CH01")
            {
                dest.transform.SetParent(charImgLayer[1].transform);
            }
            else
            {
                dest.transform.SetParent(charImgLayer[0].transform);
            }
        }

        // 덮어씌워질곳 덮어씌울오브젝트
        private void NpcImageOverwrite(GameObject dest, GameObject src)
        {
            dest.GetComponent<RawImage>().texture = src.GetComponent<RawImage>().texture;
            dest.name = src.name;
            dest.transform.SetParent(src.transform.parent);
            dest.GetComponent<RectTransform>().sizeDelta = src.GetComponent<RectTransform>().sizeDelta;
            StartCoroutine(ObjectFade.ObjectFadeIn(dest, fadeTime));
            StartCoroutine(ObjectFade.ObjectFadeOut(src, fadeTime));
        }

        // 대사 분류
        private void DialogLoadClassifcation(string day)
        {
            foreach (DayEventsType events in DataJsonSet.DayEventsDictionary[day])
            {
                if (CharDataSet.charDataDictionary.ContainsKey(events.CharID))
                {
                    if (CharDataSet.charDataDictionary[events.CharID].StoryState == events.StoryState)
                    {
                        eventsQueue.Enqueue(events);
                    }
                }
            }
        }

        public void FoodSet(string id)
        {
            RunTimeData.RunTimeDataSet.foodID = id;
        }

        private bool ButtonActiveCheck()
        {
            return btnCook.activeSelf || btnEnd.activeSelf;
        }

        public static UIMgr GetUIMgr()
        {
            if (uiMgrSingleton == null)
                uiMgrSingleton = FindObjectOfType(typeof(UIMgr)) as UIMgr;
            return uiMgrSingleton;
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
            RunTimeData.RunTimeDataSet.DayPlus();
            UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene");
        }

        public void NpcImageLoad(string id, int state)
        {
            for (int i = 0; i < charImg.Length; i++)
            {
                if (charImg[i].name == id && charImg[i].activeSelf)
                {
                    charImg[i].GetComponent<RawImage>().texture = Resources.Load(DataJsonSet.CharImageDictionary[id][state].ImagePath, typeof(Texture)) as Texture;
                }
            }
        }

        public void NpcJoin(string id, int state)
        {
            if (charImg[(int)CHARIMG.CENTER].activeSelf)
            {
                NpcImageOverwrite(charImg[(int)CHARIMG.LEFT], charImg[(int)CHARIMG.CENTER]);
                NpcImageSet(charImg[(int)CHARIMG.RIGHT], id, state);
            }
            else
            {
                NpcImageSet(charImg[(int)CHARIMG.CENTER], id, state);
            }
        }

        public void NpcExit(string id)
        {
            for (int i = 0; i < charImg.Length; i++)
            {
                if (charImg[i].name == id && charImg[i].activeSelf)
                {
                    if (i == (int)CHARIMG.LEFT)
                    {
                        NpcImageOverwrite(charImg[(int)CHARIMG.CENTER], charImg[(int)CHARIMG.RIGHT]);
                    }
                    else if (i == (int)CHARIMG.RIGHT)
                    {
                        NpcImageOverwrite(charImg[(int)CHARIMG.CENTER], charImg[(int)CHARIMG.LEFT]);
                    }
                    StartCoroutine(ObjectFade.ObjectFadeOut(charImg[i], fadeTime));
                }
            }
        }

        public void ScreenShake(float shakeOffSet)
        {
            StartCoroutine(ObjectShake.ScreenShake(uiDialog.transform, shakeOffSet));
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

        public void TextStackPush(List<TextTypeRaction> copy)
        {
            TextStackType temp = new TextStackType();
            temp.textTypeList = new List<TextType>();
            for (int i = 0; i < copy.Count; i++)
            {
                if (nowEvent.StoryState == copy[i].StoryState)
                    temp.textTypeList.Add(copy[i]);
            }
            textStack.Push(temp);

        }

        public void FoodPopUp()
        {
            BlackOnOff();
            Image[] img = uiBlack.GetComponentsInChildren<Image>();
            img[2].sprite = Resources.Load<Sprite>(DataJsonSet.FoodDataDictionary[RunTimeData.RunTimeDataSet.foodID][0].ImageLocation);
            Text[] text = uiBlack.GetComponentsInChildren<Text>();
            text[0].text = DataJsonSet.FoodDataDictionary[RunTimeData.RunTimeDataSet.foodID][0].Name;
            text[1].text = DataJsonSet.FoodDataDictionary[RunTimeData.RunTimeDataSet.foodID][0].Description;
        }

        public void BlackOnOff()
        {
            uiBlack.SetActive(!uiBlack.activeSelf);
        }

        public TextStackType NextText()
        {
            if (textStack.Count == 0 && eventsQueue.Count == 0)
            {
                return null;
            }
            else if (textStack.Count == 0 && eventsQueue.Count > 0)
            {
                nowEvent = eventsQueue.Dequeue();
                TextStackType temp = new TextStackType();
                temp.textTypeList = new List<TextType>(DataJsonSet.TextDictionary[nowEvent.DialogID]);
                textStack.Push(temp);
            }
            textStringBuilder.Clear();
            textIndex = 0;
            return textStack.Peek();
        }

        public void End()
        {
            if (textStack.Count > 0)
            {
                textStack.Peek().textTypeList = null;
                textStack.Pop();
            }
            foreach (TriggerType temp in DataJsonSet.TriggerDictionary[nowEvent.TriggerID])
            {
                if (temp.IsTrigger(nowEvent.CharID))
                {
                    for(int i = 0; i < temp.Status.Length; i++)
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].Status[i] += temp.Status[i];
                    }
                    if(temp.StoryState != -1)
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].StoryState = temp.StoryState;
                    }
                }
            }
            for (int i = 0; i < currentStatus.Length; i++)
            {
                currentStatus[i] = 0;
            }
            if (textStack.Count == 0 && eventsQueue.Count == 0)
            {
                btnEnd.SetActive(true);
            }
        }

        public void NextTextCount()
        {
            if (textStack.Peek().textTypeList.Count <= textStack.Peek().TextTypeIndex)
            {
                textStack.Peek().textTypeList = null;
                textStack.Pop();
            }
        }

        public void FoodBonus(string foodID, int[] status)
        {
            if (foodID == RunTimeData.RunTimeDataSet.foodID)
            {
                for (int i = 0; i < status.Length; i++)
                {
                    currentStatus[i] += status[i];
                }
            }
        }

        public void StatusUpdate()
        {
            for (int i = 0; i < currentStatus.Length; i++)
            {
                singleStatus[i] = CharDataSet.charDataDictionary[nowEvent.CharID].Status[i];
                float check = CharDataSet.charDataDictionary[nowEvent.CharID].Status[i] + currentStatus[i];
                if (check < 0)
                    check = 0;
                else if (check > 100)
                    check = 100;
                StartCoroutine(StatusLerp(check, 1.0f, i));
            }
        }


        private IEnumerator StatusLerp(float arrive, float speed, int index)
        {
            while (Mathf.Abs(arrive - singleStatus[index]) > 1f)
            {
                singleStatus[index] = Mathf.Lerp(singleStatus[index], arrive, speed * Time.deltaTime);
                StatusFillUpdate(index);
                yield return null;
            }
            CharDataSet.charDataDictionary[nowEvent.CharID].Status[index] = System.Convert.ToInt32(arrive);
            singleStatus[index] = System.Convert.ToInt32(arrive);
            StatusFillUpdate(index);
        }

        private void StatusFillUpdate(int index)
        {
            Image[] img = statusArr[index].GetComponentsInChildren<Image>();
            if (singleStatus[index] >= 50)
            {
                img[0].fillAmount = (singleStatus[index] - 50.0f) / 50.0f;
            }
            else
            {
                img[1].fillAmount = 1.0f - singleStatus[index] / 50.0f;
            }
        }

        public void IndexJump(int index)
        {
            if (textStack.Peek().textTypeList.Count > index)
            {
                textStack.Peek().TextTypeIndex = index;
            }
            foreach (GameObject obj in SelectBtnList)
            {
                Destroy(obj);
            }
            SelectBtnList.Clear();
        }

        public void BackGroundClick()
        {
            if (!ButtonActiveCheck() && SelectBtnList.Count <= 0)
                isBackGroundClick = true;
        }

        public void CreateSelectBtn(string btnText, int index)
        {
            SelectBtnList.Add(Instantiate(Resources.Load("Prefebs/SelectBtn")) as GameObject);
            int count = SelectBtnList.Count - 1;
            SelectBtnList[count].transform.SetParent(uiDialog.transform);
            SelectBtnList[count].transform.localPosition = new Vector2(655, -50 + (-80 * count));
            SelectBtnList[count].transform.localScale = new Vector2(1, 1);
            SelectBtnList[count].GetComponentInChildren<Text>().text = btnText;
            SelectBtnList[count].GetComponent<Button>().onClick.AddListener(() => IndexJump(index));
        }

        public bool ScreenReaction()
        {
            if (isBackGroundClick)
            {
                isBackGroundClick = false;
                return true;
            }
            else if (!ButtonActiveCheck() && SelectBtnList.Count <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    return true;
                }
            }
            return false;
        }

        public void StatusLayerUpDown()
        {
            if (runningCoroutine != null)
                StopCoroutine(runningCoroutine);
            if (isStatusLayerInteraction)
            {
                runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpY(statusLayer.transform, 810.0f, 5.0f));
            }
            else
            {
                runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpY(statusLayer.transform, 270.0f, 5.0f));
            }
            isStatusLayerInteraction = !isStatusLayerInteraction;
        }
    }
}
