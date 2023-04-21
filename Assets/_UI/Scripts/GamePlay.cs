using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    [SerializeField] private Player player;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject imageContinue;
    [SerializeField] private GameObject imagePause;
    [SerializeField] private GameObject imageReset;

    private bool isSetting = false;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    // Kéo joystick vào Player
    public void SetPlayerJoystick()
    {
        // Kiểm tra xem đối tượng nhân vật và joystick có tồn tại không
        if (player != null && joystick != null)
        {
            // Tìm đối tượng con của nhân vật có chứa component Joystick và gán joystick vào đó
            player.Joystick = joystick;
            player.TransformJoystick = joystick.transform.position;
        }
    }
    // Cài đặt
    public void Setting()
    {
        imageReset.SetActive(!isSetting);
        if (player.IsPause)
        {
            imageContinue.SetActive(!isSetting);
        } else
        {
            imagePause.SetActive(!isSetting);
        }
        isSetting = !isSetting;
    }
    // Tiếp tục
    public void Continue()
    {
        LevelManager.Ins.ContinueGame();
        imageContinue.SetActive(false);
        imagePause.SetActive(true);
    }
    // Dừng lại
    public void Pause()
    {
        LevelManager.Ins.PauseGame();
        imageContinue.SetActive(true);
        imagePause.SetActive(false);
    }
    // Tải lại
    public void ResetGame()
    {
        LevelManager.Ins.ResetGame();
        Continue();
    }
}
