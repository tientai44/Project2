using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBrickController : MonoBehaviour
{
    [SerializeField] private GameObject wallCollider;

    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player");
                GetComponent<Renderer>().material.color= collision.gameObject.GetComponent<Renderer>().material.color;
            }
        }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Player" )
        {
            if (other.gameObject.GetComponent<Renderer>().material.color != GetComponent<Renderer>().material.color ) {
                if (other.GetComponent<CharacterController>().isHaveBrick()) {
                    Debug.Log("New Player");
                    wallCollider.GetComponent<Collider>().enabled=false;
                    other.GetComponent<CharacterController>().RemoveBrick();
                    GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
                }
                else if(other.gameObject.transform.position.z<transform.position.z)
                {
                    wallCollider.GetComponent<Collider>().enabled = true;
                }
                else if (other.gameObject.transform.position.z < transform.position.z)
                {
                    wallCollider.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
