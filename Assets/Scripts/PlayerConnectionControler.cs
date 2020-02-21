using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
public class PlayerConnectionControler : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdateBlock))]
    private Cord CurrentCord;
    private int[] playerScore = {0, 0};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 4])))
        {
            Rotate("L");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 5])))
        {
            Rotate("R");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 0])))
        {
            OnMoveCalled("up");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 2])))
        {
            OnMoveCalled("down");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 1])))
        {
            OnMoveCalled("left");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 3])))
        {
            OnMoveCalled("right");
        }
        if (Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), Values.keys[0, 6])))
        {
            OnPlaceBlockCalled();
        }
    }

    void Rotate(string dir)
    {
        
    }

    void OnMoveCalled(string dir)
    {
        switch (dir)
        {
            case "up":
                if (CurrentCord.y + 1 < Values.cellCount)
                {
                    CurrentCord.y++;
                }
                break;
            case "down":
                if (CurrentCord.y - 1 > 0)
                {
                    CurrentCord.y--;
                }
                break;
            case "left":
                if (CurrentCord.x - 1 > 0)
                {
                    CurrentCord.x--;
                }
                break;
            case "right":
                if (CurrentCord.x + 1 < Values.cellCount)
                {
                    CurrentCord.x++;
                }
                break;
        }
    }

    void OnPlaceBlockCalled()
    {
        
    }

    void UpdateBlock(Cord currentCord)
    {
        
    }
}
