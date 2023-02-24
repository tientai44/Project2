using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GOSingleton<GameController>
{
    [SerializeField] Transform winPos;
    [SerializeField] private PlayerController player;
    [SerializeField] private FixedJoystick _joystick;
    bool isGameOver = false;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    // Update is called once per frame
    void Update()
    {
        player.Move(_joystick);
    }
    public void GameOver()
    {
        isGameOver = true;
        UIManager.GetInstance().OnEndGame();
    }
}
