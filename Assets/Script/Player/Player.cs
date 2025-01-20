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
}
public enum PlayerType
{
    basic, //기본
    fire, //불
    electricity, //전기
    ice, //얼음
    ninja, //닌자
    legend //롤
}


public class Player : MonoBehaviour
{
    playerStats stats = new playerStats();
  
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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(skil1());
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
        Instantiate(ball,gameObject.transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }
    #endregion

    public void HpDown(int damgae)
    {

    }
}
