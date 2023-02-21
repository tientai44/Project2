using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] private Transform brickPos;
    [SerializeField] private GameObject brickPrefab;
    private Stack<GameObject> bricks=new Stack<GameObject>();
    string currentAnimName;
    [SerializeField] Animator anim;
    protected int brickOwner = 0;
    private float brickHeight=0.15f;
    [SerializeField] LayerMask layerMask;
    protected FloorController currentFloor;
    protected bool isWin = false;
    public FloorController CurrentFloor { get => currentFloor; set => currentFloor = value; }

    private void Start()
    {
    }
    private void Update()
    {
        
        
    }

    public virtual void AddBrick()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        brickOwner++;
        //bricks.Push(Instantiate(brickPrefab,brickPos.position+brickOwner*Vector3.up*brickHeight,brickPrefab.transform.rotation,brickPos));
        bricks.Push(GameObjectPool.GetInstance().GetGameObject(brickPos.position + brickOwner * Vector3.up * brickHeight ));
        bricks.Peek().transform.rotation = transform.rotation;
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

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    public void Win()
    {
        isWin = true;
        ChangeAnim("dance");
    }
}
