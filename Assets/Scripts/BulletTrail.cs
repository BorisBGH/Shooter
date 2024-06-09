using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lifeTime = 2f;

    public void Setup(Vector3 a, Vector3 b)
    {
        _lineRenderer.SetPosition(0, a);
        _lineRenderer.SetPosition(1, b);
        StartCoroutine(LifeProcess());

    }

    private IEnumerator LifeProcess()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / _lifeTime)
        {
            float alpha = 1 - t;
            _lineRenderer.material.color = new Color(1,1,1,alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}
