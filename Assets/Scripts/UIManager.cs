using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] Text _coinText, _timerText;
    [SerializeField] private float _maxTime;
    [SerializeField] private bool _canTime;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] CanvasGroup _fadeImage;

    [Header("End Game")]
    [SerializeField] GameObject _endGamePanel;
    [SerializeField] Text _bestTimeText, _newTimeText;
    [SerializeField] AudioClip _congratulationsClip;

    private float _timer;
    private float _newTimeScore;
    private float _bestTimeScore;

    Player _player;

    private void Awake()
    {
        _instance = this;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        _timer = _maxTime;
        TimerDisplayCalculation(_maxTime, _timerText);
    }

    private void Update()
    {
        if (_canTime)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }

    }

    public void UpdateCoins()
    {
        _coinText.text =
            $"{_player.GetTotalCoins()} / {GameManager.Instance.GetTotalCoinsInLevel()}";
    }

    public void UpdateTimer()
    {
        _timer -= Time.deltaTime;
        TimerDisplayCalculation(_timer, _timerText);
    }

    private void TimerDisplayCalculation(float time, Text text)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100f);

        text.text = $"{minutes} : {seconds} : {milliseconds}";
    }

    public void ControlTimer(bool active)
    {
        _canTime = active;
    }

    public void EndGameUI()
    {
        _canTime = false;
        StartCoroutine(EndGameRoutine());
    }

    IEnumerator EndGameRoutine()
    {
        yield return new WaitForSeconds(1f);
        _endGamePanel.SetActive(true);
        GetNewTimeScore();
        GetBestTimeScore();
    }

    public void GetNewTimeScore()
    {
        if (!_canTime)
        {
            _newTimeScore = _timer;
            TimerDisplayCalculation(_newTimeScore, _newTimeText);
        }
    }

    public void GetBestTimeScore()
    {
        if (PlayerPrefs.GetFloat("Best Score") < 1f)
        {
            _bestTimeScore = _newTimeScore;
            PlayerPrefs.SetFloat("Best Score", _bestTimeScore);
        }
        else if(_newTimeScore < PlayerPrefs.GetFloat("Best Score"))
        {
            SFXManager.Instance.PlaySFX(_congratulationsClip, 1, 2,1.5f);
            _bestTimeScore = _newTimeScore;
            PlayerPrefs.SetFloat("Best Score", _bestTimeScore);
        }
        else if (_newTimeScore > PlayerPrefs.GetFloat("Best Score"))
        {
            _bestTimeScore = PlayerPrefs.GetFloat("Best Score");
        }    

        TimerDisplayCalculation(_bestTimeScore, _bestTimeText);
    }

    public void PauseMenu()
    {
        _pausePanel.SetActive(!_pausePanel.activeInHierarchy);

        if (_pausePanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /*public void FadeIn(float time)
    {
        _fadeImage.alpha = Mathf.Lerp(0f, 1f, time);
    }*/
    public IEnumerator FadeIn(float time)
    {
        while (_fadeImage.alpha < 1)
        {
            _fadeImage.alpha += Time.deltaTime / time;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float time)
    {
        while (_fadeImage.alpha > 0)
        {
            _fadeImage.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
}
