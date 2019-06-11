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

        public int logTextIndex;

        public int textOverFlowIndex;

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

        public bool isAutoPlay;

        private Dictionary<string, string> textTypeDictionary = new Dictionary<string, string>();

        private GameObject[] charImgLayer;

        public GameObject[] charImg;

        private GameObject statusLayer;

        private GameObject[] statusArr;

        private List<GameObject> SelectBtnList = new List<GameObject>();

        private Coroutine runningCoroutine;

        public GameObject uiBlack;

        public GameObject uiLog;

        private GameObject uiLogOff;

        private GameObject uiLogPanel;

        private GameObject uiLogScrollBar;

        private GameObject uiLogContent;

        private Scrollbar logScrollBar;

        private GameObject materialImgLeft;

        private GameObject materialImgRight;

        private GameObject foodMaterialButton;

        private Dictionary<string, FoodMaterialButtonMgr> foodMaterialButtonDic = new Dictionary<string, FoodMaterialButtonMgr>();

        private Dictionary<string, Image> cookButtonDic = new Dictionary<string, Image>();

        private string[] foodMaterialSelectID = new string[2]; //선택된 음식재료 ID 0은왼쪽 1은오른쪽

        private bool[] foodMaterialSelectOn = new bool[2]; //선택위에 들어와있는지 없는지

        private GameObject cookButton;

        private GameObject cookImageObject;

        private StringBuilder logStringBuilder = new StringBuilder();

        //추후 사운드 매니저 혹은 리스트로 관리

        [FMODUnity.EventRef]
        public string eventPath;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance restaurant;           // 이벤트 주소로 생성할 임시객체
        public FMOD.Studio.ParameterInstance CookType;      // 이벤트 파라미터 변수
        public FMOD.Studio.ParameterInstance Perspective;      // 이벤트 파라미터 변수

        [FMODUnity.EventRef]
        public string eventPath2;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance voice;           // 이벤트 주소로 생성할 임시객체
        public FMOD.Studio.ParameterInstance accent;      // 이벤트 파라미터 변수


        [FMODUnity.EventRef]
        public string eventPath3;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance entry;           // 이벤트 주소로 생성할 임시객체
        public FMOD.Studio.ParameterInstance DoorState;      // 이벤트 파라미터 변수

        [FMODUnity.EventRef]
        public string eventPath4;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance unlock;           // 이벤트 주소로 생성할 임시객체

        [FMODUnity.EventRef]
        public string eventPath5;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance finish;           // 이벤트 주소로 생성할 임시객체

        [FMODUnity.EventRef]
        public string eventPath6;    // 재생할 이벤트 주소
        public FMOD.Studio.EventInstance serve;           // 이벤트 주소로 생성할 임시객체

        private void Awake()
        {
            uiCook = GameObject.FindWithTag("CookUI");
            uiDialog = GameObject.FindWithTag("DlgUI");
            //uiBlack = GameObject.FindWithTag("Black");
            btnCook = GameObject.FindWithTag("CookBtn");
            btnEnd = GameObject.FindWithTag("EndBtn");
            statusLayer = GameObject.FindWithTag("StatusLayer");
            materialImgLeft = GameObject.FindWithTag("FMLeft");
            materialImgRight = GameObject.FindWithTag("FMRight");
            foodMaterialButton = GameObject.FindWithTag("FMBtns");
            cookButton = GameObject.FindWithTag("CookBtns");
            cookImageObject = GameObject.FindWithTag("CookImage");
            uiLogOff = GameObject.FindWithTag("LogOff");
            uiLogPanel = GameObject.FindWithTag("LogPanel");
            uiLogScrollBar = GameObject.FindWithTag("LogScrollBar");
            uiLogContent = GameObject.FindWithTag("LogContent");
            logScrollBar = uiLogScrollBar.GetComponent<Scrollbar>();
            FoodMaterialButtonMgr[] temp = foodMaterialButton.GetComponentsInChildren<FoodMaterialButtonMgr>();
            for (int i = 0; i < temp.Length; i++)
            {
                foodMaterialButtonDic.Add(temp[i].name, temp[i]);
            }
            Image[] temp2 = cookButton.GetComponentsInChildren<Image>();
            for (int i = 0; i < temp2.Length; i++)
            {
                cookButtonDic.Add(temp2[i].name, temp2[i]);
            }
            charImg = GameObject.FindGameObjectsWithTag("CharImg");
            charImgLayer = GameObject.FindGameObjectsWithTag("CharImgLayer");
            statusArr = GameObject.FindGameObjectsWithTag("Status");
            DialogLoadClassifcation(RunTimeData.RunTimeDataSet.day);
            uiCook.SetActive(false);
            uiBlack.SetActive(false);
            btnCook.SetActive(false);
            btnEnd.SetActive(false);
            materialImgLeft.SetActive(false);
            materialImgRight.SetActive(false);
            for (int i = 0; i < charImg.Length; i++)
            {
                charImg[i].SetActive(false);
            }

            isAutoPlay = true;
            textStringBuilder = new StringBuilder();
            textIndex = 0;
            logTextIndex = 0;
            textOverFlowIndex = 0;
            textOutputTime = 0f;
            fadeTime = 0.1f;
            bChAppend = false;
            isBackGroundClick = false;
            isStatusLayerInteraction = false;
            runningCoroutine = null;
            textTypeDictionary.Add("color", "size");
            textTypeDictionary.Add("size", "color");
            if (RunTimeData.RunTimeDataSet.lockMaterials.Count == 0)     //땜빵 코드 추후에 수정 int 비트플래그로 하도록하자
            {
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM05");
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM07");
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM11");
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM12");
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM13");
                RunTimeData.RunTimeDataSet.lockMaterials.Add("FM14");
            }
            //Texture2D[] spritesTemp = Resources.LoadAll<Texture2D>("UI/Dialog/DialogSpriteSheet");

            //Cursor.SetCursor(spritesTemp[0], Vector2.zero, CursorMode.Auto);
        }

        private void Start()
        {
            restaurant = FMODUnity.RuntimeManager.CreateInstance(eventPath);   // 이벤트 주소를 참조하여 객체 생성
            restaurant.getParameter("CookType", out CookType);         // 임시객체의 파라미터와 파라미터 변수 연동
            restaurant.getParameter("Perspective", out Perspective);         // 임시객체의 파라미터와 파라미터 변수 연동
            restaurant.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            restaurant.start();            // 객체 활성화(재생)

            entry = FMODUnity.RuntimeManager.CreateInstance(eventPath3);   // 이벤트 주소를 참조하여 객체 생성
            entry.getParameter("DoorState", out DoorState);         // 임시객체의 파라미터와 파라미터 변수 연동
            entry.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            entry.start();            // 객체 활성화(재생)
        }

        public void NpcEntry()
        {
            float temp;
            DoorState.getValue(out temp);
            if (temp == 1)
                DoorState.setValue(0);
            DoorState.setValue(1);
        }

        public void MaterialEnterOn(string tag)
        {
            if (tag == "FMLeft")
            {
                foodMaterialSelectOn[0] = true;
            }
            else if (tag == "FMRight")
            {
                foodMaterialSelectOn[1] = true;
            }
        }

        public void MaterialEnterOff(string tag)
        {
            if (tag == "FMLeft")
            {
                foodMaterialSelectOn[0] = false;
            }
            else if (tag == "FMRight")
            {
                foodMaterialSelectOn[1] = false;
            }
        }

        public void MaterialUnLock(string fmID)
        {
            if (RunTimeData.RunTimeDataSet.lockMaterials.Contains(fmID))
                RunTimeData.RunTimeDataSet.lockMaterials.Remove(fmID);
        }

        public void DragImagCheck(string fm, Sprite materialSprite)
        {
            if (foodMaterialSelectOn[0])
            {
                foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
                {
                    obj.SetState(ESpriteState.Disable);
                }
                materialImgLeft.GetComponent<Image>().sprite = materialSprite;
                materialImgLeft.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                materialImgLeft.SetActive(true);
                foodMaterialSelectID[0] = fm;
                foodMaterialSelectOn[0] = false;
                if (!materialImgRight.activeSelf)
                    RecipeIDCheck(fm);
                else
                    FoodSet();
            }
            else if (foodMaterialSelectOn[1])
            {
                foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
                {
                    obj.SetState(ESpriteState.Disable);
                }
                materialImgRight.GetComponent<Image>().sprite = materialSprite;
                materialImgRight.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                materialImgRight.SetActive(true);
                foodMaterialSelectID[1] = fm;
                foodMaterialSelectOn[1] = false;
                if (!materialImgLeft.activeSelf)
                    RecipeIDCheck(fm);
                else
                    FoodSet();
            }
        }


        public void MaterialRightClickSelect(string fm, Sprite materialSprite)
        {
            foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
            {
                obj.SetState(ESpriteState.Disable);
            }
            if (materialImgLeft.activeSelf && materialImgRight.activeSelf == false)  //왼쪽에 재료가있고 오른쪽에 재료가 없는경우
            {
                materialImgRight.GetComponent<Image>().sprite = materialSprite;
                materialImgRight.SetActive(true);
                foodMaterialSelectID[1] = fm;
                foodMaterialSelectOn[1] = false;
                FoodSet();
            }
            else if (materialImgLeft.activeSelf == false)   //왼쪽에 재료가 없는경우
            {
                materialImgLeft.GetComponent<Image>().sprite = materialSprite;
                materialImgLeft.SetActive(true);
                foodMaterialSelectID[0] = fm;
                foodMaterialSelectOn[0] = false;
                RecipeIDCheck(fm);
            }
        }

        public void CookChangeSelect(string name)
        {
            MaterialClear();
            foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
            {
                obj.SetState(ESpriteState.Disable);
            }
            Sprite temp = cookButtonDic[name].sprite;
            cookButtonDic[name].sprite = cookImageObject.GetComponent<Image>().sprite;
            cookImageObject.GetComponent<Image>().sprite = temp;
            RecipeIDCheck(CookImageCheck(temp));
        }

        public void CookStartSet()
        {
            MaterialClear();
            foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
            {
                obj.SetState(ESpriteState.Disable);
            }
            RecipeIDCheck("C1");
        }

        private string CookImageCheck(Sprite temp)
        {
            if (temp == Resources.Load<Sprite>(DataJsonSet.CookDataDictionary["C1"].ImageLocation))
            {
                return "C1";
            }
            else if (temp == Resources.Load<Sprite>(DataJsonSet.CookDataDictionary["C2"].ImageLocation))
            {
                return "C2";
            }
            else if (temp == Resources.Load<Sprite>(DataJsonSet.CookDataDictionary["C3"].ImageLocation))
            {
                return "C3";
            }
            return "C4";
        }

        public void MaterialClear()
        {
            MaterialOff("FMLeft");
            MaterialOff("FMRight");
        }

        public void MaterialOff(string tag)
        {
            foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //전부 disable
            {
                obj.SetState(ESpriteState.Disable);
            }
            if (materialImgLeft.tag == tag)
            {
                materialImgLeft.SetActive(false);
                materialImgLeft.GetComponent<Image>().sprite = null;
                foodMaterialSelectID[0] = null;
                foodMaterialSelectOn[0] = false;
            }
            else if (materialImgRight.tag == tag)
            {
                materialImgRight.SetActive(false);
                materialImgRight.GetComponent<Image>().sprite = null;
                foodMaterialSelectID[1] = null;
                foodMaterialSelectOn[1] = false;
            }
            if (foodMaterialSelectID[0] != null)
            {
                RecipeIDCheck(foodMaterialSelectID[0]);
            }
            else if (foodMaterialSelectID[1] != null)
            {
                RecipeIDCheck(foodMaterialSelectID[1]);
            }
            else
            {
                //if (cookImageObject.GetComponent<Text>().text != "")
                //    RecipeCheckID(cookImageObject.GetComponent<Text>().text);
                //else
                //{
                //
                //    foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //전부 enable
                //    {
                //        obj.SetState(ESpriteState.Enable);
                //    }
                //}
                RecipeIDCheck(CookImageCheck(cookImageObject.GetComponent<Image>().sprite));
            }
        }

        private void RecipeIDCheck(string id)
        {
            List<string> recipeIDMaterial = new List<string>();
            RecipeIDCheckDic(id, recipeIDMaterial);
            List<string> recipeIDMaterialSub = new List<string>();
            List<string> recipeIDCookSub = new List<string>();
            if (id != foodMaterialSelectID[0] && null != foodMaterialSelectID[0])
            {
                RecipeIDCheckDic(foodMaterialSelectID[0], recipeIDMaterialSub);
            }
            else if (id != foodMaterialSelectID[1] && null != foodMaterialSelectID[1])
            {
                RecipeIDCheckDic(foodMaterialSelectID[1], recipeIDMaterialSub);
            }
            List<string> recipeTotalMaterial = RecipeIDOverlap(recipeIDMaterial, recipeIDMaterialSub);



            for (int i = 0; i < recipeTotalMaterial.Count; i++)   // 조합에 해당한 음식재료만 ON
            {
                foodMaterialButtonDic[recipeTotalMaterial[i]].SetState(ESpriteState.Enable);
            }
        }

        private List<string> RecipeIDOverlap(List<string> list1, List<string> list2)
        {
            List<string> temp = new List<string>();
            if (list2.Count == 0)
            {
                return list1;
            }
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    for (int j = 0; j < list2.Count; j++)
                    {
                        if (list1[i] == list2[j])
                        {
                            temp.Add(list1[i]);
                            break;
                        }
                    }
                }
            }
            return temp;
        }

        private void RecipeIDCheckDic(string id, List<string> material)
        {
            for (int i = 0; i < DataJsonSet.RecipeDictionary[id].Count; i++)    //조합에 해당하는 재료ID만 가져옴
            {
                if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[id][i]].CookID == CookImageCheck(cookImageObject.GetComponent<Image>().sprite))
                {
                    string temp = DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[id][i]].FoodMaterialID;
                    if (id != temp)
                    {
                        material.Add(temp);
                    }
                    temp = DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[id][i]].FoodSubMaterialID;
                    if (id != temp)
                    {
                        material.Add(temp);
                    }
                }
            }
        }

        // NPC이미지 설정
        private void NpcImageSet(GameObject dest, string id, int state)
        {
            dest.GetComponent<RawImage>().texture = Resources.Load<Texture>(DataJsonSet.CharImageDictionary[id][state].ImagePath);
            dest.name = id;
            StartCoroutine(ObjectFade.ObjectFadeIn(dest, fadeTime));
            //if (id == "CH05") 추후 확인후 수정
            //{
            //    dest.GetComponent<RectTransform>().sizeDelta = new Vector2(750, 800);
            //}
            //else
            //{
            //    dest.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 800);
            //}
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
            StartCoroutine(ObjectFade.ObjectFadeOutChange(src, fadeTime));
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

        public void FoodSet()
        {
            string foodID = CookImageCheck(cookImageObject.GetComponent<Image>().sprite);
            for (int i = 0; i < DataJsonSet.RecipeDictionary[foodID].Count; i++)
            {
                if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[foodID][i]].FoodMaterialID == foodMaterialSelectID[0])
                {
                    if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[foodID][i]].FoodSubMaterialID == foodMaterialSelectID[1])
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID = DataJsonSet.RecipeDictionary[foodID][i];
                    }
                }
                else if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[foodID][i]].FoodSubMaterialID == foodMaterialSelectID[0])
                {
                    if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[foodID][i]].FoodMaterialID == foodMaterialSelectID[1])
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID = DataJsonSet.RecipeDictionary[foodID][i];
                    }
                }
            }
            RunTimeData.RunTimeDataSet.cookID = foodID;
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

        enum ECookValue
        {
            Bolling = 1,
            Pan,
            Oven,
            Fry,
        }

        private ECookValue CookIDToEnum(string cookID)
        {
            ECookValue temp = ECookValue.Pan;
            if (cookID == "C2")
                temp = ECookValue.Bolling;
            else if (cookID == "C3")
                temp = ECookValue.Fry;
            else if (cookID == "C4")
                temp = ECookValue.Oven;
            return temp;
        }

        public void ChangeUI()
        {
            if (uiCook.activeSelf && foodMaterialSelectID[0] != null && foodMaterialSelectID[1] != null)
            {
                Perspective.setValue(0);
                CookType.setValue((int)CookIDToEnum(RunTimeData.RunTimeDataSet.cookID));
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
                btnCook.SetActive(!btnCook.activeSelf);
                FoodStatusUp();
            }
            else if(!uiCook.activeSelf)
            {
                Perspective.setValue(1);
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
            }
        }

        public void LogUI()
        {
            if (uiLog.activeSelf)
            {
                uiLog.SetActive(false);
                uiLogPanel.transform.SetParent(uiLogOff.transform, false);

            }
            else if(!uiLog.activeSelf)
            {
                uiLog.SetActive(true);
                uiLogPanel.transform.SetParent(uiLog.transform, false);
                logScrollBar.value = 0;
            }

        }

        public void FoodStatusUp()
        {
            FoodBonus(CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID, DataJsonSet.StatusDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].Status);
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
                    charImg[i].GetComponent<RawImage>().texture = Resources.Load<Texture>(DataJsonSet.CharImageDictionary[id][state].ImagePath);
                }
            }
        }

        public void NpcJoin(string id, int state)
        {
            NpcEntry();
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
            if (type == "color")
                LogRichTextColor(type, value);
        }

        private void LogRichTextColor(string type,string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("<{0}={1}>", type, value);
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder2.AppendFormat("</{0}>", type);
            int num = logStringBuilder.ToString().IndexOf(stringBuilder2.ToString(), logTextIndex);
            if (num >= 0)
            {
                logTextIndex += num + stringBuilder2.Length - logTextIndex;
            }
            logStringBuilder.Insert(logTextIndex++, stringBuilder);
            logStringBuilder.Append(stringBuilder2);
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder3.AppendFormat("</{0}>", textTypeDictionary[type]);
            num = logStringBuilder.ToString().IndexOf(stringBuilder3.ToString(), logTextIndex);
            if (num >= 0)
            {
                logStringBuilder.Remove(num, stringBuilder3.Length);
                logStringBuilder.Append(stringBuilder3);
            }
            logTextIndex += logStringBuilder.ToString().IndexOf(">", logTextIndex) + 1 - logTextIndex;
        }

        public void ChAppend(char ch)
        {
            voice = FMODUnity.RuntimeManager.CreateInstance(eventPath2);   // 이벤트 주소를 참조하여 객체 생성
            //restaurant.getParameter("cooktype", out cooktype);         // 임시객체의 파라미터와 파라미터 변수 연동
            voice.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            voice.start();            // 객체 활성화(재생)
            voice.release();
            textStringBuilder.Insert(textIndex++, ch);
            logStringBuilder.Insert(logTextIndex++, ch);
            textOverFlowIndex++;
            if (ch == '\n')
                textOverFlowIndex = 0;
            else if(textOverFlowIndex >= 26)
            {
                textStringBuilder.Insert(textIndex++, '\n');
                textOverFlowIndex = 0;
            }

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
            finish = FMODUnity.RuntimeManager.CreateInstance(eventPath5);   // 이벤트 주소를 참조하여 객체 생성
            finish.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            finish.start();            // 객체 활성화(재생)
            finish.release();            // 객체 활성화(재생)
            BlackOnOff();
            Image[] img = uiBlack.GetComponentsInChildren<Image>();
            img[2].sprite = Resources.Load<Sprite>(DataJsonSet.FoodDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].ImageLocation);
            RectTransform[] rectTransform = uiBlack.GetComponentsInChildren<RectTransform>();
            rectTransform[2].sizeDelta = new Vector2(620.0f, 385.0f);
            Text[] text = uiBlack.GetComponentsInChildren<Text>();
            text[0].text = DataJsonSet.FoodDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].Name;
            text[1].text = DataJsonSet.FoodDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].Description;
        }

        public void MaterialPopUp(string fmID)
        {
            unlock = FMODUnity.RuntimeManager.CreateInstance(eventPath4);   // 이벤트 주소를 참조하여 객체 생성
            unlock.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            unlock.start();            // 객체 활성화(재생)
            unlock.release();            // 객체 활성화(재생)
            BlackOnOff();
            Image[] img = uiBlack.GetComponentsInChildren<Image>();
            img[2].sprite = Resources.Load<Sprite>(DataJsonSet.MaterialDataDictionary[fmID].ImageLocation);
            RectTransform[] rectTransform = uiBlack.GetComponentsInChildren<RectTransform>();
            rectTransform[2].sizeDelta = new Vector2(385.0f, 385.0f);
            Text[] text = uiBlack.GetComponentsInChildren<Text>();
            text[0].text = DataJsonSet.MaterialDataDictionary[fmID].Name;
            text[1].text = DataJsonSet.MaterialDataDictionary[fmID].Description;
        }

        public void BlackOnOff()
        {
            serve = FMODUnity.RuntimeManager.CreateInstance(eventPath6);   // 이벤트 주소를 참조하여 객체 생성
            serve.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            serve.start();            // 객체 활성화(재생)
            serve.release();            // 객체 활성화(재생)
            CookType.setValue(0);
            uiBlack.SetActive(!uiBlack.activeSelf);
        }

        public void LogTextAppend(string talkerName)
        {
            //logStringBuilder.AppendFormat("{0} : {1}\n", talkerName,textStringBuilder);
            //uiLog.GetComponentInChildren<Text>().text = logStringBuilder.ToString();
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/LogText"));
            obj.GetComponent<Text>().text = string.Format("{0} : {1}", talkerName, logStringBuilder);
            obj.transform.SetParent(uiLogContent.transform,false);
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
            logStringBuilder.Clear();
            textStringBuilder.Clear();
            textIndex = 0;
            logTextIndex = 0;
            textOverFlowIndex = 0;
            return textStack.Peek();
        }

        public void End()
        {
            if (textStack.Count > 0)
            {
                textStack.Peek().textTypeList = null;
                textStack.Pop();
            }
            if (textStack.Count == 0)
            {
                for (int i = 0; i < singleStatus.Length; i++)
                {
                    CharDataSet.charDataDictionary[nowEvent.CharID].Status[i] = System.Convert.ToInt32(singleStatus[i]);
                }
                foreach (TriggerType temp in DataJsonSet.TriggerDictionary[nowEvent.TriggerID])
                {
                    if (temp.IsTrigger(nowEvent.CharID))
                    {
                        for (int i = 0; i < temp.Status.Length; i++)
                        {
                            CharDataSet.charDataDictionary[nowEvent.CharID].Status[i] += temp.Status[i];
                        }
                        if (temp.StoryState != -1)
                        {
                            CharDataSet.charDataDictionary[nowEvent.CharID].StoryState = temp.StoryState;
                        }
                    }
                }
                for (int i = 0; i < currentStatus.Length; i++)
                {
                    currentStatus[i] = 0;
                }
                if (eventsQueue.Count == 0)
                {
                    btnEnd.SetActive(true);
                }
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
            if (foodID == CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID)
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
            Image img = statusArr[index].GetComponentInChildren<Image>();
            img.fillAmount = 1.0f - singleStatus[index] / 100.0f;
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
            SelectBtnList.Add(Instantiate(Resources.Load<GameObject>("Prefebs/SelectBtn")));
            int count = SelectBtnList.Count - 1;
            SelectBtnList[count].transform.SetParent(uiDialog.transform,false);
            SelectBtnList[count].transform.localPosition = new Vector2(655, -50 + (-80 * count));
            SelectBtnList[count].GetComponentInChildren<Text>().text = btnText;
            SelectBtnList[count].GetComponent<ButtonTrigger>().OnEvent.AddListener(() => IndexJump(index));
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
        public void StatusLayerDown()
        {
            if (runningCoroutine != null)
                StopCoroutine(runningCoroutine);
            runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpYDelegate(statusLayer.transform, 270.0f, 5.0f,()=>StatusUpdate()));
            isStatusLayerInteraction = true;
        }
    }
}
