using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyShooting : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.38f;
    [SerializeField] private Transform _spawn;
    [SerializeField] private GameObject _flash;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private BulletTrail _bulletTrailPref;
    [SerializeField] private float _maxShotDistance = 100f;
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
        float randomAngleX = Random.Range(-2f, 2f);
        float randomAngleY = Random.Range(-2f, 2f);
        Vector3 direction = _spawn.forward;
        Quaternion xRotation = Quaternion.AngleAxis(randomAngleX, _spawn.right);
        Quaternion yRotation = Quaternion.AngleAxis(randomAngleY, _spawn.up);
        direction = xRotation * direction;
        direction = yRotation * direction;

        float trailLength = _maxShotDistance;
        Ray ray = new Ray(_spawn.position, direction);
       
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxShotDistance, _layerMask))
        {
            if (hit.collider.GetComponent<EnemyBodyPart>() is EnemyBodyPart enemyBodyPart)
            {
                enemyBodyPart.Hit(_damage, ray.direction);
                trailLength = hit.distance;
            }
        }


        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, _maxShotDistance, _layerMask))
        //{
        //    GameObject bulletMark = Instantiate(_bulletMark, hit.point, Quaternion.LookRotation(hit.normal));
        //    bulletMark.transform.parent = hit.collider.transform;

        //    if (hit.collider.GetComponent<EnemyBodyPart>() is EnemyBodyPart enemyBodyPart)
        //    {
        //        enemyBodyPart.Hit(_damage, ray.direction);
        //    }
        //}

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
