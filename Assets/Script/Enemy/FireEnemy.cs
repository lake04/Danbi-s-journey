using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : Enemy
{
    public GameObject enemyFireBall;

    private float fireBallDis = 3;
    private float coolTime = 7;
    public bool readyFireball = true;

 
    void Start()
    {
        this.maxHp = 14;
        this.hp = maxHp;
        this.speed = 4f;
        this.attackDistance = 1.5f;
        this.attackTime = 4f;
        this.damage = 2;
    }

    void Update()
    {
        Move();
        float dist = Vector2.Distance(gameObject.transform.position, player.transform.position);
        if (dist <= attackDistance)
        {
            StartCoroutine(Attack(attackTime));
        }
        else if (dist <= fireBallDis)
        {
            if (readyFireball == true)
            {
                StartCoroutine(Skil());
            }
        }
    }

    protected override IEnumerator Attack(float attackTime)
    {
        yield return new WaitForSeconds(this.attackTime);
        Debug.Log("PlayerAttack");
        player.HpDown(this.damage);
    }

    public IEnumerator Skil()
    {
        Instantiate(enemyFireBall, gameObject.transform.position, Quaternion.identity);
        readyFireball = false;
        yield return new WaitForSeconds(coolTime);
        readyFireball = true;
    }
}
