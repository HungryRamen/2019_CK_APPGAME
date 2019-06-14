using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameScene;
namespace Util
{
    public class SelectTrigger : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Sprite[] temp = Resources.LoadAll<Sprite>("UI/Dialog/CookSpriteSheet");
            GetComponent<Image>().sprite = temp[2];
            name = "Check";
            GetComponent<ButtonTrigger>().enabled = false;
            GetComponent<SelectTrigger>().enabled = false;
            GetComponent<Button>().enabled = false;
        }
    }
}