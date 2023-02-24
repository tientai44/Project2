using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    private Vector3 moveVector;
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
        if (isWin)
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

    public void Fall()
    {
        ChangeAnim("falltoland");
        brickOwner = 0;
        foreach(GameObject brick in bricks)
        {
            brick.transform.SetParent(null);
            brick.GetComponent<BrickController>().isFall = true;
            brick.transform.position = Vector3.MoveTowards(brick.transform.position, brick.transform.position + new Vector3(Random.Range(0,1f),0, Random.Range(0, 1f)),1f) ;
            brick.GetComponent<Rigidbody>().isKinematic = false;
        }
        bricks.Clear();
    }
}
