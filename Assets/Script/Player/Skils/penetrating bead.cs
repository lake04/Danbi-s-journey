using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class penetratingbead : Skil
{
    playerStats stats = new playerStats();
    public float speed = 5f;
    public float returnSpeed = 5f;
    public float maxDistance = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    GameObject player;
    private Vector3 startPoint;
    private bool returning = false;
    private bool isFirst = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<Player>().spriteRenderer;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        stats.damag = 3;
        this.cooltime = 2;
        //startPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,0).normalized;
        startPoint = gameObject.transform.position;
        if (spriteRenderer.flipX == true)
        {
            rb.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
        }
        else rb.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!returning)
        {
            /*  transform.Translate(Vector3.right * speed * Time.deltaTime);*/
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
    protected internal override IEnumerator skil1(Collider2D collider)
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
