using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    [SerializeField] private FixedJoystick _joystick;
    private Vector3 moveVector;
    void Start()
    {
        
    }
    private void Move()
    {
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
    // Update is called once per frame
    void Update()
    {
        if (isWin)
        {
            return;
        }
        Move();
        //    if(_joystick.Horizontal !=0 || _joystick.Vertical != 0)
        //    {
        //        ChangeAnim("run");
        //    }
        //    else
        //    {
        //        ChangeAnim("idle");
        //    }
        //    transform.transform.rotation = Quaternion.LookRotation(new Vector3(_joystick.Horizontal,0,_joystick.Vertical));

        //    transform.Translate(Vector3.right * _joystick.Horizontal * Time.deltaTime * _moveSpeed);
        //    transform.Translate(Vector3.forward * _joystick.Vertical  * Time.deltaTime * _moveSpeed);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position+ Vector3.forward*0.5f, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //    Debug.Log("Did Hit");
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position + Vector3.forward *0.5f, transform.TransformDirection(Vector3.down) * 1000, Color.red);
        //    Debug.Log("Did not Hit");
        //}
        //_rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
        //if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        //{
        //    transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        //    _animator.SetBool("isRunning", true);
        //}
        //else
        //    _animator.SetBool("isRunning", false);
    }
}
