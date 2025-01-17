using System.Collections;
using System.Collections.Generic;
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
public class Player : MonoBehaviour
{
    playerStats stats = new playerStats();
  
    private Rigidbody2D rigidbody2D;
    private Vector3 moveDirection = Vector3.zero;
    private SpriteRenderer spriteRenderer;

    private bool isJump;
    private int jumpPower = 5;

    private void Awake()
    {
        stats.Hp = stats.maxHP;
        stats.Mp = stats.maxMp;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
       
    }
    void FixedUpdate()
    {
        Move();
    }
    #region 이동
    void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            Vector3 dir = new Vector3(h, 0f, 0f).normalized;
            transform.Translate(dir * stats.moveSpeed * Time.deltaTime);
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
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
        /*if (Input.GetKeyDown(KeyCode.V) && stats.attackspeed <= 0)
        {
           

            if (spriteRenderer.flipX == true)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(leftPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<EnemyHP>().TakeDamage(2);
                }
                stats.attackspeed = 0.5f;
                Debug.Log("leftAttack");
            }
            if (spriteRenderer.flipX == false)
            {
                Collider2D[] collider2D = Physics2D.OverlapBoxAll(rigntPos.position, boxSize, 0);
                foreach (Collider2D collider in collider2D)
                {
                    if (collider.tag == "Enemy") collider.GetComponent<EnemyHP>().TakeDamage(2);

                }
                stats.attackspeed = 0.5f;
                Debug.Log("RightAttack");
            }
        }

        else
        {
           stats.attackspeed -= Time.deltaTime;

        }*/
    }
#endregion

    public void HpDown(int damgae)
    {

    }

    
}
