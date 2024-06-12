using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBotState : BotStateMachine
{
    protected AllyBotStateMachine _allyBotStateMachine;
  //protected List<EnemyStateMachine> _enemies = new List<EnemyStateMachine>();
    public virtual void Enter()
    { }

    public virtual void Process()
    { }

    public virtual void Exit()
    { }

    private void Update()
    {

     //   _enemies.Add(FindAnyObjectByType<EnemyStateMachine>());
    }
}
