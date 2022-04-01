using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed, _jumpForce, _rotateForce, _verticalMove, _horizontalMove, _shiftLeft;

    [SerializeField]
    private bool _isJump, _isRunning;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private Animator _animController;

    [SerializeField]
    private ActionsPlayerController _actionPlayerController;

    [SerializeField]
    private AudioSource _walkAudio;


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
        _walkAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = _actionPlayerController.Player.MoveHorizontal.ReadValue<float>();
        _verticalMove = _actionPlayerController.Player.MoveVertical.ReadValue<float>();
        _isJump = _actionPlayerController.Player.jump.triggered;
        _isRunning = _actionPlayerController.Player.run.triggered;
        _shiftLeft = _actionPlayerController.Player.ShiftLeft.ReadValue<float>();

        Debug.Log("Horizontal: " + _horizontalMove);
        Debug.Log("Vertical: " + _verticalMove);

        /*_jumpForce = _jumpForce || _actionPlayerController.Player.Move.ReadValue<float>();*/
    }

    private void FixedUpdate()
    {

        if (_shiftLeft!=0 && _verticalMove!=0)
        {
            _animController.SetBool("run", true);
        }
        else
        {
            _animController.SetBool("run", false);
        }


        if (_isRunning)
        {
           
        }

        if (_isJump)
        {
            _animController.SetBool("jump", true);
        }
        else
        {
            _animController.SetBool("jump", false);
        }

        if (_verticalMove > 0)
        {
            _animController.SetBool("walk_with_briefcase", true);
            _walkAudio.Play();
        }
        else 
        {
            _animController.SetBool("WalkingBackwards", true);
            _walkAudio.Play();
        }

        if (_verticalMove == 0)
        {
            _animController.SetBool("walk_with_briefcase", false);
            _animController.SetBool("WalkingBackwards", false);
            _walkAudio.Pause();
        }


        if (_horizontalMove != 0)
        {
            transform.Rotate(new Vector3(0,_rotateForce*_horizontalMove,0),Space.World);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name.ToString());
    }
}
