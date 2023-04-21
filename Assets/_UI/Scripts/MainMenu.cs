using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    // Bắt đầu chơi
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<GamePlay>().SetPlayerJoystick();
        Close(0);
        Time.timeScale = 1;
    }
}
