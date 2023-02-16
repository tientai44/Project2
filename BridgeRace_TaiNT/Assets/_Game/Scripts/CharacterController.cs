using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform brickPos;
    [SerializeField] private GameObject brickPrefab;
    private List<GameObject> bricks=new List<GameObject>();
    private int brickOwner = 0;
    private float brickHeight=0.05f;
    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
    private void Update()
    {
        transform.Translate(Vector3.right * _joystick.Horizontal*Time.deltaTime*_moveSpeed);
        transform.Translate(Vector3.forward * _joystick.Vertical*Time.deltaTime*_moveSpeed);

        //_rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
        //if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        //{
        //    transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        //    _animator.SetBool("isRunning", true);
        //}
        //else
        //    _animator.SetBool("isRunning", false);
    }

    public void AddBrick()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        brickOwner++;
        bricks.Add(Instantiate(brickPrefab,brickPos.position+brickOwner*Vector3.up*brickHeight,brickPrefab.transform.rotation,brickPos));
        bricks[bricks.Count-1].GetComponent<Renderer>().material.color = Color.blue;

    }

}
