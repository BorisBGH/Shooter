using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DieState : EnemyState
{
    private NavMeshAgent _navMeshAgent;
    private EnemyCreator _enemyCreator;

    public void Init(EnemyStateMachine enemyStateMachine, NavMeshAgent navMeshAgent, EnemyCreator enemyCreator)
    {
        _enemyStateMachine = enemyStateMachine;
        _navMeshAgent = navMeshAgent;
        _enemyCreator = enemyCreator;
    }
    public override void Enter()
    {
        base.Enter();
        _navMeshAgent.isStopped = true;
        Invoke(nameof(Respawn), 3f);

    }

    private void Respawn()
    {
        _enemyCreator.Respawn();
    }
}
