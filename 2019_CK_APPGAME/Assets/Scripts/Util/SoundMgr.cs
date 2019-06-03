using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Util
{
    public enum ESound
    {
        None = -1,
        Button,
    }
    public static class SoundMgr
    {
        public delegate void Func();
        public static Dictionary<ESound,Func> soundDics = new Dictionary<ESound,Func>();
        //static Test TestFunc = new Test(SoundOn);
        public static string eventPath = "event:/Public/Public.Button";    // 재생할 이벤트 주소
        public static FMOD.Studio.EventInstance button;           // 이벤트 주소로 생성할 임시객체
        public static FMOD.Studio.ParameterInstance BtState;      // 이벤트 파라미터 변수


        public static string eventPath2 = "snapshot:/Volume";    // 재생할 이벤트 주소
        public static FMOD.Studio.EventInstance volume;           // 이벤트 주소로 생성할 임시객체
        public static FMOD.Studio.ParameterInstance volumeState;      // 이벤트 파라미터 변수

        public static void SetUp()
        {

        }

        public static void SoundOn()
        {

            //button = FMODUnity.RuntimeManager.CreateInstance(eventPath);   // 이벤트 주소를 참조하여 객체 생성
            //button.getParameter("BtState", out BtState);         // 임시객체의 파라미터와 파라미터 변수 연동
            //button.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정
            //button.start();
            //button.release();
        }
        //테스트
        public static IEnumerator Check()
        {
            FMOD.Studio.PLAYBACK_STATE test;
            button.getPlaybackState(out test);
            if (test != FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                Debug.Log(test);
                yield return null;
            }
            Debug.Log(test);
            Debug.Log("끝남");
        }

        public static void SoundSetting()
        {
            volume = FMODUnity.RuntimeManager.CreateInstance(eventPath2);   // 이벤트 주소를 참조하여 객체 생성
            volume.getParameter("Volume", out volumeState);         // 임시객체의 파라미터와 파라미터 변수 연동
            volume.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));       // 어디서 사운드가 들리는지 설정

            volume.setParameterValue("Volume", 0.0f);
            volume.start();
        }
    }
}
