using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    float delayTime = 2f;
    float timer=0;
    public void OnEnter(BotController bot)
    {
        timer = 0;
        bot.ChangeAnim("falltoland");
        bot.Fall();
        bot.StopMoving();
    }

    public void OnExecute(BotController bot)
    {
        timer+=Time.deltaTime;
        if (timer > delayTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(BotController bot)
    {
        
    }
}
