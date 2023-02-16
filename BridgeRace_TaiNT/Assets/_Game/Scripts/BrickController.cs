using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private int numFloor;//Brick thu?c floor n�o

    public int NumFloor { get => numFloor; set => numFloor = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<Renderer>().material.color==gameObject.GetComponent<Renderer>().material.color)
        {
            other.gameObject.GetComponent<CharacterController>().AddBrick();
            SpawnManager.GetInstance().StartCoroutine(SpawnManager.GetInstance().SpawnBrick(numFloor, transform.position));
            GameObjectPool.GetInstance().ReturnGameObject(gameObject);
        }
    }

}