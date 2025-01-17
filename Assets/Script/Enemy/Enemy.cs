using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private IObjectPool<Enemy> _ManagerPool;

    private void Start()
    {
        Invoke("DestroyEnemy", 5f);

    }
    void Update()
    {
        
    }

    public void SetManagePool(IObjectPool<Enemy> pool)
    {
        _ManagerPool = pool;
    }

    public void DestroyEnemy()
    { 
        _ManagerPool.Release(this);
    }
}
