using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private GameObject _flash;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bulletMark;
    [SerializeField] private LayerMask _layerMask;
}
