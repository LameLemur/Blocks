using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public void RotateRight()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<BlockHandler>().blocks[i].GetComponent<Transform>().localPosition = gameObject.GetComponent<BlockHandler>().relativeCord[(i + gameObject.GetComponent<BlockHandler>().indexOfFirst + 1) % 4];
        }
        gameObject.GetComponent<BlockHandler>().indexOfFirst = (gameObject.GetComponent<BlockHandler>().indexOfFirst + 1) % 4;
    }

    public void RotateLeft()
    {
        for (int i = 0; i < 3; i++)
        {
            gameObject.GetComponent<BlockHandler>().blocks[i].GetComponent<Transform>().localPosition = gameObject.GetComponent<BlockHandler>().relativeCord[(i + gameObject.GetComponent<BlockHandler>().indexOfFirst + 3) % 4];
        }
        gameObject.GetComponent<BlockHandler>().indexOfFirst = (gameObject.GetComponent<BlockHandler>().indexOfFirst - 1 + 4) % 4;
    }
    
    public void Move(string dir)
    {
        switch(dir)
        {
            case "up" :
                gameObject.GetComponent<Transform>().Translate( new Vector3(0, Values.GetCellSize(), 0));
                break;
            case "down" :
                gameObject.GetComponent<Transform>().Translate(new Vector3(0, -Values.GetCellSize(),0));
                break;
            case "left" :
                gameObject.GetComponent<Transform>().Translate(new Vector3(-Values.GetCellSize(),0,0));
                break;
            case "right" :
                gameObject.GetComponent<Transform>().Translate(new Vector3(Values.GetCellSize(),0,0));
                break;
        }
    }
}
