using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    [SerializeField] private GameObject imageContinue;
    [SerializeField] private GameObject imagePause;
    [SerializeField] private GameObject imageReset;

    public void Continue()
    {
        LevelManager.Ins.ContinueGame();
        imageContinue.SetActive(false);
        imagePause.SetActive(true);
    }
    public void Pause()
    {
        LevelManager.Ins.PauseGame();
        imageContinue.SetActive(true);
        imagePause.SetActive(false);
    }
    public void ResetGame()
    {
        LevelManager.Ins.ResetGame();
        Continue();
    }
}
