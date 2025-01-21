using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : Enemy
{
    void Start()
    {
        this.maxHp = 10;
        this.hp = maxHp;
        this.speed = 3f;
        this.attackDistance = 2f;
        this.attackTime = 2.5f;
        this.damage = 1;
    }

    void Update()
    {
        Rotate();
        float dist = Vector2.Distance(gameObject.transform.position,player.transform.position);
        if (dist <= attackDistance)
        {
            StartCoroutine(Attack(attackTime));

        }
    }

    protected override IEnumerator Attack(float attackTime)
    {
        yield return new WaitForSeconds(this.attackTime);
        Debug.Log("PlayerAttack");
        player.HpDown(this.damage);

    }
}
