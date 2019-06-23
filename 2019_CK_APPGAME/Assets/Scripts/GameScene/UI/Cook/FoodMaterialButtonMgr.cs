using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GameScene
{

    public enum ESpriteState
    {
        Disable,
        Enable,
        None,
    }

    public class FoodMaterialButtonMgr : MonoBehaviour
    {
        private static Sprite[] sprites = new Sprite[3];
        private ESpriteState currentState = ESpriteState.Enable;
        private Image imageMain;
        private RectTransform[] material;
        private void Awake()
        {
            imageMain = GetComponent<Image>();
            Sprite[] temp = Resources.LoadAll<Sprite>("UI/Dialog/SpriteSheet2");
            material = GetComponentsInChildren<RectTransform>();
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = temp[i];
            }
            for(int i = 0; i < RunTimeData.RunTimeDataSet.lockMaterials.Count;i++)
            {
                if(name == RunTimeData.RunTimeDataSet.lockMaterials[i])
                {
                    SetState(ESpriteState.None);
                    return;
                }
            }
            SetState(ESpriteState.Enable);
        }
        public void SetState(ESpriteState eState)
        {
            if (currentState == ESpriteState.None)
                return;
            currentState = eState;
            imageMain.sprite = sprites[(int)currentState];
            if (eState == ESpriteState.None)
                material[1].gameObject.SetActive(false);
            else
                material[1].gameObject.SetActive(true);
        }

        public void UnLock()
        {
            currentState = ESpriteState.Enable;
            imageMain.sprite = sprites[(int)currentState];
            material[1].gameObject.SetActive(true);
        }

        public ESpriteState GetState()
        {
            return currentState;
        }
    }
}
