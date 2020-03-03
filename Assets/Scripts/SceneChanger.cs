using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void changescene(string name)
    {
        Values.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        switch (name)
        {
            case "menu":
                SceneManager.LoadScene(0);
                break;
            case "single":
                SceneManager.LoadScene(1);
                break;
            case "local":
                SceneManager.LoadScene(2);
                break;
            case "lan":
                SceneManager.LoadScene(3);
                break;
            case "sett":
                SceneManager.LoadScene(4);
                break;
            case "tut":
                SceneManager.LoadScene(5);
                break;
            case "localselect":
                Values.gameMode = "local";
                SceneManager.LoadScene(6);
                break;
            case "lanselect":
                Values.gameMode = "lanhost";
                SceneManager.LoadScene(7);
                break;
            case "singleselect":
                Values.gameMode = "single";
                SceneManager.LoadScene(8);
                break;
            case "lanmodeselect":
                Values.gameMode = "lan";
                SceneManager.LoadScene(9);
                break;
        }
    }
    
    public void changescene(int index)
    {
        Values.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(index);
    }
    
    public void changescene()
    {
        SceneManager.LoadScene(Values.lastSceneIndex);
    }
    
    public void quit()
    {
        Application.Quit();
    }
}

