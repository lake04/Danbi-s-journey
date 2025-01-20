using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class penetratingbead : Skil
{
    playerStats stats = new playerStats();
    public float speed = 10f;
    public float returnSpeed = 15f;
    public float maxDistance = 5f;

    private Vector3 startPoint;
    private bool returning = false;

    void Start()
    {
        stats.damag = 3;
        this.cooltime = 1;
        Destroy(this.gameObject,2f);
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!returning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Vector3.Distance(startPoint, transform.position) >= maxDistance)
            {
                returning = true;
            }
        }
        else
        {
            Vector3 directionToStart = (startPoint - transform.position).normalized;
            transform.Translate(directionToStart * returnSpeed * Time.deltaTime);

            if (Vector3.Distance(startPoint, transform.position) <= 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //여기 버그 있음
        
      StartCoroutine(skil1(collider));
    }
    protected override IEnumerator skil1(Collider2D collider)
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
