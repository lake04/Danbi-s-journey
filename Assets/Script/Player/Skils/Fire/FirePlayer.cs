using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FirePlayer : Skil
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject fireBlanket;
    private bool isFireSp = false;
    [SerializeField]
    private GameObject fireBall;
    Coroutine coroutine;

    void Start()
    {
        this.cooltime = 1f;
        this.isPassive = true;
        if (player == null)
        {
            player = FindObjectOfType<Player>();
          
        }
    }
   
    private void FixedUpdate()
    {
        if (isFireSp == true)
            Instantiate(fireBlanket, new Vector2(player.transform.position.x, player.transform.position.y-1), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Enemy 객체의 코루틴 호출
            enemy.StartCoroutine(enemy.FireDamage());
        }

    }
    protected internal override IEnumerator skil1()
    {
        player.stats.isShoting = false;
        Instantiate(fireBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cooltime);
        player.stats.isShoting = true;
    }

    protected internal override IEnumerator skil2()
    {
        player.stats.isSkil2 = false;
        isFireSp = true;
        yield return new WaitForSeconds(cooltime);
        player.stats.isSkil2 = true;
        isFireSp = false;
    }
}
