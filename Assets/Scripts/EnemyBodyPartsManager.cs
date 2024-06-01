using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPartsManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyHealth _enemyHealth;
    private EnemyBodyPart[] _bodyParts;
    

    private void Awake()
    {
        _bodyParts = GetComponentsInChildren<EnemyBodyPart>();
        foreach (EnemyBodyPart part in _bodyParts)
        {
            part.Init(this);
        }

        MakeKinematic();
    }

    private void MakeKinematic()
    {
        foreach (var part in _bodyParts)
        {
            part.MakeKinematic();
        }
    }

    [ContextMenu("MakePhysical")]
    public void MakePhysical(EnemyBodyPart hittenPart, Vector3 direction)
    {
        foreach (var part in _bodyParts)
        {
            part.MakePhysical();
        }
        hittenPart.SetVelocity(direction * 40f);
        _animator.enabled = false;
    }

    public void Hit(float damage, EnemyBodyPart hittenPart, Vector3 direction)
    {
        _enemyHealth.ApplyDamage(damage,hittenPart, direction);
    }

}
