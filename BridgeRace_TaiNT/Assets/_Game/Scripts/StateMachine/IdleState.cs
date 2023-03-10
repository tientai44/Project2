using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(BotController bot)
    {
        timer = 0;
        bot.ChangeAnim("idle");
        bot.StopMoving();
        randomTime = Random.Range(0.5f, 1f);
    }

    public void OnExecute(BotController bot)
    {
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            bot.ChangeState(new PatrolState());
        }
    }

    public void OnExit(BotController bot)
    {
        
    }
}
