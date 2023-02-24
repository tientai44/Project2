using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private int numFloor;//Brick thu?c floor nào
    public bool isFall=false;
    public int NumFloor { get => numFloor; set => numFloor = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (isFall)
        {
            return;
        }
        if (other.CompareTag("Player") && other.gameObject.GetComponent<Renderer>().material.color==gameObject.GetComponent<Renderer>().material.color)
        {
            other.gameObject.GetComponent<CharacterController>().AddBrick();
            SpawnManager.GetInstance().StartCoroutine(SpawnManager.GetInstance().SpawnBrick(numFloor, gameObject.GetComponent<Renderer>().material.color, transform.position));
            GameObjectPool.GetInstance().ReturnGameObject(gameObject);
        }
        if (other.CompareTag("Floor"))
        {
            GetComponent<Rigidbody>().isKinematic =true;
        }
    }

}
