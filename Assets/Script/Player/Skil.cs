using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skil : MonoBehaviour
{
    public float cooltime;
    
    public void PassiveSkill()
    {
       
    }

    protected virtual IEnumerator skil1(Collider2D collider)
    {
        yield return new WaitForSeconds(cooltime);
    }
    
    public IEnumerator skil2()
    {
        yield return new WaitForSeconds(cooltime);
    }
}
