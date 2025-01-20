using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hart : MonoBehaviour
{
    public GameObject hart;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(fascination());
        }
    }
    public IEnumerator fascination()  //∏≈»§
    {
        Instantiate(hart, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
    }
}
