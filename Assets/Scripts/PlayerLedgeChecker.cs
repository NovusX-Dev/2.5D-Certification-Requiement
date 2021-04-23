using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeChecker : MonoBehaviour
{
    [SerializeField] Transform _handPosition, _standPosition;
    [SerializeField] private float yOffset = 6.5f;

    private Vector3 newHandPos;

    private void Start()
    {
        newHandPos = new Vector3(_handPosition.position.x, _handPosition.position.y - yOffset, _handPosition.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge Checker"))
        {
            var player = other.GetComponentInParent<Player>();
            player.GrabLedge(newHandPos, this);
        }
    }

    public Vector3 GetStandUpPos()
    {
        return _standPosition.position;
    }

}
