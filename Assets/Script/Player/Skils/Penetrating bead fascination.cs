using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetratingbeadfascination : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float speed = 10f;
    public float maxDistance = 5f;
    public float fascinationCooltime = 10f;
    public SpriteRenderer spriteRenderer;

    private Vector3 startPoint;
    private bool returning = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        spriteRenderer = player.GetComponent<Player>().spriteRenderer;
        Destroy(this.gameObject, 2f);
        startPoint = transform.position;
    }
    void Start()
    {
        if (spriteRenderer.flipX == true)
        {
            rb.AddForce(Vector3.right * speed, ForceMode2D.Impulse);
        }
        else rb.AddForce(Vector3.left * speed, ForceMode2D.Impulse);
    }

    void Update()
    {
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("매혹");
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(fascinationCooltime);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(gameObject.transform.position, gameObject.transform.position);
    }
}
