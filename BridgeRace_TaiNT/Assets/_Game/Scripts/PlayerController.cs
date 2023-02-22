using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    private Vector3 moveVector;
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
}
