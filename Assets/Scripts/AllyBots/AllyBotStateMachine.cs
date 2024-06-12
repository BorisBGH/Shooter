using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AllyBotStateMachine : BotStateMachine
{
    [SerializeField] private AllyPatrolState _allyPatrolState;
    [SerializeField] private AllyFollowState _allyFollowState;
    [SerializeField] private AllyHittedState _hittedState;
    [SerializeField] private AllyDieState _dieState;


    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Rig _rig;


    private AllyBotState _currentState;

    public void Init(PatrolManager patrolManager, Transform playerCenter, AllyBotsCreator allyBotsCreator)
    {
        _allyPatrolState.Init(this, patrolManager, _navMeshAgent, playerCenter, _animator);
        _allyFollowState.Init(this, _navMeshAgent, playerCenter, _animator);
        _hittedState.Init(this, _navMeshAgent, _animator, _rig);
        _dieState.Init(this, _navMeshAgent, allyBotsCreator);
    }
    private void Start()
    {
        SetState(_allyPatrolState);
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
        SetState(_allyPatrolState);
    }

    public void StartFollowState()
    {
        SetState(_allyFollowState);
    }

    public override void StartHittedState()
    {
        SetState(_hittedState);
    }

    public override void StartDieState()
    {
        SetState(_dieState);
    }


    private void SetState(AllyBotState allyBotState)
    {
        if (_currentState && _currentState != allyBotState)
        {
            _currentState.Exit();
        }
        _currentState = allyBotState;
        _currentState.Enter();
    }
}
