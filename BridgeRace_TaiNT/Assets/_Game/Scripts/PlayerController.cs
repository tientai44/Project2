using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    [SerializeField] private FixedJoystick _joystick;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _joystick.Horizontal * Time.deltaTime * _moveSpeed);
        transform.Translate(Vector3.forward * _joystick.Vertical * Time.deltaTime * _moveSpeed);
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
