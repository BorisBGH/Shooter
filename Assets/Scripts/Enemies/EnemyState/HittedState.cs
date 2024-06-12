using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class HittedState : EnemyState
{
    [SerializeField] private AnimationCurve _rigWeighCurve;
    private NavMeshAgent _agent;
    private Animator _animator;
    private Rig _rig;

    public void Init(EnemyStateMachine enemyStateMachine, NavMeshAgent navMeshAgent, Animator animator, Rig rig)
    {
        _enemyStateMachine = enemyStateMachine;
        _animator = animator;
        _agent = navMeshAgent;
        _rig = rig;
    }
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(HitProcess());
    }

    private IEnumerator HitProcess()
    {
        _agent.isStopped = true;
        _animator.SetTrigger("Hit");
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.6f)
        {
            _rig.weight = _rigWeighCurve.Evaluate(t);
            yield return null;
        }
        _rig.weight = 1f;
        _enemyStateMachine.StartPatrolState();
     //   _enemyStateMachine.StartFollowState();
    }

    public override void Exit()
    {
        base.Exit();
        _rig.weight = 1f;
        StopAllCoroutines();
    }

}
