using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skil : MonoBehaviour
{
    public float cooltime;
    public bool isPassive = false;  

    protected internal virtual IEnumerator PassiveSkill()
    {
        yield return new WaitForSeconds(cooltime);
    }

    protected virtual internal IEnumerator skil1()
    {
        yield return new WaitForSeconds(cooltime);
    }

    protected virtual internal IEnumerator skil2()
    {
        yield return new WaitForSeconds(cooltime);
    }
}


