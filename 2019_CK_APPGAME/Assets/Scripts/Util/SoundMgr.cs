using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SheetData;
using RunTimeData;

namespace Util
{
    public class SoundData
    {
        public FMOD.Studio.EventInstance obj;
        public List<FMOD.Studio.ParameterInstance> states = new List<FMOD.Studio.ParameterInstance>();
        public ESoundSet eSoundSet;
    }
    public static class SoundMgr
    {

        public static Dictionary<ESoundSet, SoundData> playSoundDic = new Dictionary<ESoundSet, SoundData>();

        public static void SoundMasterValueChange(ESoundType eSoundType, float value)
        {
            RunTimeDataSet.soundVolumeDic[eSoundType] = value;
            foreach(SoundData obj in playSoundDic.Values)
            {
                if(DataJsonSet.SoundDataDictionary[obj.eSoundSet].eSoundType == eSoundType)
                {
                    obj.obj.setVolume(value);

                }
            }
        }

        public static void SoundOnRelease(ESoundSet eSoundSet) //사용되고 바로 버려지는 SFX사운드
        {
            FMOD.Studio.EventInstance temp = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundSet].Path);
            temp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            temp.setVolume(RunTimeDataSet.soundVolumeDic[DataJsonSet.SoundDataDictionary[eSoundSet].eSoundType]);
            temp.start();
            temp.release();

        }

        public static void SoundOnStart(ESoundSet eSoundSet)
        {
            SoundData temp = new SoundData();
            temp.eSoundSet = eSoundSet;
            temp.obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundSet].Path);
            for (int i = 0; i < DataJsonSet.SoundDataDictionary[eSoundSet].parameterName.Count; i++)
            {
                FMOD.Studio.ParameterInstance pt;
                temp.obj.getParameter(DataJsonSet.SoundDataDictionary[eSoundSet].parameterName[i], out pt);
                temp.states.Add(pt);
            }
            temp.obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            temp.obj.setVolume(RunTimeDataSet.soundVolumeDic[DataJsonSet.SoundDataDictionary[eSoundSet].eSoundType]);
            temp.obj.start();
            playSoundDic.Add(eSoundSet, temp);
        }

        public static void SoundOn(ESoundSet eSoundSet)
        {
            SoundData temp = new SoundData();
            temp.eSoundSet = eSoundSet;
            temp.obj = FMODUnity.RuntimeManager.CreateInstance(DataJsonSet.SoundDataDictionary[eSoundSet].Path);
            for (int i = 0; i < DataJsonSet.SoundDataDictionary[eSoundSet].parameterName.Count; i++)
            {
                FMOD.Studio.ParameterInstance pt;
                temp.obj.getParameter(DataJsonSet.SoundDataDictionary[eSoundSet].parameterName[i], out pt);
                temp.states.Add(pt);
            }
            temp.obj.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.transform));
            temp.obj.setVolume(RunTimeDataSet.soundVolumeDic[DataJsonSet.SoundDataDictionary[eSoundSet].eSoundType]);
            playSoundDic.Add(eSoundSet, temp);
        }

        public static void Release(ESoundSet eSoundType)
        {
            playSoundDic[eSoundType].obj.start();
            playSoundDic[eSoundType].obj.release();
            playSoundDic.Remove(eSoundType);
        }

        public static void Stop()
        {
            ESoundSet eSoundType = ESoundSet.Empty;
            if(playSoundDic.ContainsKey(ESoundSet.Sizzling))
            {
                eSoundType = ESoundSet.Sizzling;
            }
            else if(playSoundDic.ContainsKey(ESoundSet.Boiling))
            {
                eSoundType = ESoundSet.Boiling;
            }
            else if(playSoundDic.ContainsKey(ESoundSet.Poizon))
            {
                eSoundType = ESoundSet.Poizon;
            }
            if (eSoundType == ESoundSet.Empty)
                return;
            playSoundDic[eSoundType].obj.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            playSoundDic[eSoundType].obj.release();
            playSoundDic.Remove(eSoundType);
        }

        public static void Stop(ESoundSet eSoundType)
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
