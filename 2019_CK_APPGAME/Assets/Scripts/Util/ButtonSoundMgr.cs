using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class ButtonSoundMgr
    {
        public static string eventPath = "event:/Public/Public.Button";    // 재생할 이벤트 주소
        public static FMOD.Studio.EventInstance button;           // 이벤트 주소로 생성할 임시객체
        public static FMOD.Studio.ParameterInstance BtState;      // 이벤트 파라미터 변수

        public static void SoundOn()
        {
            button = FMODUnity.RuntimeManager.CreateInstance(eventPath);   // 이벤트 주소를 참조하여 객체 생성
            button.getParameter("BtState", out BtState);         // 임시객체의 파라미터와 파라미터 변수 연동
            button.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            button.start();
            button.release();
        }
    }
}
