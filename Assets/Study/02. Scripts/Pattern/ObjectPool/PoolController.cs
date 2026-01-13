using System;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public StudyObjectPool pool;
    public Transform shootPoint;
    public UnityPoolManager poolManager;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = poolManager.pool.Get();
            //GameObject bullet = pool.DequeueObject();

            bullet.transform.position = shootPoint.position;
        }
    }
}
