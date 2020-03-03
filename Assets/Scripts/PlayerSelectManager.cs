using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    [SerializeField] private GameObject selectOne;
    [SerializeField] private GameObject selectTwo;
    [SerializeField] private GameObject nameOne;
    [SerializeField] private GameObject nameTwo;
    List<string> colors1 = new List<string>() {"Red", "Blue", "Yellow"};
    List<string> colors2 = new List<string>() {"Green", "Blue", "Yellow"};
    private string firstplayer = "Red";
    private string seondplayer = "Green";

    public void OnFirstPlayerUpdateCalled(int index)
    {
        colors2[colors2.IndexOf(colors1[index])] = firstplayer;
        firstplayer = colors1[index];
        
        selectTwo.GetComponent<Dropdown>().options.Clear();
        selectTwo.GetComponent<Dropdown>().AddOptions(colors2);
    }

    public void OnSecondPlayerUpdateCalled(int index)
    {
        colors1[colors1.IndexOf(colors2[index])] = seondplayer;
        seondplayer = colors2[index];

        selectOne.GetComponent<Dropdown>().options.Clear();
        selectOne.GetComponent<Dropdown>().AddOptions(colors1);
    }

    public void OnPlayButtonCalled()
    {
        Values.playerColor[0] = firstplayer.ToLower();
        Values.playerColor[1] = seondplayer.ToLower();
        Values.playerName[0] = nameOne.GetComponent<Text>().text;
        Values.playerName[1] = nameTwo.GetComponent<Text>().text;
        if(Values.gameMode == "local")
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changescene("local");
        else
            GameObject.Find("SceneChanger").GetComponent<SceneChanger>().changescene("lan");
    }
}
