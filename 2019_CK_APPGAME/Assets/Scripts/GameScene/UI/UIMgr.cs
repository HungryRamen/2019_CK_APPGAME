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

    enum EStatus
    {
        Down,
        Up,
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

        private GameObject btnDrinks;

        public StringBuilder textStringBuilder;

        [Range(0.01f, 1f)]
        public float fadeTime = 0.01f;

        private static UIMgr uiMgrSingleton = null;

        private bool isBackGroundClick;

        private bool isStatusLayerInteraction;

        private int[] cookCurrentStatus = new int[5];

        private int[] currentStatus = new int[5];

        private float[] singleStatus = new float[5];

        private Stack<TextStackType> textStack = new Stack<TextStackType>();

        private Queue<DayEventsType> eventsQueue = new Queue<DayEventsType>();

        public DayEventsType nowEvent;

        private Dictionary<string, string> textTypeDictionary = new Dictionary<string, string>();

        private GameObject[] charImgLayer;

        public GameObject[] charImg;

        private GameObject statusLayer;

        private GameObject[] statusArr;

        private List<GameObject> SelectBtnList = new List<GameObject>();

        private Coroutine runningCoroutine;

        private Coroutine talkCoroutine;

        private Coroutine cookingCoroutine;

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

        private GameObject statusName;

        private GameObject helpLayer;

        private GameObject cookingLayer;

        private Dictionary<string, FoodMaterialButtonMgr> foodMaterialButtonDic = new Dictionary<string, FoodMaterialButtonMgr>();

        private Dictionary<string, Image> cookButtonDic = new Dictionary<string, Image>();

        private string[] foodMaterialSelectID = new string[2]; //선택된 음식재료 ID 0은왼쪽 1은오른쪽

        private bool[] foodMaterialSelectOn = new bool[2]; //선택위에 들어와있는지 없는지

        private GameObject cookButton;

        private Dictionary<string, Sprite> statusNameDic = new Dictionary<string, Sprite>();

        private List<Sprite> talkAnimationSpriteList = new List<Sprite>();

        private List<Sprite> cookingAnimationSpriteList = new List<Sprite>();

        private Dictionary<CursorImageData.EMouseState, Sprite> drinkSpriteDic = new Dictionary<CursorImageData.EMouseState, Sprite>();

        private Dictionary<EStatus, Sprite> statusSpriteDic = new Dictionary<EStatus, Sprite>();

        private GameObject cookImageObject;

        private StringBuilder logStringBuilder = new StringBuilder();

        private int talkSpriteIndex = 0;

        private int cookSpriteIndex = 0;

        private string talkerName;

        public bool isTextCancel;
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
            statusName = GameObject.FindWithTag("StatusName");
            cookingLayer = GameObject.FindWithTag("Cooking");
            helpLayer = GameObject.FindWithTag("HelpLayer");
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
            btnDrinks = GameObject.FindWithTag("DrinksBtn");

            Sprite[] talkSprites = Resources.LoadAll<Sprite>("UI/Dialog/DialogSpriteSheet");
            for (int i = 0; i < 4; i++)
            {
                talkAnimationSpriteList.Add(talkSprites[i]);
            }

            for(int i =1;i< 4;i++)
            {
                cookingAnimationSpriteList.Add(Resources.Load<Sprite>(string.Format("{0}{1}", "UI/Text/Cooking", i.ToString())));
            }

            Sprite[] statusSprites = Resources.LoadAll<Sprite>("UI/Text/TextSpriteSheet");
            statusNameDic.Add("CH01", statusSprites[0]);
            statusNameDic.Add("CH02", statusSprites[1]);
            statusNameDic.Add("CH03", statusSprites[2]);
            statusNameDic.Add("CH05", statusSprites[3]);
            statusNameDic.Add("CH06", statusSprites[4]);

            drinkSpriteDic.Add(CursorImageData.EMouseState.DK1, Resources.Load<Sprite>("UI/Dialog/DK1"));
            drinkSpriteDic.Add(CursorImageData.EMouseState.DK2, Resources.Load<Sprite>("UI/Dialog/DK2"));
            drinkSpriteDic.Add(CursorImageData.EMouseState.DK3, Resources.Load<Sprite>("UI/Dialog/DK3"));
            drinkSpriteDic.Add(CursorImageData.EMouseState.DK4, Resources.Load<Sprite>("UI/Dialog/DK4"));
            drinkSpriteDic.Add(CursorImageData.EMouseState.DK5, Resources.Load<Sprite>("UI/Dialog/DK5"));

            statusSprites = Resources.LoadAll<Sprite>("UI/Dialog/SpriteSheet3");
            statusSpriteDic.Add(EStatus.Down, statusSprites[0]);
            statusSpriteDic.Add(EStatus.Up, statusSprites[1]);

            DialogLoadClassifcation(RunTimeData.RunTimeDataSet.day);

            uiCook.SetActive(false);
            uiBlack.SetActive(false);
            btnCook.SetActive(false);
            btnEnd.SetActive(false);
            materialImgLeft.SetActive(false);
            materialImgRight.SetActive(false);
            cookingLayer.SetActive(false);
            if (RunTimeData.RunTimeDataSet.day != "1")
                helpLayer.SetActive(false);
            for (int i = 0; i < charImg.Length; i++)
            {
                charImg[i].SetActive(false);
            }

            textStringBuilder = new StringBuilder();
            textIndex = 0;
            logTextIndex = 0;
            textOverFlowIndex = 0;
            textOutputTime = 0f;
            fadeTime = 0.1f;
            bChAppend = false;
            isBackGroundClick = false;
            isStatusLayerInteraction = false;
            isTextCancel = false;
            runningCoroutine = null;
            talkCoroutine = null;
            textTypeDictionary.Add("color", "size");
            textTypeDictionary.Add("size", "color");
            //Texture2D[] spritesTemp = Resources.LoadAll<Texture2D>("UI/Dialog/DialogSpriteSheet");

            //Cursor.SetCursor(spritesTemp[0], Vector2.zero, CursorMode.Auto);
        }

        private void Start()
        {
            btnDrinks.SetActive(false);

            SoundMgr.SoundOnStart(ESoundType.RestaurantAmb);
            SoundMgr.SoundOnStart(ESoundType.RestaurantMusic);

            SoundMgr.SoundOnStart(ESoundType.NPCEntry);
            SoundMgr.SoundOnStart(ESoundType.Fridge);

        }

        public void HelpLayerOnOff()
        {
            helpLayer.SetActive(!helpLayer.activeSelf);
        }

        public void NpcEntry()
        {
            float temp;
            SoundMgr.playSoundDic[ESoundType.NPCEntry].states[0].getValue(out temp);
            if (temp == 1)
                SoundMgr.playSoundDic[ESoundType.NPCEntry].states[0].setValue(0);
            SoundMgr.playSoundDic[ESoundType.NPCEntry].states[0].setValue(1);
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
            {
                RunTimeData.RunTimeDataSet.lockMaterials.Remove(fmID);
                foreach (FoodMaterialButtonMgr obj in foodMaterialButtonDic.Values)    //일단 전부 disable
                {
                    obj.Restart();
                }
            }
        }

        public void AutoOnOff()
        {
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

        public void MaterialOff(string tag) //
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
            CookStatusReset();
        }

        private void CookStatusReset()
        {
            cookCurrentStatus = CharDataSet.charDataDictionary[nowEvent.CharID].Status;
            CookStatusFillChange();
        }

        private void CookStatusFillChange()
        {
            for (int i = 0; i < statusArr.Length; i++)
            {
                Image[] img = statusArr[i].GetComponentsInChildren<Image>();
                img[2].fillAmount = 1.0f - cookCurrentStatus[i] / 100.0f;
                img[1].fillAmount = cookCurrentStatus[i] / 100.0f;
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
            //if (id == "CH01") 마찬가지
            //{
            //    dest.transform.SetParent(charImgLayer[1].transform);
            //}
            //else
            //{
            //    dest.transform.SetParent(charImgLayer[0].transform);
            //}
        }

        // 덮어씌워질곳 덮어씌울오브젝트
        private void NpcImageOverwrite(GameObject dest, GameObject src)
        {
            dest.GetComponent<RawImage>().texture = src.GetComponent<RawImage>().texture;
            Image[] temp = dest.GetComponentsInChildren<Image>();
            dest.GetComponentInChildren<Image>().sprite = src.GetComponentInChildren<Image>().sprite;
            dest.name = src.name;
            //dest.transform.SetParent(src.transform.parent);
            //dest.GetComponent<RectTransform>().sizeDelta = src.GetComponent<RectTransform>().sizeDelta;
            StartCoroutine(ObjectFade.ObjectFadeIn(dest, fadeTime));
            StartCoroutine(ObjectFade.ObjectSpriteFadeIn(dest.GetComponentInChildren<Image>().transform.gameObject, fadeTime));
            StartCoroutine(ObjectFade.ObjectFadeOutChange(src, fadeTime));
            StartCoroutine(ObjectFade.ObjectSpriteFadeOutChange(src.GetComponentInChildren<Image>().transform.gameObject, fadeTime));
        }

        // 대사 분류
        private void DialogLoadClassifcation(string day)
        {
            DataJsonSet.TextDictionary.Clear();
            SheetLoad.SheetLoad_Dlg dlg = new SheetLoad.SheetLoad_Dlg();
            try
            {

                foreach (DayEventsType events in DataJsonSet.DayEventsDictionary[day])
                {
                    if (CharDataSet.charDataDictionary.ContainsKey(events.CharID))
                    {
                        if (CharDataSet.charDataDictionary[events.CharID].StoryState == events.StoryState)
                        {
                            eventsQueue.Enqueue(events);
                            dlg.SheetDialogLoad(events.StartIndex, events.EndIndex);
                        }
                    }
                }
            }
            catch(System.Exception e)
            {
                Debug.Log(e);
                SundryUtil.ErrorOutput();
            }
        }

        public void FoodSet()
        {
            string cookID = CookImageCheck(cookImageObject.GetComponent<Image>().sprite);
            for (int i = 0; i < DataJsonSet.RecipeDictionary[cookID].Count; i++)
            {
                if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[cookID][i]].FoodMaterialID == foodMaterialSelectID[0])
                {
                    if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[cookID][i]].FoodSubMaterialID == foodMaterialSelectID[1])
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID = DataJsonSet.RecipeDictionary[cookID][i];
                    }
                }
                else if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[cookID][i]].FoodSubMaterialID == foodMaterialSelectID[0])
                {
                    if (DataJsonSet.RecipeDataDictionary[DataJsonSet.RecipeDictionary[cookID][i]].FoodMaterialID == foodMaterialSelectID[1])
                    {
                        CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID = DataJsonSet.RecipeDictionary[cookID][i];
                    }
                }
            }
            RunTimeData.RunTimeDataSet.cookID = cookID;
            CookStatusCheck(DataJsonSet.StatusDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].Status);
        }

        private void CookStatusCheck(int[] changeStatus)
        {
            for (int i = 0; i < changeStatus.Length; i++)
            {
                int height = cookCurrentStatus[i] + changeStatus[i];
                if (height < 0)
                    height = 0;
                else if (height > 100)
                    height = 100;
                if (cookCurrentStatus[i] < height)
                {
                    CookStatusHeightFill(i, EStatus.Up, cookCurrentStatus[i], height);
                }
                else if (cookCurrentStatus[i] > height)
                {
                    CookStatusHeightFill(i, EStatus.Down, cookCurrentStatus[i], height);
                }
            }
        }

        private void CookStatusHeightFill(int index, EStatus eStatus, int currentFill, int totalFill)
        {
            Image[] img = statusArr[index].GetComponentsInChildren<Image>();
            img[0].sprite = statusSpriteDic[eStatus];
            switch (eStatus)
            {
                case EStatus.Down:
                    img[1].fillAmount = totalFill / 100.0f;
                    img[2].fillAmount = 1.0f - currentFill / 100.0f;
                    break;
                case EStatus.Up:
                    img[1].fillAmount = currentFill / 100.0f;
                    img[2].fillAmount = 1.0f - totalFill / 100.0f;
                    break;
            }
        }

        private bool ButtonActiveCheck()
        {
            return btnCook.activeSelf || btnEnd.activeSelf || btnDrinks.activeSelf;
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
            if (uiCook.activeSelf && foodMaterialSelectID[0] != null && foodMaterialSelectID[1] != null) //이 부분 제거되고 버튼 활성화 된경우닌까
            {
                SoundMgr.playSoundDic[ESoundType.RestaurantMusic].states[0].setValue(0);
                SoundMgr.playSoundDic[ESoundType.RestaurantAmb].states[0].setValue((int)CookIDToEnum(RunTimeData.RunTimeDataSet.cookID));
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
                btnCook.SetActive(!btnCook.activeSelf);
                CookStatusReset();
                FoodStatusUp();
                CookingOn();
                MaterialClear();
            }
            else if (!uiCook.activeSelf)
            {
                SoundMgr.playSoundDic[ESoundType.RestaurantMusic].states[0].setValue(1);
                uiDialog.SetActive(!uiDialog.activeSelf);
                uiCook.SetActive(!uiCook.activeSelf);
            }
        }

        private void CookingOn()
        {
            cookingLayer.SetActive(true);
            cookingCoroutine = StartCoroutine(CookingImageChange());
        }

        private void CookingOff()
        {
            cookingLayer.SetActive(false);
            StopCoroutine(cookingCoroutine);
        }

        private IEnumerator CookingImageChange()
        {
            cookSpriteIndex = 0;
            while(true)
            {
                cookingLayer.GetComponent<Image>().sprite = cookingAnimationSpriteList[cookSpriteIndex++];
                if (cookSpriteIndex >= cookingAnimationSpriteList.Count)
                    cookSpriteIndex = 0;
                yield return new WaitForSeconds(0.3f);
            }
        }

        public void LogUI()
        {
            if (uiLog.activeSelf)
            {
                uiLog.SetActive(false);
                uiLogPanel.transform.SetParent(uiLogOff.transform, false);
                SoundMgr.Stop(ESoundType.PopUp);

            }
            else if (!uiLog.activeSelf)
            {
                SoundMgr.SoundOnRelease(ESoundType.Log);
                uiLog.SetActive(true);
                uiLogPanel.transform.SetParent(uiLog.transform, false);
                SoundMgr.SoundOnStart(ESoundType.PopUp);
                logScrollBar.value = 0;
            }

        }

        public void FoodStatusUp()
        {
            FoodBonus(CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID, DataJsonSet.StatusDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID].Status);
        }

        public void RestroomSceneLoad()
        {
            SoundMgr.SoundClear();
            RunTimeData.RunTimeDataSet.DayPlus();
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
            obj.GetComponent<SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("RestroomScene"));
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

        public void NpcSoundJoin(string id, int state)
        {
            isTextCancel = false;
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

        public void NpcJoin(string id, int state)
        {
            isTextCancel = true;
            NpcEntry();
            StartCoroutine(SoundMgr.NpcJoinCheck(() => NpcSoundJoin(id, state)));
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
                    StartCoroutine(ObjectFade.ObjectSpriteFadeOut(charImg[i].GetComponentInChildren<Image>().transform.gameObject, fadeTime));
                    if (talkCoroutine != null)
                        StopCoroutine(talkCoroutine);
                    break;
                }
            }
        }

        public void NpcTalkCheck(string charID)
        {
            for (int i = 1; i < charImg.Length; i++)
            {
                string chID = "CH01";
                if (charID == "한 별")
                    chID = "CH01";
                else if (charID == "송아연")
                    chID = "CH02";
                else if (charID == "우주인")
                    chID = "CH03";
                else if (charID == "소니아 로즈메리")
                    chID = "CH05";
                else if (charID == "데이브 러셀")
                    chID = "CH06";
                Image[] temp = charImg[i].GetComponentsInChildren<Image>();
                if (charImg[i].name == chID && charImg[i].activeSelf)
                {
                    temp[1].color = Color.white;
                    if (talkCoroutine != null)
                        StopCoroutine(talkCoroutine);
                    talkCoroutine = StartCoroutine(TalkAnimation(temp[1]));
                }
                else if (charImg[i].activeSelf)
                    temp[1].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
        IEnumerator TalkAnimation(Image img)
        {
            while (true)
            {
                img.sprite = talkAnimationSpriteList[talkSpriteIndex++];
                if (talkAnimationSpriteList.Count <= talkSpriteIndex)
                    talkSpriteIndex = 0;
                yield return new WaitForSeconds(0.25f);
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

        private void LogRichTextColor(string type, string value)
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
            ESoundType eSoundType = ESoundType.CH00;
            if (talkerName == "한 별")
                eSoundType = ESoundType.CH01;
            else if (talkerName == "송아연")
                eSoundType = ESoundType.CH02;
            else if (talkerName == "우주인")
                eSoundType = ESoundType.CH03;
            else if (talkerName == "소니아 로즈메리")
                eSoundType = ESoundType.CH05;
            else if (talkerName == "데이브 러셀")
                eSoundType = ESoundType.CH06;

            SoundMgr.SoundOnRelease(eSoundType);
            textStringBuilder.Insert(textIndex++, ch);
            logStringBuilder.Insert(logTextIndex++, ch);
            textOverFlowIndex++;
            if (ch == '\n')
                textOverFlowIndex = 0;
            else if (textOverFlowIndex >= 26)
            {
                textStringBuilder.Insert(textIndex++, '\n');
                textOverFlowIndex = 0;
            }

            bChAppend = true;
        }

        public void TextStackPush(List<TextTypeRaction> copy)
        {
            if (copy == null)
                return;
            else if (copy.Count == 0)
                return;
            TextStackType temp = new TextStackType();
            temp.textTypeList = new List<TextType>();
            for(int i = 0; i <copy.Count;i++)
            {
                temp.textTypeList.Add(copy[i]);

            }

            //for (int i = 0; i < copy.Count; i++) //스토리 변수 확인
            //{
            //    if (nowEvent.StoryState == copy[i].StoryState)
            //        temp.textTypeList.Add(copy[i]);
            //}
            textStack.Push(temp);

        }

        public void FoodPopUp()
        {
            SoundMgr.SoundOnRelease(ESoundType.FinishSFX);
            CookingOff();
            SoundFoodDataType temp = DataJsonSet.SoundFoodDataDictionary[CharDataSet.charDataDictionary[nowEvent.CharID].EatFoodID];
            if(!temp.IsLoop)
                SoundMgr.SoundOnRelease((ESoundType)temp.Index);
            else
                SoundMgr.SoundOnStart((ESoundType)temp.Index);
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
            SoundMgr.SoundOnRelease(ESoundType.Unlock);
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
            if(!uiBlack.activeSelf)
            {
                SoundMgr.SoundOnStart(ESoundType.PopUp);
            }
            else if(uiBlack.activeSelf)
            {
                SoundMgr.Stop(ESoundType.PopUp);
                SoundMgr.Stop();
            }
            SoundMgr.SoundOnRelease(ESoundType.Plate);
            SoundMgr.playSoundDic[ESoundType.RestaurantAmb].states[0].setValue(0);
            uiBlack.SetActive(!uiBlack.activeSelf);
        }

        public void LogTextAppend(string talkerName)
        {
            if (talkerName == "" || talkerName == null)
                return;
            //logStringBuilder.AppendFormat("{0} : {1}\n", talkerName,textStringBuilder);
            //uiLog.GetComponentInChildren<Text>().text = logStringBuilder.ToString();
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/LogText"));
            obj.GetComponent<Text>().text = string.Format("{0} : {1}", talkerName, logStringBuilder);
            obj.transform.SetParent(uiLogContent.transform, false);
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
                statusName.GetComponent<Image>().sprite = statusNameDic[nowEvent.CharID];
                StatusUpdate();
                TextStackType temp = new TextStackType();
                temp.textTypeList = new List<TextType>(DataJsonSet.TextDictionary[nowEvent.DialogID]);
                textStack.Push(temp);
            }
            logStringBuilder.Clear();
            textStringBuilder.Clear();
            textIndex = 0;
            logTextIndex = 0;
            textOverFlowIndex = 0;
            talkerName = textStack.Peek().textTypeList[textStack.Peek().TextTypeIndex].TalkerName;
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
                if (CharDataSet.charDataDictionary[nowEvent.CharID].Status[i] != System.Convert.ToInt32(check))
                    StartCoroutine(StatusLerp(check, 1.0f, i));
            }
        }


        private IEnumerator StatusLerp(float arrive, float speed, int index)
        {
            SoundMgr.SoundOnRelease(ESoundType.StatusUpdate);
            while (Mathf.Abs(arrive - singleStatus[index]) > 1f)
            {
                singleStatus[index] = Mathf.Lerp(singleStatus[index], arrive, speed * Time.deltaTime);
                StatusFillUpdate(index);
                yield return null;
            }
            CharDataSet.charDataDictionary[nowEvent.CharID].Status[index] = System.Convert.ToInt32(arrive);
            singleStatus[index] = System.Convert.ToInt32(arrive);
            StatusFillUpdate(index);
            if (isStatusLayerInteraction)
                StatusLayerUpDown();
        }

        private void StatusFillUpdate(int index)
        {
            Image[] img = statusArr[index].GetComponentsInChildren<Image>();
            img[2].fillAmount = 1.0f - singleStatus[index] / 100.0f;
            img[1].fillAmount = singleStatus[index] / 100.0f;
        }

        public void IndexJump(int index)
        {
            for (int i = 0; i < textStack.Peek().textTypeList.Count; i++)
            {
                if (textStack.Peek().textTypeList[i].Index == index)
                {
                    textStack.Peek().TextTypeIndex = i;
                    break;
                }
            }
            //if (textStack.Peek().textTypeList.Count < index)
            //    textStack.Peek().TextTypeIndex = index;
            foreach (GameObject obj in SelectBtnList)
            {
                if (obj.name == "Check")
                {
                    Destroy(obj, 1.0f);
                    if (SelectBtnList.Count == 1)
                    {
                        SelectBtnList.Clear();
                        return;
                    }
                }
                else
                {
                    obj.GetComponent<ButtonTrigger>().enabled = false;
                    obj.GetComponent<SelectTrigger>().enabled = false;
                    obj.GetComponent<Button>().enabled = false;
                    StartCoroutine(StatusExit(obj, 1.5f));
                }
            }
        }

        private IEnumerator StatusExit(GameObject obj, float speed)
        {
            obj.GetComponent<Button>().interactable = false;
            Vector2 v = new Vector2(1400, obj.transform.localPosition.y);
            while (obj.transform.localPosition.x < 1280)
            {
                obj.transform.localPosition = Vector2.Lerp(obj.transform.localPosition, v, speed * Time.deltaTime);
                yield return null;
            }
            Destroy(obj);
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
            SelectBtnList[count].transform.SetParent(uiDialog.transform, false);
            SelectBtnList[count].transform.localPosition = new Vector2(655, -50 + (-80 * count));
            SelectBtnList[count].GetComponentInChildren<Text>().text = btnText;
            SelectBtnList[count].GetComponent<ButtonTrigger>().OnEvent.AddListener(() => IndexJump(index));
            SoundMgr.SoundOnRelease(ESoundType.Selectionln);
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

        public void DrinkButtonOn()
        {
            SoundMgr.playSoundDic[ESoundType.Fridge].states[0].setValue(0);
            SoundMgr.playSoundDic[ESoundType.Fridge].states[0].setValue(1);
            btnDrinks.SetActive(true);
        }

        public void DrinkButtonConfirmed()
        {
            for (int i = 0; i < charImg.Length; i++)
            {
                if (charImg[i].name == nowEvent.CharID)
                {
                    charImg[i].GetComponentInChildren<Image>().sprite = drinkSpriteDic[CursorImageData.currentState];
                    CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID = CursorImageData.GetCursor();
                    DrinkSound();
                    CursorImageData.SetCursor(null);
                    StartCoroutine(ObjectFade.ObjectSpriteFadeIn(charImg[i].GetComponentInChildren<Image>().transform.gameObject, fadeTime));
                    break;
                }
            }
            DrinkButtonOff();
        }

        public void DrinkButtonDragConfirmed()
        {
            bool bCheck = true;
            for (int i = 0; i < charImg.Length; i++)
            {
                if (charImg[i].name == nowEvent.CharID && charImg[i].GetComponent<CharTrigger>().isCharOn)
                {
                    bCheck = false;
                    charImg[i].GetComponentInChildren<Image>().sprite = drinkSpriteDic[CursorImageData.currentState];
                    CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID = CursorImageData.GetCursor();
                    DrinkSound();
                    StartCoroutine(ObjectFade.ObjectSpriteFadeIn(charImg[i].GetComponentInChildren<Image>().transform.gameObject, fadeTime));
                    break;
                }
            }
            if (bCheck)
                return;
            DrinkButtonOff();
        }

        private void DrinkButtonOff()
        {
            SoundMgr.playSoundDic[ESoundType.Fridge].states[0].setValue(2);
            btnDrinks.SetActive(false);
        }

        private void DrinkSound()
        {
            if (CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID == "DK1")
            {
                SoundMgr.SoundOnRelease(ESoundType.DK01);
            }
            else if (CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID == "DK2")
            {
                SoundMgr.SoundOnRelease(ESoundType.DK02);
            }
            else if (CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID == "DK3")
            {
                SoundMgr.SoundOnRelease(ESoundType.DK03);
            }
            else if (CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID == "DK4")
            {
                SoundMgr.SoundOnRelease(ESoundType.DK04);
            }
            else if (CharDataSet.charDataDictionary[nowEvent.CharID].DrinkID == "DK5")
            {
                SoundMgr.SoundOnRelease(ESoundType.DK05);
            }
        }

        public void StatusLayerUpDown()
        {
            SoundMgr.SoundOn(ESoundType.Status);
            if (runningCoroutine != null)
                StopCoroutine(runningCoroutine);
            if (isStatusLayerInteraction)
            {
                runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpY(statusLayer.transform, 810.0f, 5.0f));
                SoundMgr.playSoundDic[ESoundType.Status].states[0].setValue(1);
            }
            else
            {
                runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpY(statusLayer.transform, 270.0f, 5.0f));
                SoundMgr.playSoundDic[ESoundType.Status].states[0].setValue(0);
            }
            SoundMgr.Release(ESoundType.Status);
            isStatusLayerInteraction = !isStatusLayerInteraction;
        }
        public void StatusLayerDown()
        {
            if (runningCoroutine != null)
                StopCoroutine(runningCoroutine);
            runningCoroutine = StartCoroutine(ObjectLerf.LocalLerpYDelegate(statusLayer.transform, 270.0f, 5.0f, () => StatusUpdate()));
            isStatusLayerInteraction = true;
        }
    }
}
