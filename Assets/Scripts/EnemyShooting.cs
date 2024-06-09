using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.38f;
    [SerializeField] private Transform _spawn;
    [SerializeField] private GameObject _flash;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private BulletTrail _bulletTrailPref;
    private float _timer;

    private void Start()
    {
        _flash.SetActive(false);
    }

    public void Process()
    {
        _timer += Time.deltaTime;
        if (_timer > _fireRate)
        {
            _timer = 0;
            Shot();
        }
    }

    private void Shot()
    {
        float randomAngleX = Random.Range(-5f, 5f);
        float randomAngleY = Random.Range(-5f, 5f);
        Vector3 direction = _spawn.forward;
        Quaternion xRotation = Quaternion.AngleAxis(randomAngleX, _spawn.right);
        Quaternion yRotation = Quaternion.AngleAxis(randomAngleY, _spawn.up);
        direction = xRotation * direction;
        direction = yRotation * direction;

        Ray ray = new Ray(_spawn.position, direction);
        float maxDistance = 100f;
        float trailLength = maxDistance;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, _layerMask))
        {
            if (hit.collider.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
            {
                playerHealth.TakeDamage(_damage);
                trailLength = hit.distance;
            }
        }
        BulletTrail bulletTrail = Instantiate(_bulletTrailPref, Vector3.zero, Quaternion.identity);
        bulletTrail.Setup(_spawn.position, _spawn.position + direction * trailLength);

        StartCoroutine(ShotProcess());
    }

    private IEnumerator ShotProcess()
    {
        _flash.SetActive(true);
        yield return new WaitForSeconds(.05f);
        _flash.SetActive(false);
    }
}
