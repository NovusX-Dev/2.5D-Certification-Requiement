using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] AudioClip _clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddCoins();
            SFXManager.Instance.PlaySFX(_clip, Random.Range(0.5f, 1f),1,0f);
            Destroy(gameObject);
        }
    }
}
