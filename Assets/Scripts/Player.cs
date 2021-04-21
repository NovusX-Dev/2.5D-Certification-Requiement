using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = -1f;

    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float zHorizontal;
    private float _yVelocity;
    private bool _isJumping;

    public float Velocity => _velocity.x;
    public bool IsJumping => _isJumping;

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

        if (_controller.isGrounded)
        {
            if(_isJumping) { _isJumping = false; }

            _moveDirection.z = zHorizontal;
            _velocity = _moveDirection * _moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isJumping = true;
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            _yVelocity += _gravity;
        }

        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);

        FlipPlayer();
    }

    private void FlipPlayer()
    {
        if (zHorizontal > 0)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (zHorizontal < 0)
        {
            transform.localScale = new Vector3(3, 3, -3);
        }
    }

}//class
