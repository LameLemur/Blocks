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
    public int indexOfFirst = 0;
    
    void Start()
    {
        blocks[0] = gameObject.transform.GetChild(0).gameObject;
        blocks[1] = gameObject.transform.GetChild(1).gameObject;
        blocks[2] = gameObject.transform.GetChild(2).gameObject;

        ResizeBlocks();
        CalculateRelativeCords();
        
    }

    public void ChangeColor(string color)
    {
        switch (color)
        {
            case "green":
                blocks[0].GetComponent<SpriteRenderer>().color = Color.green;
                blocks[1].GetComponent<SpriteRenderer>().color = Color.green;
                blocks[2].GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case "red":
                blocks[0].GetComponent<SpriteRenderer>().color = Color.red;
                blocks[1].GetComponent<SpriteRenderer>().color = Color.red;
                blocks[2].GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "fadegreen":
                break;
            case "fadered":
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
