using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Penetratingbeadfascination : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float returnSpeed = 15f;
    public float maxDistance = 5f;
    public float fascinationCooltime = 10f;

    private Vector3 startPoint;
    private bool returning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Cooltime());
    }
    public IEnumerator Cooltime()  //��Ȥ
    {
        if (!returning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Vector3.Distance(startPoint, transform.position) >= maxDistance)
            {
                Destroy(gameObject);
            }
        }
        yield return new WaitForSeconds(fascinationCooltime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("매혹");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(gameObject.transform.position, gameObject.transform.position);
    }
}
