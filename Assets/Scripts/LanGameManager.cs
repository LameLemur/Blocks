using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class LanGameManager : NetworkBehaviour
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

    private void Awake()
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkManagerHUD>().host = Values.gameMode == "lanhost";
    }

    void Start()
    {
        CheckSerielizedFields();
        ResetGameGrid();
        CurrentCord.x = (Values.cellCount + 1) / 2;
        CurrentCord.y = (Values.cellCount + 1) / 2;
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
        
        Player1Name.GetComponent<Text>().text = Values.playerName[0];
        Player2Name.GetComponent<Text>().text = Values.playerName[1];
    }

    public void UpdateNameAndColors()
    {
        Player1Name.GetComponent<Text>().text = Values.playerName[0];
        Player2Name.GetComponent<Text>().text = Values.playerName[1];
        
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
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

    public void NewGame()
    {
        //Score
        if (IsGameFinished())
        {
            playerScore[(onTurnPlayerIndex + 1) % 2] += 1;
            Player1Score.GetComponent<Text>().text = playerScore[0].ToString();
            Player2Score.GetComponent<Text>().text = playerScore[1].ToString();
        }
        
        //Reset visual
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Block")) 
        {
            Destroy(g);
        }
        
        //Reset internal
        ResetGameGrid();
        
        //Reset Player
        ResetPlayer();
    }

    void ResetPlayer()
    {
        Player.GetComponent<BlockHandler>().indexOfFirst = 3;
        Player.GetComponent<BlockHandler>().InitRotation();
        Player.GetComponent<PlayerHandler>().SetColor(Values.playerColor[onTurnPlayerIndex]);
        Player.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        CurrentCord.x = (Values.cellCount + 1) / 2;
        CurrentCord.y = (Values.cellCount + 1) / 2;
    }
}
