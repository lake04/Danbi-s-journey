using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class penetratingbead : MonoBehaviour
{
    playerStats stats = new playerStats();
    public float speed = 5f;
    public float returnSpeed = 5f;
    public float maxDistance = 5f;
    public float cooltime = 2f;
    public SpriteRenderer playrSpriteRenderer;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    GameObject player;
    private Vector3 startPoint;
    private bool returning = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playrSpriteRenderer = player.GetComponent<Player>().spriteRenderer;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        stats.damag = 3;
        cooltime = 2;
        startPoint = gameObject.transform.position;
        if (playrSpriteRenderer.flipX == true)
        {
            rb.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
            spriteRenderer.flipX = false;
        }
        else
        {
            rb.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
            spriteRenderer.flipX = true;
        }

    }

    void Update()
    {
        if (!returning)
        {
            if (Vector3.Distance(startPoint, transform.position) >= maxDistance)
            {
                returning = true;
            }
        }
        else
        {
            Vector3 directionToStart = (player.transform.position - transform.position).normalized;
            transform.Translate(directionToStart * returnSpeed * Time.deltaTime);

            if (Vector3.Distance(player.transform.position, transform.position) <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(skil1(collider));
    }
    private IEnumerator skil1(Collider2D collider)
    {
        Debug.Log("skil1");
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (!enemy.isSkilldDamaged)
            {
                enemy.TakeDamage(stats.damag);
            }
        }
        yield return new WaitForSeconds(cooltime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.transform.position, gameObject.transform.position);
    }
}