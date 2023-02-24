using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{

    public void OnEnter(BotController bot)
    {
        bot.SetTarget();
    }

    public void OnExecute(BotController bot)
    {
    }

    public void OnExit(BotController bot)
    {
        
    }
}
