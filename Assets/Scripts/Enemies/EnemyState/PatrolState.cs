using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyState
{
    [SerializeField] private Transform _aim;
    [SerializeField] private Transform _aimDefaultPos;

    [SerializeField] private float _viewingDistance = 20f;
    [SerializeField] private float _viewingAngle = 50f;
    [SerializeField] private LayerMask _wallMask;
    private Animator _animator;
    private PatrolManager _patrolManager;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerCenter;

    private List<AllyBotStateMachine> _allAllies = new List<AllyBotStateMachine>();
    private List<AllyBotStateMachine> _foundAllies = new List<AllyBotStateMachine>();


    public void Init(EnemyStateMachine enemyStateMachine, PatrolManager patrolManager, NavMeshAgent navMeshAgent, Transform playerCenter, Animator animator)
    {
        _enemyStateMachine = enemyStateMachine;
        _patrolManager = patrolManager;
        _navMeshAgent = navMeshAgent;
        _playerCenter = playerCenter;
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

        bool canSeePlayer = SearchUtility.SearchInSector(transform.position + Vector3.up * 1.5f, transform.forward, _playerCenter.position, _viewingAngle, _viewingDistance, _wallMask);

        _allAllies = FindObjectsOfType<AllyBotStateMachine>().ToList();
        _foundAllies = SearchUtility.SearchAlliesInSector(transform.position, transform.forward, _allAllies, _viewingAngle, _viewingDistance, _wallMask);
        List<Transform> targets = new List<Transform>();
        if (_foundAllies != null && _foundAllies.Count > 0)
        {
            foreach (var allyBot in _foundAllies)
            {
                targets.Add(allyBot.transform);
            }
        }

        if (canSeePlayer)
        {
            targets.Add(_playerCenter.transform);
        }      

        Transform closestTarget = SearchUtility.FindClosestTarget(transform.position, targets);
        if (closestTarget != null)
        {
            _enemyStateMachine.StartFollowState(closestTarget);
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
