using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCharVoice : MonoBehaviour
{
    [FMODUnity.BankRef]
    public string path = "event:/Dialog/Voice_Sample1";
    public FMOD.Studio.EventInstance Voice_Sample1;
    
    // Start is called before the first frame update
    void Start()
    {
        Voice_Sample1 = FMODUnity.RuntimeManager.CreateInstance(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
