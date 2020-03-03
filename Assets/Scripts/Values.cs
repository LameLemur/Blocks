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

    public static string[] playerColor = {"red", "green"};
    public static string[] playerName = {"Wilfréd", "Alfons"};

    public static string gameMode = "local";
}

public static class Colors
{
    public static Color RED    = ConvertColor( 168, 0, 102);
    public static Color GREEN  = ConvertColor( 168, 229, 48);
    public static Color BLUE = ConvertColor(7, 212, 213);
    public static Color YELLOW = ConvertColor( 250, 228, 8);
    public static Color fadedRED = ConvertColor(120, 4, 74);
    public static Color fadedGREEN = ConvertColor(103, 164, 4);
    public static Color fadedBLUE = ConvertColor(0, 134, 137);
    public static Color fadedYELLOW = ConvertColor(	234, 187, 4);
    
    static Color ConvertColor (float r, float g, float b)
    {
        return new Color(r/255.0f, g/255.0f, b/255.0f);
    }
}