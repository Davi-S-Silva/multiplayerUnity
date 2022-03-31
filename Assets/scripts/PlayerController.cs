using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed, _jumpForce, _rotateForce, _verticalMove, _horizontalMove;

    [SerializeField]
    private bool _isJump;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private Animator _animController;

    [SerializeField]
    private ActionsPlayerController _actionPlayerController;


    private void Awake()
    {
        _actionPlayerController = new ActionsPlayerController();
    }

    private void OnEnable()
    {
        _actionPlayerController.Enable();
    }
    private void OnDisable()
    {
        _actionPlayerController.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = _actionPlayerController.Player.MoveHorizontal.ReadValue<float>();
        _verticalMove = _actionPlayerController.Player.MoveVertical.ReadValue<float>();



        Debug.Log("Horizontal: " + _horizontalMove);
        Debug.Log("Vertical: " + _verticalMove);

        /*_jumpForce = _jumpForce || _actionPlayerController.Player.Move.ReadValue<float>();*/
    }

    private void FixedUpdate()
    {

        

        //_rb.AddForce(new Vector3(_horizontalMove * _moveSpeed, 0, _verticalMove * _moveSpeed), ForceMode.Force);



        if (_verticalMove > 0)
        {
            _animController.applyRootMotion = true;
            _animController.SetBool("walk_with_briefcase", true);
        }else
        {
            _animController.applyRootMotion = true;
            _animController.SetBool("WalkingBackwards", true);
        }


        if (_horizontalMove > 0)
        {
            _animController.applyRootMotion = true;
            _animController.SetBool("TurnRight", true);
        }
        else
        {
            _animController.applyRootMotion = true;
            _animController.SetBool("TurnLeft", true);
        }
        if (_verticalMove == 0)
        {
            _animController.SetBool("walk_with_briefcase", false);
            _animController.SetBool("WalkingBackwards", false);
            _animController.applyRootMotion = false;
        }


        if (_horizontalMove != 0)
        {
            transform.Rotate(new Vector3(0,_rotateForce*_horizontalMove,0),Space.World);
          /*  _animController.SetBool("TurnRight", false);
            _animController.SetBool("TurnLeft", false);*/
            // _animController.applyRootMotion = false;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name.ToString());
    }
}
