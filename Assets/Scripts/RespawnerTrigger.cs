using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnerTrigger : MonoBehaviour
{
    [SerializeField] Transform _respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().RespawnPlayer(_respawnPosition);
        }
    }
}
