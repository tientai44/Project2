using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.AI;

public class BotController : CharacterController
{
    [SerializeField] private Vector3 target;
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

    }
    public void SetTarget()
    {
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
        target = pos;
        destination = target;
        agent.destination = target;

    }
    // Update is called once per frame
    void Update()
    {
        if (target == Vector3.zero || Vector3.Distance(destination, transform.position) <= 1f )
        {
            SetTarget();
        }
        // Update destination if the target moves one unit
        //if (Vector3.Distance(destination, target) > 0.1f)
        //{
        //    destination = target;
        //    agent.velocity *= _moveSpeed;
        //    agent.destination = destination;
        //}

    }
    public override void AddBrick()
    {
        base.AddBrick();
        if (brickOwner >= 15)
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
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
