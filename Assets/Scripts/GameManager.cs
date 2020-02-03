using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


public struct Cord
{
    public int x;
    public int y;
}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int[,] gameGrid = new int[Values.cellCount, Values.cellCount];
    private int onTurnPlayerIndex = 0;
    [SerializeField] private GameObject Player;
    private Cord CurrentCord;
    void Start()
    {
        ResetGameGrid();
        CurrentCord.x = (Values.cellCount + 1) / 2;
        CurrentCord.y = (Values.cellCount + 1) / 2;
    }
    
    void Update()
    {
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 4])))
        {
            Rotate("L");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 5])))
        {
            Rotate("R");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 0])))
        {
            OnMoveCalled("up");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 2])))
        {
            OnMoveCalled("down");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 1])))
        {
            OnMoveCalled("left");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 3])))
        {
            OnMoveCalled("right");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[onTurnPlayerIndex, 6])))
        {
            OnPlaceBlockCalled();
        }
    }

    void Rotate(string dir)
    {
        if(dir == "R")
            Player.GetComponent<PlayerHandler>().RotateRight();
        else
            Player.GetComponent<PlayerHandler>().RotateLeft(); 
    }

    void OnMoveCalled(string dir)
    {
        switch (dir)
        {
            case "up":
                if (CurrentCord.y + 1 < Values.cellCount)
                {
                    CurrentCord.y++;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "down":
                if (CurrentCord.y - 1 > 0)
                {
                    CurrentCord.y--;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "left":
                if (CurrentCord.x - 1 > 0)
                {
                    CurrentCord.x--;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "right":
                if (CurrentCord.x + 1 < Values.cellCount)
                {
                    CurrentCord.x++;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
        }
    }
    void ResetGameGrid()
    {
        for (int i = 0; i < Values.cellCount; i++)
        {
            for (int ii = 0; ii < Values.cellCount; ii++)
            {
                gameGrid[i,ii] = 0;
            }
        }
    }
    
    void OnPlaceBlockCalled()
    {
        if (IsPlaceFree())
        {
            PlaceBlock();
        }
    }

    void PlaceBlock()
    {
        onTurnPlayerIndex = (onTurnPlayerIndex + 1) % 2;
        
        if (IsGameFinished())
            NewGame();
        
        
    }

    bool IsPlaceFree()
    {
        
        return true;
    }
    
    bool IsGameFinished()
    {
        for (int i = 0; i < Values.cellCount - 1; i++)
        {
            for (int ii = 0; ii < Values.cellCount - 1; ii++)
            {
                if ((gameGrid[i, ii] + gameGrid[i + 1, ii] + gameGrid[i, ii + 1] + gameGrid[i + 1, ii + 1]) < 2)
                    return false;
            }
        }
        return true;
    }

    void NewGame()
    {
        ResetGameGrid();
    }
}
