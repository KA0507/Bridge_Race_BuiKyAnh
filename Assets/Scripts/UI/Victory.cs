using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : UICanvas
{
    // Chơi lại
    public void PlayAgain()
    {
        LevelManager.Ins.ResetGame();
    }
    // Chuyển level
    public void NextLevel()
    {
        LevelManager.Ins.LoadNextScene();
    }
}
