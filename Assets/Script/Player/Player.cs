using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using TMPro;
using UnityEngine;

public class playerStats
{
    [Header("Player")]
    public float maxHP = 10f;
    public float Hp;
    public float Mp;
    public float maxMp =10f;
    public float attackspeed = 1f;
    public float damag = 2f;
    public float moveSpeed = 5f;
    public bool isShoting = true;
}
#region 타입
public enum PlayerType
{
    basic,
    fire,
    electricity,
    ice,
    ninja,
    legend
}
#endregion

public class Player : MonoBehaviour
{
    public playerStats stats = new playerStats();
    
    [Header("Type")]
    public PlayerType type;
    [SerializeField]
    private BasePlayer basePlayer;

    private Rigidbody2D rigidbody2D;
    private Vector3 moveDirection = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    public Vector3 dir;

    private bool isJump;
    private int jumpPower = 5;

    [Header("Attack")]
    [SerializeField]
    private Vector2 boxSize;
    [SerializeField]
    private Transform rigntPos;
    [SerializeField]
    private Transform leftPos;

    public GameObject ball;
   

    private void Awake()
    {
        stats.Hp = stats.maxHP;
        stats.Mp = stats.maxMp;
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        type = PlayerType.basic;
        basePlayer = GetComponent<BasePlayer>();
    }
   /* private void OnDrawGizmos()
    {
        //공격 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(leftPos.position, boxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(rigntPos.position, boxSize);
    }*/
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.Q) && stats.isShoting)
        {
            StartCoroutine(skil1());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("skil2");
            if(type == PlayerType.basic)
            {
                StartCoroutine(basePlayer.skil2());
            }
        }
        attack();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isJump = true;
    }
    #region 이동
    void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            float h = Input.GetAxisRaw("Horizontal");
             dir = new Vector3(h, 0f, 0f).normalized;
            transform.Translate(dir * stats.moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }
    }

    void Jump()
    {
        if (!isJump)
            return;

        //Prevent Velocity amplification.
        rigidbody2D.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigidbody2D.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJump = false;
    }
    #endregion

    #region 공격
    private void attack()
    {
        if (Input.GetMouseButtonDown(0) && stats.attackspeed <= 0)
        {
            Debug.Log("attack");

            if (spriteRenderer.flipX == true)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(leftPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<Enemy>().TakeDamage(2);
                }
                stats.attackspeed = 0.5f;
                Debug.Log("leftAttack");
            }
            if (spriteRenderer.flipX == false)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(rigntPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<Enemy>().TakeDamage(2);

                }
                stats.attackspeed = 0.5f;
                Debug.Log("RightAttack");
            }
        }

        else
        {
            stats.attackspeed -= Time.deltaTime;

        }
    }
    public IEnumerator skil1()
    {
        stats.isShoting = false;
        Instantiate(ball,gameObject.transform.position,Quaternion.identity);
        yield return new WaitForSeconds(3f);
        stats.isShoting = true;
    }
    #endregion

    public void HpDown(int damgae)
    {

    }
}
