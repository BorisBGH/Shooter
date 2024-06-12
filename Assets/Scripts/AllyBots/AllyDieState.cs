using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyDieState : AllyBotState
{
    private NavMeshAgent _navMeshAgent;
    private AllyBotsCreator _allyBotCreator;

    public void Init(AllyBotStateMachine enemyStateMachine, NavMeshAgent navMeshAgent, AllyBotsCreator allyCreator)
    {
        _allyBotStateMachine = enemyStateMachine;
        _navMeshAgent = navMeshAgent;
        _allyBotCreator = allyCreator;
    }
    public override void Enter()
    {
        base.Enter();
        _navMeshAgent.isStopped = true;
        Invoke(nameof(Respawn), 3f);

    }

    private void Respawn()
    {
        _allyBotCreator.Respawn();
    }
}
