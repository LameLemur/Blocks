using System.Collections;
using System.Collections.Generic;
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
    
    public void SetColor(string color)
    {
        switch (color)
        {
            case "green":
                gameObject.GetComponent<BlockHandler>().blocks[0].GetComponent<SpriteRenderer>().color = Colors.GREEN;
                gameObject.GetComponent<BlockHandler>().blocks[1].GetComponent<SpriteRenderer>().color = Colors.GREEN;
                gameObject.GetComponent<BlockHandler>().blocks[2].GetComponent<SpriteRenderer>().color = Colors.GREEN;
                break;
            case "red":
                gameObject.GetComponent<BlockHandler>().blocks[0].GetComponent<SpriteRenderer>().color = Colors.RED;
                gameObject.GetComponent<BlockHandler>().blocks[1].GetComponent<SpriteRenderer>().color = Colors.RED;
                gameObject.GetComponent<BlockHandler>().blocks[2].GetComponent<SpriteRenderer>().color = Colors.RED;
                break;
            case "yellow":
                gameObject.GetComponent<BlockHandler>().blocks[0].GetComponent<SpriteRenderer>().color = Colors.YELLOW;
                gameObject.GetComponent<BlockHandler>().blocks[1].GetComponent<SpriteRenderer>().color = Colors.YELLOW;
                gameObject.GetComponent<BlockHandler>().blocks[2].GetComponent<SpriteRenderer>().color = Colors.YELLOW;
                break;
            case "blue":
                gameObject.GetComponent<BlockHandler>().blocks[0].GetComponent<SpriteRenderer>().color = Colors.BLUE;
                gameObject.GetComponent<BlockHandler>().blocks[1].GetComponent<SpriteRenderer>().color = Colors.BLUE;
                gameObject.GetComponent<BlockHandler>().blocks[2].GetComponent<SpriteRenderer>().color = Colors.BLUE;
                break;
        }
    }
}
