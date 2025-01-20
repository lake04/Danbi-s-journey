using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{
    private IObjectPool<Enemy> _ManagerPool;
    [SerializeField]
    private BasePlayer _Player;

    #region enemy¡§∫∏
    public float maxHp = 10;
    public float hp;
    public bool isSkilldDamaged;
    
    public float distance;
    public LayerMask isLayer;
    public float speed;
    private int rotateSpeed;
    public Player player;
    Transform playerTransform;
    public Rigidbody2D rigidbody2D;

    public float attackDistance;
    public float attackTime;
    #endregion


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = FindAnyObjectByType<Player>();
        _Player = FindAnyObjectByType<BasePlayer>();
    }

    private void Start()
    {
        hp = maxHp;
    }
    private void Update()
    {
        Rotate();
    }
    public void SetManagePool(IObjectPool<Enemy> pool)
    {
        _ManagerPool = pool;
    }

    public void DestroyEnemy()
    {
        if (player.type == PlayerType.basic)
        {
            _Player.PassiveSkill();
            _ManagerPool.Release(this);
        }
        else _ManagerPool.Release(this);

    }

    public void TakeDamage(float damage)
    {
        if (hp >0)
        {
            hp -= damage;
            Debug.Log("TakeDamage");
            StartCoroutine(SkillDamagedRoutine(1f));
        }
        if(hp <=0)
        {

            DestroyEnemy();
            
        }
        
    }

    public IEnumerator SkillDamagedRoutine(float skillTime)
    {
        this.isSkilldDamaged = true;
        yield return new WaitForSeconds(skillTime);
        this.isSkilldDamaged = false;
    }

    public void Rotate()
    {
        Vector2 direction = new Vector2(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, isLayer);
        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);

        transform.rotation = rotation;

        Vector3 dir = direction;
        transform.position += (-dir.normalized * speed * Time.deltaTime);
    }

    protected virtual IEnumerator Attack(float attackTime)
    {
        yield return new WaitForSeconds(attackTime);
    }
}
