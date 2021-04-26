using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _ladderClimbSpeed = 6f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = -1f;

    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float zHorizontal, yVertical;
    private float _yVelocity;
    private int _coins;
    private bool _isJumping;
    private bool _grabbedLedge;
    private bool _canClimbLadder;
    private bool _onLadder;

    private PlayerLedgeChecker _activeLedge;
    private LadderLedgeClimb _activeLadder;


    public float Velocity => _velocity.x;
    public bool IsJumping => _isJumping;
    public bool CanClimbLadder
    {
        get => _canClimbLadder;
        set => _canClimbLadder = value;
    }

    public bool GrabbedLedge
    {
        get => _grabbedLedge;
        set => _grabbedLedge = value;
    }

    public bool OnLadder
    {
        get => _onLadder;
        set => _onLadder = value;
    }



    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();    
    }

    void Start()
    {
        
    }

    void Update()
    {
        zHorizontal = Input.GetAxisRaw("Horizontal");
        yVertical = Input.GetAxisRaw("Vertical");
        
        if (_grabbedLedge) return;
        CalculateMovement();
        FlipPlayer();
    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {

            if (_isJumping)
            {
                _isJumping = false;
            }

            if (_onLadder)
            {
                _onLadder = false;
            }

            _moveDirection.z = zHorizontal;
            _velocity = _moveDirection * _moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _yVelocity = 0;
                _yVelocity = _jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.W) && _canClimbLadder)
            {
                _onLadder = true;
                _canClimbLadder = false;
            }

        }
        else
        {
            if (!OnLadder)
            {
                _yVelocity += _gravity;
            }
        }

        if (_onLadder)
        {
            _yVelocity = 0;
            _moveDirection.y = yVertical;
            _velocity = _moveDirection * _ladderClimbSpeed;
        }

        if(!_onLadder) _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void FlipPlayer()
    {
        if (_onLadder || _grabbedLedge) return;
        
        if (zHorizontal > 0)
        { 
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (zHorizontal < 0)
        {
            transform.localScale = new Vector3(3, 3, -3);
        }
        
    }

    public void GrabLedge(Vector3 handPos, PlayerLedgeChecker currentLedge)
    {
        _controller.enabled = false;
        _grabbedLedge = true;
        transform.position = handPos;
        _isJumping = false;
        _activeLedge = currentLedge;
    }

    public void ClimbUpFromLedge()
    {
        _grabbedLedge = false;
        transform.position = _activeLedge.GetStandUpPos();
        _controller.enabled = true;
    }

    public void ClimbUpFromLadderAnimationStart(LadderLedgeClimb currentLadder)
    {
        _controller.enabled = false;
        GetComponentInChildren<PlayerAnimation>().ClimbUpFromLadder();
        _activeLadder = currentLadder;
    }

    public void ClimbUpFromLadder()
    {
        _onLadder = false;
        transform.position = _activeLadder.GetStandUpPoint();
        _controller.enabled = true;
    }

    public void AddCoins()
    {
        _coins++;
        UIManager.Instance.UpdateCoins();
    }

    public int GetTotalCoins()
    {
        return _coins;
    }

}//class
