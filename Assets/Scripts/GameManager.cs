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
    //Prefabs and player object init
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Block;
    
    //UI objects init
    [SerializeField] private GameObject Player1Name;
    [SerializeField] private GameObject Player2Name;
    [SerializeField] private GameObject Player1Score;
    [SerializeField] private GameObject Player2Score;
    
    //Game variable
    private int[,] gameGrid = new int[Values.cellCount, Values.cellCount];
    private int onTurnPlayerIndex = 0;
    private Cord currentCord;
    private int[] playerScore = {0, 0};

    void Start()
    {
        //Internal logic setup
        ResetGameGrid();
        currentCord.x = (Values.cellCount + 1) / 2;
        currentCord.y = (Values.cellCount + 1) / 2;
        
        //UI setup
        Player1Name.GetComponent<Text>().text = Values.playerName[0];
        Player2Name.GetComponent<Text>().text = Values.playerName[1];
        
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
        
        CheckSerielizedFields();
    }
    
    void CheckSerielizedFields()
    {
        if (Player1Name == null)
            Player1Name = GameObject.Find("Player1Name");
        if (Player2Name == null)
            Player2Name = GameObject.Find("Player2Name");
        if (Player1Score == null)
            Player1Score = GameObject.Find("Player1 score");
        if (Player2Score == null)
            Player2Score = GameObject.Find("Player2 score");
    }
    
    void Update()
    {
        //Controls
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
                if (currentCord.y + 1 < Values.cellCount)
                {
                    currentCord.y++;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "down":
                if (currentCord.y - 1 > 0)
                {
                    currentCord.y--;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "left":
                if (currentCord.x - 1 > 0)
                {
                    currentCord.x--;
                    Player.GetComponent<PlayerHandler>().Move(dir);
                }
                break;
            case "right":
                if (currentCord.x + 1 < Values.cellCount)
                {
                    currentCord.x++;
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
                gameGrid[currentCord.x, currentCord.y] = 1;
                gameGrid[currentCord.x - 1, currentCord.y] = 1;
                gameGrid[currentCord.x - 1, currentCord.y - 1] = 1;
                break;
            case 1:
                gameGrid[currentCord.x, currentCord.y - 1] = 1;
                gameGrid[currentCord.x - 1, currentCord.y] = 1;
                gameGrid[currentCord.x, currentCord.y] = 1;
                break;
            case 2:
                gameGrid[currentCord.x, currentCord.y - 1] = 1;
                gameGrid[currentCord.x - 1, currentCord.y - 1] = 1;
                gameGrid[currentCord.x, currentCord.y] = 1;
                break;
            case 3:
                gameGrid[currentCord.x - 1, currentCord.y] = 1;
                gameGrid[currentCord.x - 1, currentCord.y - 1] = 1;
                gameGrid[currentCord.x, currentCord.y - 1] = 1;
                break;
        }
        
        //Creates new Block
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
        switch (gameGrid[currentCord.x,currentCord.y] + gameGrid[currentCord.x - 1,currentCord.y] + gameGrid[currentCord.x,currentCord.y - 1] + gameGrid[currentCord.x - 1,currentCord.y - 1])
        {
            case 0:
                return true;
            case 1:
                switch (Player.GetComponent<BlockHandler>().indexOfFirst)
                {
                    case 0:
                        if (gameGrid[currentCord.x, currentCord.y - 1] == 1)
                            return true;
                        return false;
                    case 1:
                        if (gameGrid[currentCord.x - 1, currentCord.y - 1] == 1)
                            return true;
                        return false;
                    case 2:
                        if (gameGrid[currentCord.x - 1, currentCord.y] == 1)
                            return true;
                        return false;
                    case 3:
                        if (gameGrid[currentCord.x, currentCord.y] == 1)
                            return true;
                        return false;
                }
                return true;
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
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Block")) 
        {
            Destroy(g);
        }
        
        Player1Score.GetComponent<Text>().text = playerScore[0].ToString();
        Player2Score.GetComponent<Text>().text = playerScore[1].ToString();
        ResetGameGrid();
    }
}
