using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GOSingleton<LevelManager>
{
    int currentLevel=1;
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    private void Start()
    {
        
    }
    public void Goto(int Level)
    {
        if(currentLevel>1)    
            levels[currentLevel - 1].SetActive(false);
        currentLevel = Level;
        levels[currentLevel - 1].SetActive(true);
    }
    public void GotoNextLevel()
    {
        levels[currentLevel - 1].SetActive(false);
        currentLevel += 1;
        SpawnManager.GetInstance().Floors = StaticData.GetInstance().l_floorLevel[currentLevel-1];
        SpawnManager.GetInstance().winPos = StaticData.GetInstance().l_winPos[currentLevel-1];
        levels[currentLevel - 1].SetActive(true);
    }
}
