using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatePatrol : MonoBehaviour
{
    [SerializeField] private PatrolManager _patrolManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
        _navMeshAgent.SetDestination(targetPoint.transform.position);
    }

    private void Update()
    {
        if (_navMeshAgent.remainingDistance < 0.5f)
        {
            EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
            _navMeshAgent.SetDestination(targetPoint.transform.position);
        }
    }
}
