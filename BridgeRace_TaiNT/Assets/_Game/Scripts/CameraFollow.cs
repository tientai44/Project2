using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset = new Vector3(0,15,-20);
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + player.transform.position;
    }
}
