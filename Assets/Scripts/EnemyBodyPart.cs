using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyBodyPart : MonoBehaviour
{
    [SerializeField] private float _damageMultiplier = 1f;
    private Rigidbody _rigidbody;
    private EnemyBodyPartsManager _enemyPartsManager;

    public void Init(EnemyBodyPartsManager enemyBodyPartsManager)
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyPartsManager = enemyBodyPartsManager;
    }

    public void MakePhysical()
    {
        _rigidbody.isKinematic = false;
    }

    public void MakeKinematic()
    {
        _rigidbody.isKinematic = true;
    }

    public void Hit(float damage, Vector3 direction)
    {
        _enemyPartsManager.Hit(damage * _damageMultiplier, this, direction);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}
