using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    private Vector3 moveVector;
    private bool isFall = false;
    private Vector3 fallVector;

    public bool IsFall { get => isFall; set => isFall = value; }

    private void Start()
    {
        OnInit();

    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public void Move(Joystick _joystick)
    {
        if (isWin||isFall)
        {
            return;
        }
        moveVector = Vector3.zero;
        moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            ChangeAnim("run");
        }
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            ChangeAnim("idle");
        }
        transform.position = Vector3.Lerp(transform.position, transform.position + moveVector,1f);
    }
    public override void AddBrick()
    {
        base.AddBrick();

    }
    public void Fall()
    {
        if (isFall)
        {
            return;
        }
        _rigidbody.velocity = Vector3.zero; _rigidbody.angularVelocity=Vector3.zero;
        ChangeAnim("falltoland");
        isFall = true;
        brickOwner = 0;
        foreach(GameObject brick in bricks)
        {
            brick.GetComponent<BrickController>().isFalling = true;
            brick.transform.SetParent(null);
            brick.GetComponent<Renderer>().material.color = Color.gray;
            switch(Random.Range(0, 4))
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
            //switch (Random.Range(0, 4))
            //{
            //    case 0:
            //        fallVector = new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
            //        break;
            //    case 1:
            //        fallVector = new Vector3(Random.Range(-2f,-1f),0, Random.Range(-2f, -1f));
            //        break;
            //    case 2:
            //        fallVector = new Vector3(Random.Range(1f, 2f), 0, Random.Range(-2f, -1f));
            //        break;
            //    case 3:
            //        fallVector = new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
            //        break;
            //}
            //brick.transform.position = Vector3.Lerp(brick.transform.position, brick.transform.position + fallVector,1f) ;
            brick.GetComponent<Rigidbody>().isKinematic = false;
        }
        bricks.Clear();
        Invoke(nameof(StandUp), 2f);
    }

    void StandUp()
    {
        isFall = false;
    }
}
