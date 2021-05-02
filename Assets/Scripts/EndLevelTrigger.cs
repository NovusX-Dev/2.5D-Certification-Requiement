using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireworks;
    [SerializeField] AudioClip _noWinClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.AllCoinsCollected())
            {
                _fireworks.gameObject.SetActive(true);
                GameManager.Instance.WinLevel();
            }
            else
            {
                SFXManager.Instance.PlaySFX(_noWinClip, 1, 2,0);
            }
        }    
    }
}
