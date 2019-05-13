using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RestRoomScene
{
    public class RestRoomUIMgr : MonoBehaviour
    {
        private void Awake()
        {
        }
        public void GameSceneLoad()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
