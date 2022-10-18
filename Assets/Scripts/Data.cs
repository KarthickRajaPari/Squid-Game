using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public int score;
    public int level;
    public bool isMute;
    public bool isVibrate;


    public Data(UserInterface ui){
        score = ui.score;
        level = ui.level;
        isMute = ui.isMute;

    }
}
