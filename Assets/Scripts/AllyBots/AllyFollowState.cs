using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AllyFollowState : AllyBotState
{
    [SerializeField] private Transform _aim;
    [SerializeField] private AllyShooting _allyShooting;
    [SerializeField] private float _viewingDistance = 20f;
    [SerializeField] private float _viewingAngle = 50f;
    [SerializeField] private LayerMask _wallMask;
    private NavMeshAgent _navMeshAgent;
    //  private Transform _playerCenter;
    private Animator _animator;
    private Coroutine _coroutine;
    private float _lostTargetTimer;

    List<EnemyStateMachine> allEnemies = new List<EnemyStateMachine>();
    private List<EnemyStateMachine> foundEnemies = new List<EnemyStateMachine>();
    private EnemyStateMachine closestEnemy;

    public void Init(AllyBotStateMachine allyBotStateMachine, NavMeshAgent navMeshAgent, Transform playerCenter, Animator animator)
    {
        _allyBotStateMachine = allyBotStateMachine;
        _navMeshAgent = navMeshAgent;
        //  _playerCenter = playerCenter;
        _animator = animator;
    }
    public override void Enter()
    {
        base.Enter();
        _lostTargetTimer = 0;
        _coroutine = StartCoroutine(Behaviour());
        allEnemies = FindObjectsOfType<EnemyStateMachine>().ToList();
    }

    public override void Process()
    {
        base.Process();
        _lostTargetTimer += Time.deltaTime;

        allEnemies = FindObjectsOfType<EnemyStateMachine>().ToList();
        foundEnemies = SearchUtility.SearchEnemiesInSector(transform.position, transform.forward, allEnemies, _viewingAngle, _viewingDistance, _wallMask);

        if (foundEnemies != null && foundEnemies.Count > 0)
        {
            EnemyStateMachine closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                _lostTargetTimer = 0;
            }
            if (_lostTargetTimer > 4f)
            {
                _allyBotStateMachine.StartPatrolState();
            }
        }
        else
        {
            _allyBotStateMachine.StartPatrolState();
        }


    }

    private EnemyStateMachine FindClosestEnemy()
    {



        if (foundEnemies != null && foundEnemies.Count > 0)
        {
            closestEnemy = foundEnemies.OrderBy(target => (Vector3.Distance(transform.position, target.transform.position))).First();
           

        }
        return closestEnemy;
    }

    private IEnumerator Behaviour()
    {
        EnemyStateMachine closestEnemy;
        allEnemies = FindObjectsOfType<EnemyStateMachine>().ToList();
        foundEnemies = SearchUtility.SearchEnemiesInSector(transform.position, transform.forward, allEnemies, _viewingAngle, _viewingDistance, _wallMask);
        while (true && foundEnemies != null && foundEnemies.Count > 0)
        {
            closestEnemy = FindClosestEnemy();
            _navMeshAgent.isStopped = true;
            _animator.SetBool("Walk", false);
            _animator.SetBool("Crouch", false);
            float timer = Random.Range(.5f, 1.5f);
            while (timer > 0 && closestEnemy)
            {
                timer -= Time.deltaTime;
                _aim.position = Vector3.Lerp(_aim.position, closestEnemy.transform.position + Vector3.up * 1.2f, Time.deltaTime * 5f);
                _allyShooting.Process();
                yield return null;
            }
            _navMeshAgent.isStopped = false;
            _animator.SetBool("Walk", true);
            _animator.SetBool("Crouch", true);

            timer = Random.Range(1f, 2f);
            while (timer > 0 && closestEnemy)
            {
                timer -= Time.deltaTime;
                _navMeshAgent.SetDestination(closestEnemy.transform.position);
                _aim.position = Vector3.Lerp(_aim.position, closestEnemy.transform.position, Time.deltaTime * 5f);
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
