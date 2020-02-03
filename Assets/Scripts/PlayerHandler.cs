using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerHandler : MonoBehaviour
{
    public GameObject[] blocks = new GameObject[3];
    private Vector3[] relativeCord = {new Vector3(0,0,0), new Vector3(0,0,0), new Vector3(0,0,0), new Vector3(0,0,0)};
    public int indexOfFirst = 0;
    
    void Start()
    {
        blocks[0] = gameObject.transform.GetChild(0).gameObject;
        blocks[1] = gameObject.transform.GetChild(1).gameObject;
        blocks[2] = gameObject.transform.GetChild(2).gameObject;

        ResizeBlocks();
        CalculateRelativeCords();
        
    }
    
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (blocks[0].GetComponent<SpriteRenderer>().color == Color.green)
        {
            blocks[0].GetComponent<SpriteRenderer>().color = Color.red;
            blocks[1].GetComponent<SpriteRenderer>().color = Color.red;
            blocks[2].GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            blocks[0].GetComponent<SpriteRenderer>().color = Color.green;
            blocks[1].GetComponent<SpriteRenderer>().color = Color.green;
            blocks[2].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    
    public void RotateRight()
    {
        for (int i = 0; i < 3; i++)
        {
            blocks[i].GetComponent<Transform>().localPosition = relativeCord[(i + indexOfFirst + 1) % 4];
        }
        indexOfFirst = (indexOfFirst + 1) % 4;
    }

    public void RotateLeft()
    {
        for (int i = 0; i < 3; i++)
        {
            blocks[i].GetComponent<Transform>().localPosition = relativeCord[(i + indexOfFirst + 3) % 4];
        }
        indexOfFirst = (indexOfFirst - 1 + 4) % 4;
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

    private void CalculateRelativeCords()
    {
        float offSet = Values.GetCellSize();
        if(Values.cellCount % 2 == 1)
        {
            relativeCord[1].y = offSet;
            relativeCord[3].x = offSet;
            relativeCord[2].x = offSet;
            relativeCord[2].y = offSet;
        }
        else
        {
            relativeCord[0].x = -offSet / 2;
            relativeCord[0].y = -offSet / 2;
            relativeCord[1].y = offSet / 2;
            relativeCord[1].x = -offSet / 2;
            relativeCord[3].x = offSet / 2;
            relativeCord[3].y = -offSet / 2;
            relativeCord[2].x = offSet / 2;
            relativeCord[2].y = offSet / 2;
        }
        

        blocks[0].transform.localPosition = relativeCord[0];
        blocks[1].transform.localPosition = relativeCord[1];
        blocks[2].transform.localPosition = relativeCord[3];
        
    }
    
    private void ResizeBlocks()
    {
        float size = blocks[0].GetComponent<Renderer>().bounds.size.x;
        Vector3 scale = blocks[0].GetComponent<Transform>().localScale;
        scale.x = (((Values.gridSize / Values.cellCount) - GridManager.innerWallThickness) * scale.x) / size;
        scale.y = scale.x;
        blocks[0].GetComponent<Transform>().localScale = scale;
        blocks[1].GetComponent<Transform>().localScale = scale;
        blocks[2].GetComponent<Transform>().localScale = scale;
    }
}
