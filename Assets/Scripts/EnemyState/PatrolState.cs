using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    [SerializeField] private PatrolManager _patrolManager;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _viewingDistance = 20f;
    [SerializeField] private float _viewingAngle = 50f;



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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = new Color(0, 1f, 0.3f, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -_viewingAngle, 0f) * transform.forward, _viewingAngle * 2f, _viewingDistance);
    }
#endif




}
