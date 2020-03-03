using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BlockHandler : MonoBehaviour
{
    
    public GameObject[] blocks = new GameObject[3];
    public Vector3[] relativeCord = {new Vector3(0,0,0), new Vector3(0,0,0), new Vector3(0,0,0), new Vector3(0,0,0)};
    public int indexOfFirst = 3;
    public string color = "red";
    
    void Awake()
    {
        blocks[0] = gameObject.transform.GetChild(0).gameObject;
        blocks[1] = gameObject.transform.GetChild(1).gameObject;
        blocks[2] = gameObject.transform.GetChild(2).gameObject;

        ResizeBlocks();
        CalculateRelativeCords();
        InitRotation();
        InitColor();
    }

    public void InitColor()
    {
        switch (color)
        {
            case "green":
                blocks[0].GetComponent<SpriteRenderer>().color = Colors.fadedGREEN;
                blocks[1].GetComponent<SpriteRenderer>().color = Colors.fadedGREEN;
                blocks[2].GetComponent<SpriteRenderer>().color = Colors.fadedGREEN;
                break;
            case "red":
                blocks[0].GetComponent<SpriteRenderer>().color = Colors.fadedRED;
                blocks[1].GetComponent<SpriteRenderer>().color = Colors.fadedRED;
                blocks[2].GetComponent<SpriteRenderer>().color = Colors.fadedRED;
                break;
            case "yellow":
                blocks[0].GetComponent<SpriteRenderer>().color = Colors.fadedYELLOW;
                blocks[1].GetComponent<SpriteRenderer>().color = Colors.fadedYELLOW;
                blocks[2].GetComponent<SpriteRenderer>().color = Colors.fadedYELLOW;
                break;
            case "blue":
                blocks[0].GetComponent<SpriteRenderer>().color = Colors.fadedBLUE;
                blocks[1].GetComponent<SpriteRenderer>().color = Colors.fadedBLUE;
                blocks[2].GetComponent<SpriteRenderer>().color = Colors.fadedBLUE;
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
    
    public void InitRotation()
    {
        for (int i = 0; i < 3; i++)
        {
           blocks[i].GetComponent<Transform>().localPosition = relativeCord[(i + indexOfFirst) % 4];
        }
    }
}
