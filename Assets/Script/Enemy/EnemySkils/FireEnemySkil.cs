using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemySkil : MonoBehaviour
{
    [SerializeField]
    public Player playerhp;
    public GameObject enemy;
    private Rigidbody2D rb;
    public float speed = 4f;
    public float maxDistance = 3f;
    public float fireBallCooltime = 7f;
    private int fireBallDamage = 3;
    public SpriteRenderer spriteRenderer;

    private Vector3 startPoint;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        spriteRenderer = enemy.GetComponent<Enemy>().sp;
        Destroy(this.gameObject, 2f);
    }
    void Start()
    {
        if (spriteRenderer.flipX == false)
        {
            rb.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
        }
        else rb.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * speed * Time.deltaTime);
         if (Vector3.Distance(startPoint, transform.position) >= maxDistance)
            {
                Destroy(gameObject);
            }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Cooltime(collision));
    }
    public IEnumerator Cooltime(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().HpDown(fireBallDamage);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(fireBallCooltime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(gameObject.transform.position, gameObject.transform.position);
    }
}

