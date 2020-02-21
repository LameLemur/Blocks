using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mirror;
using UnityEngine;
public class PlayerConnectionControler : NetworkBehaviour
{
    public int playerID = 1;
    private GameObject LanManager;
    private void Start()
    {
        LanManager = GameObject.Find("LanGameManager");
    }

    private void OnConnectedToServer()
    {
        playerID = 0;
    }

    void Update()
    {
        Debug.Log(playerID);
        if (!isLocalPlayer)
            return;
        if (playerID != LanManager.GetComponent<LanGameManager>().onTurnPlayerIndex)
            return;
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 4])))
        {
            CmdRotate("L");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 5])))
        {
            CmdRotate("R");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 0])))
        {
            CmdOnMoveCalled("up");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 2])))
        {
            CmdOnMoveCalled("down");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 1])))
        {
            CmdOnMoveCalled("left");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 3])))
        {
            CmdOnMoveCalled("right");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 6])))
        {
            CmdOnPlaceCalled();
        }
    }

    [Command]
    void CmdOnMoveCalled(string dir)
    {
        LanManager.GetComponent<LanGameManager>().OnMoveCalled(dir, gameObject);
    }

    [ClientRpc]
    public void RpcMoveBlock(string dir)
    {
        LanManager.GetComponent<LanGameManager>().MoveBlock(dir);
    }
    
    [Command]
    void CmdRotate(string dir)
    {
        RpcRotate(dir);
    }
    
    [ClientRpc]
    public void RpcRotate(string dir)
    {
        LanManager.GetComponent<LanGameManager>().Rotate(dir);
    }
    
    [Command]
    void CmdOnPlaceCalled()
    {
        LanManager.GetComponent<LanGameManager>().OnPlaceCalled(gameObject);
    }
    
    [ClientRpc]
    public void RpcPlace()
    {
        LanManager.GetComponent<LanGameManager>().PlaceBlock();
    }
}
