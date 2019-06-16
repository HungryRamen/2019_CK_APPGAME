using CharData;
using RunTimeData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameUIMgr : MonoBehaviour
{
    public Text nameText;
    private void Awake()
    {
        RunTimeDataSet.day = "1";
        RunTimeDataSet.lockMaterials.Clear();
        RunTimeDataSet.lockMaterials.Add("FM04");
        RunTimeDataSet.lockMaterials.Add("FM05");
        RunTimeDataSet.lockMaterials.Add("FM07");
        RunTimeDataSet.lockMaterials.Add("FM11");
        RunTimeDataSet.lockMaterials.Add("FM12");
        RunTimeDataSet.lockMaterials.Add("FM13");
        RunTimeDataSet.lockMaterials.Add("FM14");
        CharDataSet.charDataDictionary.Clear();
        CharDataSet.charDataDictionary.Add("CH01", new CharDataStructure());
        CharDataSet.charDataDictionary.Add("CH02", new CharDataStructure());
        CharDataSet.charDataDictionary.Add("CH03", new CharDataStructure());
        CharDataSet.charDataDictionary.Add("CH05", new CharDataStructure());
        CharDataSet.charDataDictionary.Add("CH06", new CharDataStructure());
    }
    public void SceneLoad()
    {
        if (nameText.text != "")
            RunTimeDataSet.userName = nameText.text;
        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefebs/Fade"));
        obj.GetComponent<Util.SceneMgr>().LoadScene(1.0f, () => UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"));
    }
}
