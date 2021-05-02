using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] AudioClip _countDownStart;
    [SerializeField] AudioClip _winClip;

    private GameObject[] _totalCoinsInLevel;
    private bool _allCoinsCollected = false;

    Player _player;

    private void Awake()
    {
        _instance = this;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        _totalCoinsInLevel = GameObject.FindGameObjectsWithTag("Coin");
        UIManager.Instance.UpdateCoins();
        StartCoroutine(StartGameRoutine());
    }

    void Update()
    {
        #if(UNITY_EDITOR)
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All prefs deleted");
        }
        #endif
    }

    public int GetTotalCoinsInLevel()
    {
        return _totalCoinsInLevel.Length;
    }

    public bool AllCoinsCollected()
    {
       return _allCoinsCollected = _player.GetTotalCoins() == GetTotalCoinsInLevel();
    }

    public void WinLevel()
    {
        SFXManager.Instance.StopMusic();
        SFXManager.Instance.PlaySFX(_winClip, 1,2,0f);
        _player.ChangeStateToFreeze();
        _player.GetComponentInChildren<PlayerAnimation>().WinDance();
        UIManager.Instance.EndGameUI();
    }

    IEnumerator StartGameRoutine()
    {
        SFXManager.Instance.StopMusic();
        _player.ChangeStateToFreeze();
        SFXManager.Instance.PlaySFX(_countDownStart, 1, 2,0f);

        yield return new WaitForSeconds(3f);

        _player.ChangeStateToNormal();
        UIManager.Instance.ControlTimer(true);
        SFXManager.Instance.PlayMusic();
    }
}
