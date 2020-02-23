using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class LanGameManager : MonoBehaviour
{
    private int[,] gameGrid = new int[Values.cellCount, Values.cellCount];
    public int onTurnPlayerIndex = 0;
    
    private Cord CurrentCord;
    
    private int[] playerScore = {0, 0};
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Block;
    [SerializeField] private GameObject Player1Name;
    [SerializeField] private GameObject Player2Name;
    [SerializeField] private GameObject Player1Score;
    [SerializeField] private GameObject Player2Score;

    void Start()
    {
        ResetGameGrid();
        CurrentCord.x = (Values.cellCount + 1) / 2;
        CurrentCord.y = (Values.cellCount + 1) / 2;
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
        
        Player1Name.GetComponent<Text>().text = Values.playerName[0];
        Player2Name.GetComponent<Text>().text = Values.playerName[1];
        
    }
    
    

    public void Rotate(string dir)
    {
        if(dir == "R")
            Player.GetComponent<PlayerHandler>().RotateRight();
        else
            Player.GetComponent<PlayerHandler>().RotateLeft(); 
    }

    public void OnMoveCalled(string dir)
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
    
    public void OnPlaceCalled(GameObject sender)
    {
        if (IsPlaceFree())
        { 
            sender.GetComponent<PlayerConnectionControler>().RpcPlace();
        }
    }

    public void PlaceBlock()
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
                gameGrid[CurrentCord.x, CurrentCord.y] = 1; 
                break;
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
        PrintGame();
        if (IsGameFinished())
        {
            NewGame();
        }
    }

    bool IsPlaceFree()
    {
        switch (gameGrid[CurrentCord.x,CurrentCord.y] + gameGrid[CurrentCord.x - 1,CurrentCord.y] + gameGrid[CurrentCord.x,CurrentCord.y - 1] + gameGrid[CurrentCord.x - 1,CurrentCord.y - 1])
        {
            case 0:
                return true;
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
        Player1Score.GetComponent<Text>().text = playerScore[0].ToString();
        Player2Score.GetComponent<Text>().text = playerScore[1].ToString();
        ResetGameGrid();
    }

    void PrintGame()
    {
        Debug.Log(CurrentCord.x + " " + CurrentCord.y);
        Debug.Log("–––––––––––––––––––––––––––––––––––––––––");
        string []radky = new string[Values.cellCount];
        for (int i = 0; i < Values.cellCount; i++)
        {
            for (int a = 0; a < Values.cellCount; a++)
            {
                radky[i] += gameGrid[i,a].ToString() + " ";
            }
            Debug.Log(radky[i]);
        }
    }
}
