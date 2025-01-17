using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private IObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(CreateEnemy,OnGetEnemy,OnRelwaseEnemy,OnDestroyEnemy,maxSize:5);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K ))
        {
            var enemy = _pool.Get();
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab).GetComponent<Enemy>();
        enemy.SetManagePool(_pool);
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnRelwaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
