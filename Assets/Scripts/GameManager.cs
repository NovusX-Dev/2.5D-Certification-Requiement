using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private GameObject[] _totalCoinsInLevel;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _totalCoinsInLevel = GameObject.FindGameObjectsWithTag("Coin");
        UIManager.Instance.UpdateCoins();
    }

    void Update()
    {
        
    }

    public int GetTotalCoinsInLevel()
    {
        return _totalCoinsInLevel.Length;
    }
}
