using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : Skil
{
    [SerializeField]
    private Player player;

    void Start()
    {
        this.cooltime = 2f;
    }

    void Update()
    {
        
    }

    protected internal override void PassiveSkill()
    {
        Debug.Log("체력 회복");
        player.stats.Hp += 2;
    }
    protected internal override IEnumerator skil2()
    {
        player.stats.moveSpeed += 3f;
        yield return new WaitForSeconds(cooltime);
        player.stats.moveSpeed = 5f;
    }
}
