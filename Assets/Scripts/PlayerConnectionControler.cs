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

    public override void OnStartLocalPlayer()
    {
        CmdSyncNameAndColors();
    }

    void Update()
    {
        if (base.isServer)
        {
            playerID = 0;
        }
        if (!isLocalPlayer || playerID != LanManager.GetComponent<LanGameManager>().onTurnPlayerIndex || GameObject.Find("NetworkManager").GetComponent<NetworkManager>().numPlayers == 1)
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
        RpcMoveBlock(dir);
    }

    [ClientRpc]
    public void RpcMoveBlock(string dir)
    {
        LanManager.GetComponent<LanGameManager>().OnMoveCalled(dir);
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

    [Command]
    void CmdSyncNameAndColors()
    {
        RpcSyncNameAndColors(Values.playerName, Values.playerColor);
    }

    [ClientRpc]
    void RpcSyncNameAndColors(String[]playerName, String[]playerColor)
    {
        Values.playerColor = playerColor;
        Values.playerName = playerName;
        
        LanManager.GetComponent<LanGameManager>().UpdateNameAndColors();
    }
    private void OnDestroy()
    {
        LanManager.GetComponent<LanGameManager>().NewGame();
    }
}
