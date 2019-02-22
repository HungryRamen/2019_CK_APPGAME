using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetLoad : MonoBehaviour
{

    void Awake()
    {
        Char_Script charScript = Resources.Load<Char_Script>("New Char_Script");
        for(int index = 0; index < charScript.dataArray.Length; index++)
        {
            Debug.LogFormat("{0} : {1}", charScript.dataArray[index].Key, charScript.dataArray[index].Command);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
