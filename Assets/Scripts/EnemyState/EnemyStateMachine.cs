using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private PatrolState _patrolState;
    [SerializeField] private FollowState _followState;
    [SerializeField] private HittedState _hittedState;
    [SerializeField] private DieState _dieState;


    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Rig _rig;


    private EnemyState _currentState;

    public void Init(PatrolManager patrolManager, Transform playerCenter, EnemyCreator enemyCreator)
    {
        _patrolState.Init(this, patrolManager, _navMeshAgent, playerCenter, _animator);
        _followState.Init(this, _navMeshAgent, playerCenter, _animator);
        _hittedState.Init(this, _navMeshAgent, _animator, _rig);
        _dieState.Init(this, _navMeshAgent, enemyCreator);
    }
    private void Start()
    {
        SetState(_patrolState);
    }

    private void Update()
    {
        if (_currentState)
        {
            _currentState.Process();
        }
    }

    public void StartPatrolState()
    {
        SetState(_patrolState);
    }

    public void StartFollowState()
    {
        SetState(_followState);
    }

    public void StartHittedState()
    {
        SetState(_hittedState);
    }

    public void StartDieState()
    {
        SetState(_dieState);
    }


    private void SetState(EnemyState enemyState)
    {
        if (_currentState && _currentState != enemyState)
        {
            _currentState.Exit();
        }
        _currentState = enemyState;
        _currentState.Enter();
    }
}
