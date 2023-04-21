using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : UICanvas
{
    // Chơi lại
    public void PlayAgain()
    {
        LevelManager.Ins.ResetGame();
    }
}