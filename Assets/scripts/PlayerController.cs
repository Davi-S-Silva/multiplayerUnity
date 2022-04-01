using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed, _jumpForce, _rotateForce, _verticalMove, _horizontalMove, _shiftLeft;

    [SerializeField]
    private bool _isJump, _isRunning, _isWalk, _withGun;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private Animator _animController;

    [SerializeField]
    private ActionsPlayerController _actionPlayerController;

    [SerializeField]
    private AudioSource _walkAudio;

    [SerializeField]
    private int _whichWeapon;

    [SerializeField]
    private string _animNameFoward, _animNameBackward;

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
        _withGun = false;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = _actionPlayerController.Player.MoveHorizontal.ReadValue<float>();
        _verticalMove = _actionPlayerController.Player.MoveVertical.ReadValue<float>();
        _isJump = _actionPlayerController.Player.jump.triggered;
        /*_isRunning = _actionPlayerController.Player.run.triggered;*/
        _shiftLeft = _actionPlayerController.Player.ShiftLeft.ReadValue<float>();
       

        Debug.Log("Horizontal: " + _horizontalMove);
        Debug.Log("Vertical: " + _verticalMove);

        //clicando para trocar de com arma para sem arma ou vice e versa
        if (_withGun == true && _actionPlayerController.Player.arma.triggered)
        {
            _withGun = false;
        }
        else
        {
            _withGun = _withGun || _actionPlayerController.Player.arma.triggered;
        }
       

        /*_jumpForce = _jumpForce || _actionPlayerController.Player.Move.ReadValue<float>();*/
    }

    private void FixedUpdate()
    {
        if (_withGun)
        {
            switch (_whichWeapon)
            {
                case 1:
                    {
                        _animNameFoward = "Pistol Walk";
                        _animNameBackward = "Pistol Walk Backward";
                        break;
                    }
                case 2:
                    {
                        _animNameFoward = "Fuzil Walk";
                        _animNameBackward = "";
                        break;
                    }
            }

        }
        else
        {
            _animNameFoward = "walk_with_briefcase";
            _animNameBackward = "WalkingBackwards";
        }
        if (_shiftLeft!=0 && _verticalMove!=0)
        {
            _animController.SetBool("run", true);
        }
        else
        {
            _animController.SetBool("run", false);
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
            _isWalk = true;
           /* _animController.SetBool("walk_with_briefcase", true);*/
            _animController.SetBool(_animNameFoward, true);

            Debug.Log("tocando frente = " + _verticalMove);
        }
        else if (_verticalMove < 0)
        {
            _isWalk = true;
            /*_animController.SetBool("WalkingBackwards", true);*/
            _animController.SetBool(_animNameBackward, true);
            //_walkAudio.Play();
            Debug.Log("tocando atras = " + _verticalMove);
        }     

        if (_verticalMove == 0)
        {
           /* _animController.SetBool("walk_with_briefcase", false);
            _animController.SetBool("WalkingBackwards", false);*/
            _animController.SetBool(_animNameFoward, false);
            _animController.SetBool(_animNameBackward, false);
            _isWalk = false;
            Debug.Log("sem musica");
        }

        // audio //
        if (_isWalk==false)//ta invertido. tem que descobrir o porque ainda...
        {
            tocarPassos();
            /*_walkAudio.PlayOneShot(_walkAudio.clip);*/
        }
        else
        {
           // _walkAudio.Pause();
        }


        if (_horizontalMove != 0)
        {
            transform.Rotate(new Vector3(0,_rotateForce*_horizontalMove,0),Space.World);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name.ToString());
        if (collision.gameObject.name.ToString() == "CubeFim")
        {
            SceneManager.LoadScene("Inicial");
        }
    }
    private void tocarPassos()
    {
        _walkAudio.Play();
    }
}
