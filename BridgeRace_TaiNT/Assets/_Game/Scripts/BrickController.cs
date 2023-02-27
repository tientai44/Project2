using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    private int numFloor;//Brick thu?c floor nào
    public bool isFalled=false;
    public bool isFalling = false;
    public int NumFloor { get => numFloor; set => numFloor = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (GameController.GetInstance().IsGameOver)
        {
            return;
        }
        if (other.CompareTag("Player") )
        {
            if (isFalling)
            {
                return;
            }
            if (Color.gray == gameObject.GetComponent<Renderer>().material.color || other.gameObject.GetComponent<Renderer>().material.color == gameObject.GetComponent<Renderer>().material.color)
            {
                other.gameObject.GetComponent<CharacterController>().AddBrick();
                if (!isFalled)
                {
                    SpawnManager.GetInstance().StartCoroutine(SpawnManager.GetInstance().SpawnBrick(numFloor, gameObject.GetComponent<Renderer>().material.color, transform.position));
                }
                GameObjectPool.GetInstance().ReturnGameObject(gameObject);
            }

        }
        if (other.CompareTag("Floor"))
        {
            if (isFalling)
            {
                isFalled = true;
                isFalling = false;
            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic =true;
        }
    }

}
