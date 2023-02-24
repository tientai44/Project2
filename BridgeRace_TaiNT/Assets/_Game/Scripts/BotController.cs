using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;

public class BotController : CharacterController
{
    [SerializeField] private Vector3 target;
    int brickTarget = 16;
    private IState currentState;
    Vector3 destination;
    NavMeshAgent agent;
    Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        // Cache agent component and destination
        myColor = GetComponent<Renderer>().material.color;
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
        OnInit();
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
    public void SetTarget(Vector3 pos)
    {
        ChangeAnim("run");
        target = pos;
        destination = target;
        agent.destination = target;
    }
    // Update is called once per frame
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
    public override void AddBrick()
    {

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
        ChangeAnim("idle");
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
