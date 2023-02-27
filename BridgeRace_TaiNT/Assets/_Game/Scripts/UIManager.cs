using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GOSingleton<UIManager>
{
    [SerializeField]private GameObject endGamePanel;

    public void OnEndGame()
    {
        endGamePanel.SetActive(true);
    }
    public void ButtonNewLevel()
    {
        endGamePanel.SetActive(false);
    }
}
