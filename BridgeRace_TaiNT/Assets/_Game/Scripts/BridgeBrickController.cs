using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBrickController : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    private Collider wallCollider;
    private Transform tf ;
    private Material material;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Player");
    //        GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
    //    }
    //}
    private void Start()
    {
        wallCollider = wall.GetComponent<Collider>();
        tf=transform;
        material = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Renderer>().material.color != GetComponent<Renderer>().material.color)
            {
                if (other.GetComponent<CharacterController>().isHaveBrick())
                {
                    wallCollider.enabled = false;
                    other.GetComponent<CharacterController>().RemoveBrick();
                    material.color = other.gameObject.GetComponent<Renderer>().material.color;
                }
                else if (other.gameObject.transform.position.z < tf.position.z)
                {
                    wallCollider.enabled = true;
                }
                else if (other.gameObject.transform.position.z > tf.position.z)
                {
                    wallCollider.enabled = false;
                }
            }
            else
            {
                wallCollider.enabled = false;
            }
        }
    }
}
