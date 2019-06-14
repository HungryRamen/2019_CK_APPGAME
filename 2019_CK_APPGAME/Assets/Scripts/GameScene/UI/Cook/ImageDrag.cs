using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene
{
    public class ImageDrag : MonoBehaviour
    {
        private Image currentImage;
        private float rootWidth;
        private float rootHeight;
        private void Awake()
        {
            currentImage = GetComponent<Image>();

            rootWidth = GameObject.FindWithTag("UIMgr").GetComponent<RectTransform>().rect.width;
            rootHeight = GameObject.FindWithTag("UIMgr").GetComponent<RectTransform>().rect.height;
            OffImage();
        }

        public void ChangeImage(Sprite temp,bool isCheck)
        {
            if (isCheck)
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            else
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 148);
            currentImage.sprite = temp;
            enabled = true;
            currentImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            SetPos();
        }

        public void OffImage()
        {
            currentImage.sprite = null;
            enabled = false;
            currentImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        public Sprite GetImage()
        {
            return currentImage.sprite;
        }

        // Update is called once per frame
        void Update()
        {
            SetPos();
        }

        private void SetPos()
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            pos.x *= rootWidth;
            pos.y *= rootHeight;
            transform.position = pos;
        }
    }
}
