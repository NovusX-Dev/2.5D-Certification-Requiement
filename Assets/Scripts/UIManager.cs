using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] Text _coinText;

    Player _player;

    private void Awake()
    {
        _instance = this;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateCoins()
    {
        _coinText.text =
            $"{_player.GetTotalCoins()} / {GameManager.Instance.GetTotalCoinsInLevel()}";
    }
}
