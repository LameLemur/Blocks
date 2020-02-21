using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Values
{
    public static float gridSize = 4.0f;
    public static float innerWallThicknessScale = 0.5f;
    public static float outerWallThicknessScale = 1f;
    public static int cellCount = 9;
    
    public static string[,] keys = {{ "W", "A", "S", "D", "Q", "E", "Space"}, {  "UpArrow", "LeftArrow", "DownArrow", "RightArrow", "N", "M", "K"}};

    public static int lastSceneIndex = 0;
    
    public static float GetCellSize()
    {
        return (float)(gridSize / cellCount);
    }

    public static string []playerColor = {"red", "green"};
    public static string[] playerName = {"Wilfréd", "Alfons"};
}

public static class Colors
{
    public static Color RED    = new Color( 1, 0, 0);
    public static Color GREEN  = new Color( 0, 1, 0);
    public static Color BLUE   = new Color( 0, 0, 1);
    public static Color YELLOW = new Color( 1, 1, 0);
    public static Color fadedRED    = new Color( 0.8f, 0.2f, 0.2f);
    public static Color fadedGREEN  = new Color( 0.2f, 0.8f, 0.2f);
    public static Color fadedBLUE   = new Color( 0.3f, 0.3f, 0.7f);
    public static Color fadedYELLOW = new Color( 0.8f, 0.8f, 0.2f);
}