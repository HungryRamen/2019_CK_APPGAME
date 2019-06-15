using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetData;
namespace Util
{
    public class SoundData
    {
        public FMOD.Studio.EventInstance obj;
        public List<FMOD.Studio.ParameterInstance> states = new List<FMOD.Studio.ParameterInstance>();
    }
    public static class SoundMgr
    {

        public static Dictionary<ESoundType, SoundData> playSoundDic = new Dictionary<ESoundType, SoundData>();
        public static SoundData soundMaster = new SoundData();

        public static void SoundMasterOn()
        {
            soundMaster.obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[ESoundType.Volume].Path);
            soundMaster.obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            for (int i = 0; i < DataJsonSet.SoundDataDictionary[ESoundType.Volume].parameterName.Count; i++)
            {
                FMOD.Studio.ParameterInstance pt;
                soundMaster.obj.getParameter(DataJsonSet.SoundDataDictionary[ESoundType.Volume].parameterName[i], out pt);
                soundMaster.states.Add(pt);
            }
            soundMaster.obj.start();
        }

        public static void SoundNormalOn()
        {
            FMOD.Studio.EventInstance obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[ESoundType.Normal].Path);
            obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            obj.start();
        }

        public static void SoundMasterValueChange(int index,float value)
        {
            soundMaster.states[index].setValue(value);
        }

        public static void SoundOnRelease(ESoundType eSoundType) //사용되고 바로 버려지는 SFX사운드
        {
            FMOD.Studio.EventInstance temp = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundType].Path);
            temp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            temp.start();
            temp.release();

        }

        public static void SoundOnStart(ESoundType eSoundType)
        {
            SoundData temp = new SoundData();
            temp.obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundType].Path);
            for (int i = 0; i < DataJsonSet.SoundDataDictionary[eSoundType].parameterName.Count; i++)
            {
                FMOD.Studio.ParameterInstance pt;
                temp.obj.getParameter(DataJsonSet.SoundDataDictionary[eSoundType].parameterName[i], out pt);
                temp.states.Add(pt);
            }
            temp.obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            temp.obj.start();
            playSoundDic.Add(eSoundType, temp);
        }

        public static void SoundOn(ESoundType eSoundType)
        {
            SoundData temp = new SoundData();
            temp.obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundType].Path);
            for (int i = 0; i < DataJsonSet.SoundDataDictionary[eSoundType].parameterName.Count; i++)
            {
                FMOD.Studio.ParameterInstance pt;
                temp.obj.getParameter(DataJsonSet.SoundDataDictionary[eSoundType].parameterName[i], out pt);
                temp.states.Add(pt);
            }
            temp.obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            playSoundDic.Add(eSoundType, temp);
        }

        public static void Release(ESoundType eSoundType)
        {
            playSoundDic[eSoundType].obj.start();
            playSoundDic[eSoundType].obj.release();
            playSoundDic.Remove(eSoundType);
        }

        public static void Stop()
        {
            ESoundType eSoundType = ESoundType.Empty;
            if(playSoundDic.ContainsKey(ESoundType.Sizzling))
            {
                eSoundType = ESoundType.Sizzling;
            }
            else if(playSoundDic.ContainsKey(ESoundType.Boiling))
            {
                eSoundType = ESoundType.Boiling;
            }
            else if(playSoundDic.ContainsKey(ESoundType.Poizon))
            {
                eSoundType = ESoundType.Poizon;
            }
            if (eSoundType == ESoundType.Empty)
                return;
            playSoundDic[eSoundType].obj.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            playSoundDic[eSoundType].obj.release();
            playSoundDic.Remove(eSoundType);
        }

        public static void Stop(ESoundType eSoundType)
        {
            playSoundDic[eSoundType].obj.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            playSoundDic[eSoundType].obj.release();
            playSoundDic.Remove(eSoundType);
        }

        public static void SoundClear()
        {
            foreach (SoundData sound in playSoundDic.Values)
            {
                sound.obj.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                sound.obj.release();
            }
            playSoundDic.Clear();
        }
        //테스트
        public static IEnumerator NpcJoinCheck(System.Action func)
        {
            yield return new WaitForSeconds(5.0f);
            func();
        }
    }
}
