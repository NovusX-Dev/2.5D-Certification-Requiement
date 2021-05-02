using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform _pointA, _pointB;
    [SerializeField] private float _speed = 2f;

    private Vector3 _target;
    private float _initialSpeed;
    private WaitForSeconds _waitTime;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(1.5f);
    }

    void Start()
    {
        _initialSpeed = _speed;
        CheckTarget();
    }

    private void Update()
    {
        CheckTarget();    
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target,
            _speed * Time.deltaTime);
    }

    private void CheckTarget()
    {
        if (transform.position == _pointA.position)
        {
            _target = _pointB.position;
            StartCoroutine(StopAtEndRoutine());
        }
        else if (transform.position == _pointB.position)
        {
            _target = _pointA.position;
            StartCoroutine(StopAtEndRoutine());
        }
    }

    IEnumerator StopAtEndRoutine()
    {
        _speed = 0;
        yield return _waitTime;
        _speed = _initialSpeed;
    }

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
    #endregion

}//class
