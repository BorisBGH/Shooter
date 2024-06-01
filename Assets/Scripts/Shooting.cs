using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private float _damage = 20f;
    [SerializeField] private GameObject _flash;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _bulletMark;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxShotDistance = 1000f;
    private float _timer;
    private Coroutine _shotCoroutine;


    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _fireRate && Input.GetMouseButton(0))
        {
            _timer = 0;
            Shot();
        }
    }

    private void Shot()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxShotDistance, _layerMask))
        {
            GameObject bulletMark = Instantiate(_bulletMark, hit.point, Quaternion.LookRotation(hit.normal));
            bulletMark.transform.parent = hit.collider.transform;

            if(hit.collider.GetComponent<EnemyBodyPart>() is EnemyBodyPart enemyBodyPart)
            {
                enemyBodyPart.Hit(_damage, ray.direction);
            }
        }

        if (_shotCoroutine != null)
        {
            StopCoroutine(_shotCoroutine);
        }
        _shotCoroutine = StartCoroutine(ShotProcess());
    }

    private IEnumerator ShotProcess()
    {
        _animator.SetTrigger("Shot");
        _flash.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360f));
        _flash.transform.localScale = Vector3.one * Random.Range(0.9f, 1.1f);
        _flash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        _flash.SetActive(false);
    }
}
