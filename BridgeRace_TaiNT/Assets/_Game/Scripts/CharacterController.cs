using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] private Transform brickPos;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Transform startPos;
    protected Stack<GameObject> bricks=new Stack<GameObject>();
    string currentAnimName;
    [SerializeField] Animator anim;
    protected int brickOwner = 0;
    private float brickHeight=0.15f;
    [SerializeField] LayerMask layerMask;
    protected FloorController currentFloor;
    protected bool isWin = false;
    public FloorController CurrentFloor { get => currentFloor; set => currentFloor = value; }
    public Stack<GameObject> Bricks { get => bricks; set => bricks = value; }

    public virtual void OnInit()
    {
        isWin = false;
        ClearBrick();
        transform.position = startPos.position;
        
    }
    public void ClearBrick()
    {
        while (brickOwner > 0)
        {
            RemoveBrick();
        }
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
    public virtual void RemoveBrick()
    {
        bricks.Peek().transform.SetParent(null);
        GameObjectPool.GetInstance().ReturnGameObject(bricks.Pop());
        brickOwner--;
    }
    public bool isHaveBrick()
    {
        return bricks.Count > 0;
    }

    public void ChangeAnim(string animName)
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
    //private void OnTriggerEnter(Collider other)
    //{
    //    PlayerController player;
    //    if (other.TryGetComponent<PlayerController>(out player))
    //    {
    //        player.Fall();
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player;
        BotController bot;
        if (collision.gameObject.TryGetComponent<PlayerController>(out player))
        {
            if (brickOwner >= player.brickOwner)
            {
                player.Fall();
            }
        }
        if (collision.gameObject.TryGetComponent<BotController>(out bot))
        {
            if (brickOwner >= bot.brickOwner)
            {
                if(bot.CurrentState is not FallState)
                    bot.ChangeState(new FallState());
            }
        }
    }
}
