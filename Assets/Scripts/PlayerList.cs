using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    public UserInterface userInterface;
    public int playerCount;
    int playerToFollow = 0;
    void Update()
    {

        if(playerCount <= 0){
            userInterface.LevelLost();
        }
        transform.position = transform.GetChild(playerToFollow).position;
    }
}
