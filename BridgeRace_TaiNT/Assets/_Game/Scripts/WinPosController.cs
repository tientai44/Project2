using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WinPosController : MonoBehaviour
{
    
 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Win Trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.GetInstance().GameOver();
            
            Debug.Log("Win");
            other.gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            other.gameObject.GetComponent<CharacterController>().Win();
        }
    }
}
