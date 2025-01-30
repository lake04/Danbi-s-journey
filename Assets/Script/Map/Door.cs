using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    public Player player;
    [SerializeField]
    public CameraMove mainCamera;
    public void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            player.transform.position = new Vector3(-3, 17, 0);
            mainCamera.limitMinY = 13; mainCamera.limitMaxY = 23.5f;
        }
    }
}
