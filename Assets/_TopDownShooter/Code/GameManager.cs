using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Pools")]
    public Pool BulletPool;
    public Pool MissilePool;
    public Pool CoinPool;

    public Pool EnemyBulletPool;
    public Pool EnemyMissilePool;

    [SerializeField] private Pool enemyPool;

    private bool isWaveActive = false;
    public bool IsWaveActive { set { isWaveActive = value; } }

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; }}

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private float playerScore;
    public float PlayerScore { get { return playerScore; } }

    private int waveIndex = 0;
    public int WaveIndex { get { return waveIndex; } set { waveIndex = value; } }
    public UnityEvent OnPause;
    public UnityEvent OnGameOver;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI lastWaveReachedText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void CheckEndOfWave()
    {
        if(enemyPool.CheckForActiveObj() && !isWaveActive)
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        isPaused = true;
        DrawGameOverUI();
        OnGameOver.Invoke();
    }

    public void DrawGameOverUI()
    {
        highScoreText.text = "Score: " + playerScore;
        lastWaveReachedText.text = "Last Wave Reached: " + waveIndex;
    }
    public void PauseGame()
    {
        isPaused = true;
        OnPause.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        
    }

    public void IncreaseScore(float points)
    {
        playerScore += points;
    }
    
    public void RestartGame()
    {
        isPaused = false;
        playerScore = 0;
        waveIndex = 0;

        SceneManager.LoadScene(0);
    }
}
