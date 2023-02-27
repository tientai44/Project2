using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BotController : CharacterController
{
    [SerializeField] private Vector3 target;
    int brickTarget = 16;
    private IState currentState;
    Vector3 destination;
    NavMeshAgent agent;
    Color myColor;

    public IState CurrentState { get => currentState; set => currentState = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Cache agent component and destination
        myColor = GetComponent<Renderer>().material.color;
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        OnInit();
    }
    void Update()
    {
        //{
        //    ChangeAnim("run");
        //}
        //if (target == Vector3.zero || Vector3.Distance(destination, transform.position) <= 1f )
        //{
        //    SetTarget();
        //}
        if (GameController.GetInstance().IsGameOver)
        {
            StopMoving();
        }
        if (Vector3.Distance(destination, transform.position) < 1f)
        {
            ChangeState(new IdleState());
        }
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }
    public void SetTarget()
    {
        ChangeAnim("run");
        if (CurrentFloor != null && CurrentFloor.DictColorPos[myColor].Count > 0)
        {
            int index = UnityEngine.Random.Range(0, CurrentFloor.DictColorPos[myColor].Count);
            target = CurrentFloor.DictColorPos[myColor][index];
            destination = target;
            agent.destination = target;
        }
    }
    public void Fall()
    {
        brickOwner = 0;
        foreach (GameObject brick in bricks)
        {
            brick.GetComponent<BrickController>().isFalling = true;
            brick.transform.SetParent(null);
            brick.GetComponent<Renderer>().material.color = Color.gray;
            switch (Random.Range(0, 4))
            {
                case 0:
                    brick.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
                    break;
                case 1:
                    brick.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-2f, -1f), 0, Random.Range(-2f, -1f));
                    break;
                case 2:
                    brick.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(1f, 2f), 0, Random.Range(-2f, -1f));
                    break;
                case 3:
                    brick.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
                    break;
            }
            brick.GetComponent<Rigidbody>().isKinematic = false;
        }
        bricks.Clear();
      
    }
    public void SetTarget(Vector3 pos)
    {
        ChangeAnim("run");
        target = pos;
        destination = target;
        agent.destination = target;
    }
    // Update is called once per frame
    
    public override void AddBrick()
    {
        if (currentState is FallState)
        {
            return;
        }
        base.AddBrick();
        //if (brickOwner <= brickTarget)
        //{
        //    ChangeAnim("idle");
        //    ChangeState(new IdleState());
        //}

        if (brickOwner >= brickTarget)
        {
            ChangeState(null);
            if (SpawnManager.GetInstance().Floors.Count > currentFloor.IndexFloor + 1)
            {
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        SetTarget(SpawnManager.GetInstance().Floors[currentFloor.IndexFloor + 1].transform.position + new Vector3(-6.5f, 1, -5));
                        break;
                    case 1:
                        SetTarget(SpawnManager.GetInstance().Floors[currentFloor.IndexFloor + 1].transform.position + new Vector3(0f, 1, -5));
                        break;
                    case 2:
                        SetTarget(SpawnManager.GetInstance().Floors[currentFloor.IndexFloor + 1].transform.position + new Vector3(+6.5f, 1, -5));
                        break;
                }
            }
            else
            {
                SetTarget(SpawnManager.GetInstance().winPos.position);
            }
        }

    }
    public void StopMoving()
    {

        agent.destination = transform.position;
    }
    public override void RemoveBrick()
    {
        base.RemoveBrick();
        if (brickOwner <= 0)
        {
            SetTarget();
        }
    }
    public void ChangeState(IState newState)
    {
        
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
}
