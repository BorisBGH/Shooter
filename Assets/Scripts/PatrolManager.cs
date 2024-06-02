using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolManager : MonoBehaviour
{
    [SerializeField] EnemyTargetPoint[] _points;

   public EnemyTargetPoint GetRandomPoint()
    {
        int index = Random.Range(0, _points.Length);
        return _points[index];
    }
}
