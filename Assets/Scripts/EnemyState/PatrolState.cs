using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    [SerializeField] private PatrolManager _patrolManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;

   

    public override void Enter()
    {
        base.Enter();
        EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
        _navMeshAgent.SetDestination(targetPoint.transform.position);
    }

    public override void Process()
    {
        base.Process();
        if (_navMeshAgent.remainingDistance < 0.5f)
        {
            EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
            _navMeshAgent.SetDestination(targetPoint.transform.position);
        }
    }

   


}
