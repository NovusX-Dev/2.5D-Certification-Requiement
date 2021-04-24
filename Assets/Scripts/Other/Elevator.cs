using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField] private float _speed = 5f;

    private float step;
    private bool _canSwitch;
    private bool _elevatorCalled = false;
    private bool _playerInElevator = false;
    private Vector3 _currentTarget;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        TargetCheck();
    }

    void Update()
    {
        TargetCheck();

        if (_playerInElevator)
        {
            if (Input.GetKeyDown(KeyCode.E) && _canSwitch)
            {
                _elevatorCalled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        step = _speed * Time.fixedDeltaTime;

        if (_elevatorCalled)
        {
            _animator.SetTrigger("close");
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget,
                step);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            _playerInElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            _playerInElevator = false;
        }
    }

    private void TargetCheck()
    {
        var distanceToA = Vector3.Distance(transform.position, _pointA.position);
        var distanceToB = Vector3.Distance(transform.position, _pointB.position);

        if (distanceToA < 0.1f)
        {
            _animator.SetTrigger("open");
            _currentTarget = _pointB.position;
            _canSwitch = true;
            _elevatorCalled = false;
        }
        else if (distanceToB < 0.1f) 
        {
            _animator.SetTrigger("open");
            _currentTarget = _pointA.position;
            _canSwitch = true;
            _elevatorCalled = false;
        }
        else
        {
            _canSwitch = false;
        }
    }

}//class
