using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderLedgeClimb : MonoBehaviour
{
    [SerializeField] Transform _standUpPoint;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge Checker"))
        {
            other.GetComponentInParent<Player>().ClimbUpFromLadderAnimationStart(this);
            StartCoroutine(DoorRoutine());
        }
    }

    public Vector3 GetStandUpPoint()
    {
        return _standUpPoint.position;
    }

    IEnumerator DoorRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.enabled = true;
    }
}
