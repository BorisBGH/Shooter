using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBotsCreator : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private AllyBotStateMachine _allyBotPref;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private PatrolManager _patrolManager;
    private Transform _playerCenter;



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
        var newAllyBot = Instantiate(_allyBotPref, ramdomPoint.position, ramdomPoint.rotation);
        newAllyBot.Init(_patrolManager, _playerCenter, this);
    }
}
