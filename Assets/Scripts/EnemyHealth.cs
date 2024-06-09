using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private EnemyBodyPartsManager _bodyPartsManager;
    [SerializeField] EnemyStateMachine _enemyStateMachine;
    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void ApplyDamage(float value, EnemyBodyPart hittenPart, Vector3 direction)
    {
        _health -= value;
        if (_health <= 0 )
        {
            Die(hittenPart, direction);
            _enemyStateMachine.StartDieState();
        }
        else
        {
            _enemyStateMachine.StartHittedState();
        }

    }

    private void Die(EnemyBodyPart hittenPart, Vector3 direction)
    {
        _bodyPartsManager.MakePhysical(hittenPart, direction);
    }
}
