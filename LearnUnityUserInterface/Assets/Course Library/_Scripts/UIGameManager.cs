using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Timer = UnityTimer.Timer;

public class UIGameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float _spawnRate = 1.0f;

    private int _score;

    private int _liveNumber = 300;

    private Timer _countdownTimer;
    private int _countdownNumber = 3;

    //private int _difficulty;

    // UI Contents
    private GameObject _titleScreen;
    private GameObject _gameScreen;
    private GameObject _gameOverScreen;
    public GameObject pausePanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private TextMeshProUGUI _liveText;
    private TextMeshProUGUI _timeText;

    private Button _restartBtn;
    private Button _easyBtn;
    private Button _mediumBtn;
    private Button _hardBtn;
    //private Button _pauseBtn;

    public bool isGameActive;
    public bool _isGamePaused;

    private void Awake()
    {
        UIInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //UIInit();           

    }


    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }


    private void UIInit()
    {
        _titleScreen = GameObject.Find("TitleScreen");
        _gameScreen = GameObject.Find("GameScreen");
        _gameOverScreen = GameObject.Find("GameOverScreen");

        _liveText = GameObject.Find("Live Text").GetComponent<TextMeshProUGUI>();
        _liveText.text = "Lives: " + _liveNumber;

        _timeText = GameObject.Find("Countdown Text").GetComponent<TextMeshProUGUI>();
               

        _score = 0;
        scoreText.text = "Score: " + _score;
        //scoreText.gameObject.SetActive(false);
        isGameActive = false;       
        

        _restartBtn = GameObject.Find("Restart Button").GetComponent<Button>();
        _restartBtn.onClick.AddListener(ResetGame);
        //_restartBtn.gameObject.SetActive(false);

        _easyBtn = GameObject.Find("Easy Button").GetComponent<Button>();
        _easyBtn.onClick.AddListener(OnClickEasyButton);
        //_restartBtn.gameObject.SetActive(false);

        _mediumBtn = GameObject.Find("Medium Button").GetComponent<Button>();
        _mediumBtn.onClick.AddListener(OnClickMediumButton);
        //_restartBtn.gameObject.SetActive(false);

        _hardBtn = GameObject.Find("Hard Button").GetComponent<Button>();
        _hardBtn.onClick.AddListener(OnClickHardButton);
        //_restartBtn.gameObject.SetActive(false);

        //_pauseBtn = GameObject.Find("Pause Button").GetComponent<Button>();
        //_pauseBtn.onClick.AddListener(OnClickPauseButton);

        _gameScreen.SetActive(false);
        _gameOverScreen.SetActive(false);

    }

    private void SpawnTarget()
    {
        if (isGameActive)
        {
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        else
        {
            CancelInvoke();
        }

    }

    private void GameStart(int difficulty, int gameTime)
    {

        isGameActive = true;

        _titleScreen.SetActive(false);
        _gameScreen.SetActive(true);

        _countdownNumber = gameTime;
        _timeText.text = "Time: " + _countdownNumber;        
        _countdownTimer = Timer.Register(1, UpdateCountDown, isLooped: true);

        InvokeRepeating("SpawnTarget", 0, _spawnRate /= difficulty);
    }

    public void UpdateScore(int scoreToAdd) {

        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;

    }

    private void UpdateCountDown()
    {
        _countdownNumber--;
        _timeText.text = "Time: " + _countdownNumber;

        if (_countdownNumber <=0)
        {
            _countdownTimer.Cancel();
            GameOver();
        }

        
    }

    public void GameOver()
    {
        isGameActive = false;
        //gameOverText.gameObject.SetActive(true);
        //_restartBtn.gameObject.SetActive(true);
        _gameOverScreen.SetActive(true);
    }

    // Start game


    public void ResetGame()
    {       

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LiveCountMinusOne()
    {
        if (isGameActive == false)
        {
            return;
        }

        _liveNumber--;
        _liveText.text = "Lives: " + _liveNumber;

        if (_liveNumber <= 0)
        {
            GameOver();
        }
    }

    private void OnClickEasyButton()
    {
        //Debug.Log("Easy Mode");

        GameStart(1, 9);

        
    }
    private void OnClickMediumButton()
    {
        //Debug.Log("Medium Mode");
        GameStart(2, 6);
    }
    private void OnClickHardButton()
    {
        //Debug.Log("Hard Mode");

        GameStart(4, 3);
    }

    private void OnClickPauseButton()
    {
        Time.timeScale = 0;
    }

    private void PauseGame()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;

                _isGamePaused = !_isGamePaused;
                pausePanel.SetActive(_isGamePaused);
                Time.timeScale = _isGamePaused ? 0 : 1;

            }
        }
    }

}
