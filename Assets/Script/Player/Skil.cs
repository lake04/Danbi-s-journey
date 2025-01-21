using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skil : MonoBehaviour
{
    public float cooltime;

    protected internal virtual void PassiveSkill()
    {
       
    }

    protected virtual internal IEnumerator skil1(Collider2D collider)
    {
        yield return new WaitForSeconds(cooltime);
    }

    protected virtual internal IEnumerator skil2()
    {
        yield return new WaitForSeconds(cooltime);
    }
}


