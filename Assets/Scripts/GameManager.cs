using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] private GameObject Block;
    private Cord CurrentCord;
    private int[] playerScore = {0, 0};

    void Start()
    {
        ResetGameGrid();
        CurrentCord.x = (Values.cellCount + 1) / 2;
        CurrentCord.y = (Values.cellCount + 1) / 2;
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
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
        switch (Player.GetComponent<BlockHandler>().indexOfFirst)
        {
            case 0:
                gameGrid[CurrentCord.x, CurrentCord.y] = 1;
                gameGrid[CurrentCord.x - 1, CurrentCord.y] = 1;
                gameGrid[CurrentCord.x - 1, CurrentCord.y - 1] = 1;
                break;
            case 1:
                gameGrid[CurrentCord.x, CurrentCord.y - 1] = 1;
                gameGrid[CurrentCord.x - 1, CurrentCord.y] = 1;
                gameGrid[CurrentCord.x, CurrentCord.y] = 1; break;
            case 2:
                gameGrid[CurrentCord.x, CurrentCord.y - 1] = 1;
                gameGrid[CurrentCord.x - 1, CurrentCord.y - 1] = 1;
                gameGrid[CurrentCord.x, CurrentCord.y] = 1;
                break;
            case 3:
                gameGrid[CurrentCord.x - 1, CurrentCord.y] = 1;
                gameGrid[CurrentCord.x - 1, CurrentCord.y - 1] = 1;
                gameGrid[CurrentCord.x, CurrentCord.y - 1] = 1;
                break;
        }

        Block.GetComponent<BlockHandler>().indexOfFirst = Player.GetComponent<BlockHandler>().indexOfFirst;
        Block.GetComponent<BlockHandler>().color = Values.playerColor[onTurnPlayerIndex];
        Instantiate(Block, Player.transform.position,Quaternion.identity);
        onTurnPlayerIndex = (onTurnPlayerIndex + 1) % 2;
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
        if (IsGameFinished())
            NewGame();
    }

    bool IsPlaceFree()
    {
        switch (gameGrid[CurrentCord.x,CurrentCord.y] + gameGrid[CurrentCord.x - 1,CurrentCord.y] + gameGrid[CurrentCord.x,CurrentCord.y - 1] + gameGrid[CurrentCord.x - 1,CurrentCord.y - 1])
        {
            case 0:
                return true;
                break;
            case 1:
                switch (Player.GetComponent<BlockHandler>().indexOfFirst)
                {
                    case 0:
                        if (gameGrid[CurrentCord.x, CurrentCord.y - 1] == 1)
                            return true;
                        return false;
                    case 1:
                        if (gameGrid[CurrentCord.x - 1, CurrentCord.y - 1] == 1)
                            return true;
                        return false;
                    case 2:
                        if (gameGrid[CurrentCord.x - 1, CurrentCord.y] == 1)
                            return true;
                        return false;
                    case 3:
                        if (gameGrid[CurrentCord.x, CurrentCord.y] == 1)
                            return true;
                        return false;
                }
                return true;
                break;
            default:
                return false;
        }
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
        playerScore[(onTurnPlayerIndex + 1) % 2] += 1;
        GameObject []go = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject g in go) 
        {
            Destroy(g);
        }

        GameObject score1 = GameObject.FindGameObjectWithTag("Player1Score");
        GameObject score2 = GameObject.FindGameObjectWithTag("Player2Score");
        score1.GetComponent<Text>().text = playerScore[0].ToString();
        score2.GetComponent<Text>().text = playerScore[1].ToString();
        ResetGameGrid();
    }
}
