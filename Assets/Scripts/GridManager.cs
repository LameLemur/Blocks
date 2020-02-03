using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridManager : MonoBehaviour {
    
    [SerializeField] private GameObject innerWall;
    [SerializeField] private GameObject outerWall;
    public static float innerWallThickness;

    void Awake() {

        SetUpBorder();
        SetUpWalls();
        SetUpBackground();
    }

    void SetUpWalls() {
        
        float size = innerWall.GetComponent<Renderer>().bounds.size.x;
        Vector3 scale = innerWall.GetComponent<Transform>().localScale;
        scale.x = Values.gridSize * scale.x / size;
        innerWall.GetComponent<Transform>().localScale = new Vector3(scale.x, Values.innerWallThicknessScale, 1);
        
        innerWallThickness = innerWall.GetComponent<Renderer>().bounds.size.y;
        
        for(int i = -Values.cellCount / 2; i < Values.cellCount / 2;i++) {
            Instantiate(innerWall, new Vector3(0, (Values.GetCellSize() * i + Values.GetCellSize() / (1 + Values.cellCount % 2)),  0), Quaternion.identity);
        }
        
        size = innerWall.GetComponent<Renderer>().bounds.size.y;
        scale = innerWall.GetComponent<Transform>().localScale;
        scale.y = Values.gridSize * scale.y / size;
        innerWall.GetComponent<Transform>().localScale = new Vector3(Values.innerWallThicknessScale,scale.y,  1);

        for(int i = -Values.cellCount / 2; i < Values.cellCount / 2;i++) {
            Instantiate(innerWall, new Vector3( (Values.GetCellSize() * i + Values.GetCellSize() / (1 + Values.cellCount % 2)), 0, 0), Quaternion.identity);
        }
    }
    void SetUpBorder() {
        
        float size = outerWall.GetComponent<Renderer>().bounds.size.y;
        float thickness;
        Vector3 scale = outerWall.GetComponent<Transform>().localScale;
        scale.y = (Values.gridSize + outerWall.GetComponent<Renderer>().bounds.size.y + innerWall.GetComponent<Renderer>().bounds.size.x) * scale.y / size;
        outerWall.GetComponent<Transform>().localScale = new Vector3(Values.outerWallThicknessScale, scale.y,  1);
        thickness = outerWall.GetComponent<Renderer>().bounds.size.y - innerWall.GetComponent<Renderer>().bounds.size.x;
        Instantiate(outerWall, new Vector3((Values.GetCellSize() * (-Values.cellCount / 2)) - (Values.cellCount % 2 * Values.GetCellSize() / 2) - CalculateOuterWallOffset(thickness),0,0), Quaternion.identity);
        Instantiate(outerWall, new Vector3((Values.GetCellSize() * (Values.cellCount / 2)) + (Values.cellCount % 2 * Values.GetCellSize() / 2) + CalculateOuterWallOffset(thickness),0,   0), Quaternion.identity);
        outerWall.GetComponent<Transform>().localScale = new Vector3(scale.y, Values.outerWallThicknessScale, 1);
        Instantiate(outerWall, new Vector3(0,(Values.GetCellSize() * (-Values.cellCount / 2)) - (Values.cellCount % 2 * Values.GetCellSize() / 2) - CalculateOuterWallOffset(thickness),0), Quaternion.identity);
        Instantiate(outerWall, new Vector3(0,(Values.GetCellSize() * (Values.cellCount / 2)) + (Values.cellCount % 2 * Values.GetCellSize() / 2) + CalculateOuterWallOffset(thickness),0), Quaternion.identity);
    }

    float CalculateOuterWallOffset(float outerWallSize)
    {
        float res = 0;
        res = (outerWallSize - innerWall.GetComponent<Renderer>().bounds.size.y) / 4;
        return res;
    }
    void SetUpBackground() {
        
        float sizeY = gameObject.GetComponent<Renderer>().bounds.size.y;
        float sizeX = gameObject.GetComponent<Renderer>().bounds.size.x;
        Vector3 scale = gameObject.GetComponent<Transform>().localScale;
        scale.y = (Values.gridSize) * scale.y / sizeY;
        scale.x = scale.y;
        gameObject.GetComponent<Transform>().localScale = scale;
    }
}
