using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private Transform brickPos;
    [SerializeField] private GameObject brickPrefab;
    private Stack<GameObject> bricks=new Stack<GameObject>();
    private int brickOwner = 0;
    private float brickHeight=0.05f;
    [SerializeField] LayerMask layerMask;
    private void Start()
    {
    }
    private void Update()
    {
        
        
    }

    public void AddBrick()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        brickOwner++;
        //bricks.Push(Instantiate(brickPrefab,brickPos.position+brickOwner*Vector3.up*brickHeight,brickPrefab.transform.rotation,brickPos));
        bricks.Push(GameObjectPool.GetInstance().GetGameObject(brickPos.position + brickOwner * Vector3.up * brickHeight));
        bricks.Peek().transform.SetParent(brickPos.transform);
        bricks.Peek().GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
    }
    public void RemoveBrick()
    {
        bricks.Peek().transform.SetParent(null);
        GameObjectPool.GetInstance().ReturnGameObject(bricks.Pop());
        brickOwner--;
    }
    public bool isHaveBrick()
    {
        return bricks.Count > 0;
    }
}
