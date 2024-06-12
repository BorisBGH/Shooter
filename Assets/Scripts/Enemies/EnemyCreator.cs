using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private EnemyStateMachine _enemyPref;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private PatrolManager _patrolManager;
    [SerializeField] private Transform _playerCenter;


    private void Awake()
    {
        _spawnPoints.AddRange(GetComponentsInChildren<Transform>());
        _spawnPoints.Remove(transform);
    }

    private void Start()
    {
        for (int i = 0; i < _number; i++)
        {
            CreateOne();
        }
    }

    public void Respawn()
    {
        CreateOne();
    }

    private void CreateOne()
    {
        Transform ramdomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        var newEnemy = Instantiate(_enemyPref, ramdomPoint.position, ramdomPoint.rotation);
        newEnemy.Init(_patrolManager, _playerCenter, this);
    }
}
