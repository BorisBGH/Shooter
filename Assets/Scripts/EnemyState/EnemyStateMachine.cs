using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private PatrolState _patrolState;

    private EnemyState _currentState;

    private void Start()
    {
        SetState(_patrolState);
    }

    private void Update()
    {
        if(_currentState)
        {
            _currentState.Process();
        }
    }

    public void StartPatrolState()
    {
        SetState(_patrolState);
    }

    private void SetState(EnemyState enemyState)
    {
        if(_currentState && _currentState != enemyState)
        {
            _currentState.Exit();
        }
        _currentState = enemyState;
        _currentState.Enter();
    }
}
