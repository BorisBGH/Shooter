using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : EnemyState
{
    [SerializeField] private Transform _aim;
    [SerializeField] private EnemyShooting _enemyShooting;
    [SerializeField] private float _viewingDistance = 20f;
    [SerializeField] private float _viewingAngle = 50f;
    [SerializeField] private LayerMask _wallMask;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerCenter;
    private Animator _animator;
    private Coroutine _coroutine;
    private float _lostPlayerTimer;
    

    public void Init(EnemyStateMachine enemyStateMachine, NavMeshAgent navMeshAgent, Transform playerCenter, Animator animator)
    {
        _enemyStateMachine = enemyStateMachine;
        _navMeshAgent = navMeshAgent;
        _playerCenter = playerCenter;
        _animator = animator;
    }
    public override void Enter()
    {
        base.Enter();
        _lostPlayerTimer = 0;
       _coroutine = StartCoroutine(Behaviour());       
    }

    public override void Process()
    {
        base.Process();
        _lostPlayerTimer += Time.deltaTime;
        bool seePlayer = SearchUtility.SearchInSector(transform.position, transform.forward, _playerCenter.position, _viewingAngle, _viewingDistance, _wallMask);
        if(seePlayer)
        {
            _lostPlayerTimer = 0;
        }
        if (_lostPlayerTimer > 4f)
        {
            _enemyStateMachine.StartPatrolState();
        }
       
    }

    private IEnumerator Behaviour()
    {
        while (true)
        {
            _navMeshAgent.isStopped = true;
            _animator.SetBool("Walk", false);
            _animator.SetBool("Crouch", false);
            float timer = Random.Range(.5f, 1.5f);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                _aim.position = Vector3.Lerp(_aim.position, _playerCenter.position, Time.deltaTime * 5f);
                _enemyShooting.Process();
                yield return null;
            }
            _navMeshAgent.isStopped = false;
            _animator.SetBool("Walk", true);
            _animator.SetBool("Crouch", true);

            timer = Random.Range(1f, 2f);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                _navMeshAgent.SetDestination(_playerCenter.position);
                _aim.position = Vector3.Lerp(_aim.position, _playerCenter.position, Time.deltaTime * 5f);
                yield return null;
            }
            yield return null;
        }        
    }
    public override void Exit()
    {
        base.Exit();
        _animator.SetBool("Walk", false);
        _animator.SetBool("Crouch", false);
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

    }
}
