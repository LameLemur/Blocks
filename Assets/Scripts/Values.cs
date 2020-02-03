using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Values
{
    public static float gridSize = 4.0f;
    public static float innerWallThicknessScale = 0.5f;
    public static float outerWallThicknessScale = 1f;
    public static int cellCount = 12;
    
    public static string[,] keys = {{ "W", "A", "S", "D", "Q", "E", "Space"}, {  "UpArrow", "LeftArrow", "DownArrow", "RightArrow", "M", "N", "K"}};

    public static int lastSceneIndex = 0;
    
    public static float GetCellSize()
    {
        return (float)(gridSize / cellCount);
    }
}
