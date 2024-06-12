using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float _lostTargetTimer;

    private List<AllyBotStateMachine> _allAllies = new List<AllyBotStateMachine>();
    private List<AllyBotStateMachine> _foundAllies = new List<AllyBotStateMachine>();
    private AllyBotStateMachine _closestAlly;

    private Transform _closestTarget;

    public void Init(EnemyStateMachine enemyStateMachine, NavMeshAgent navMeshAgent, Transform playerCenter, Animator animator)
    {
        _enemyStateMachine = enemyStateMachine;
        _navMeshAgent = navMeshAgent;
        _playerCenter = playerCenter;
        _animator = animator;
    }
    public override void Enter(Transform target)
    {
        base.Enter();
        _closestTarget = target;
        _lostTargetTimer = 0;
        _coroutine = StartCoroutine(Behaviour());
    }

    public override void Process()
    {
        base.Process();
        _lostTargetTimer += Time.deltaTime;
        
        if (_closestTarget)
        {
            _lostTargetTimer = 0;
        }
        if (_lostTargetTimer > 4f)
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
                _aim.position = Vector3.Lerp(_aim.position, _closestTarget.position, Time.deltaTime * 5f);
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
                _navMeshAgent.SetDestination(_closestTarget.position);
                _aim.position = Vector3.Lerp(_aim.position, _closestTarget.position + Vector3.up * 1.2f, Time.deltaTime * 5f);
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
