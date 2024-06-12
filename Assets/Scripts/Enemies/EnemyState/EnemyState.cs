using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    protected EnemyStateMachine _enemyStateMachine;
    public virtual void Enter()
    { }

    public virtual void Enter(Transform target)
    { }

    public virtual void Process()
    { }

    public virtual void Exit()
    { }

}
