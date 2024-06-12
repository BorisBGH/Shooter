using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AllyPatrolState : AllyBotState
{
    [SerializeField] private Transform _aim;
    [SerializeField] private Transform _aimDefaultPos;

    [SerializeField] private float _viewingDistance = 20f;
    [SerializeField] private float _viewingAngle = 50f;
    [SerializeField] private LayerMask _wallMask;
    private Animator _animator;
    private PatrolManager _patrolManager;
    private NavMeshAgent _navMeshAgent;


    List<EnemyStateMachine> allEnemies = new List<EnemyStateMachine>();
    List<EnemyStateMachine> foundEnemies = new List<EnemyStateMachine>();


    public void Init(AllyBotStateMachine allyBotStateMachine, PatrolManager patrolManager, NavMeshAgent navMeshAgent, Transform playerCenter, Animator animator)
    {
        _allyBotStateMachine = allyBotStateMachine;
        _patrolManager = patrolManager;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }
    public override void Enter()
    {
        base.Enter();
        _navMeshAgent.isStopped = false;
        _animator.SetBool("Walk", true);
        EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
        _navMeshAgent.SetDestination(targetPoint.transform.position);
    }

    public override void Process()
    {
        base.Process();

        _aim.position = Vector3.Lerp(_aim.position, _aimDefaultPos.position, Time.deltaTime * 4f);

        if (_navMeshAgent.remainingDistance < 0.5f)
        {
            EnemyTargetPoint targetPoint = _patrolManager.GetRandomPoint();
            _navMeshAgent.SetDestination(targetPoint.transform.position);
        }

        allEnemies = FindObjectsOfType<EnemyStateMachine>().ToList();
        foundEnemies = SearchUtility.SearchEnemiesInSector(transform.position + Vector3.up * 1.5f, transform.forward, allEnemies, _viewingAngle, _viewingDistance, _wallMask);

        if (foundEnemies != null && foundEnemies.Count > 0)
        {
            _allyBotStateMachine.StartFollowState();
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
