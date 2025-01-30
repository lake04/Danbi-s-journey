using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hart : MonoBehaviour
{
    [SerializeField]
    public Penetratingbeadfascination Pf;
    public GameObject hart;
    public bool ready = true;

    void Update()
    {
        if (ready == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Ready());
            }
        }
    }
    public IEnumerator fascination()  //∏≈»§
    {
        Instantiate(hart, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(10f);
    }
    private IEnumerator Ready()
    {
        StartCoroutine(fascination());
        this.ready = false;
        yield return new WaitForSeconds(10f);
        this .ready = true;
    }
}
