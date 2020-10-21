using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public AudioSource musicAS, sfxAS;
    public AudioClip highScoreClip;
    public Text scoreText, finalScore, finalHighScore, levelCounterText;
    public GameObject highScoreText, player, enemy, protection, pause;
    public Animator fading;
    [HideInInspector]
    public int levelCounter;
    playerData playerdata;
    public int Score { get; private set; }
    void Awake()
    {
        levelCounter = 1;

    }
    void OnEnable()
    {
        Reset();

    }
    playerData Load()
    {
        playerData pData = new playerData();
        string dataJson = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "data.txt"));
        JsonUtility.FromJsonOverwrite(dataJson, pData);
        return pData;
    }
    public void Save()
    {
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "data.txt"), playerdata.saveToString());
        finalScore.text = "Score : " + playerdata.score.ToString();
        finalHighScore.text = "High Score : " + playerdata.highScore.ToString();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            sfxAS.Play();
            pause.SetActive(true);
            pauseGame();
        }
    }

    public void back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }
    void pauseGame()
    {
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
    }
    public void IncrementScore(int N)
    {

        Score = Score + (N * Fib(N + 1) * 10);

        playerdata.score = Score;
        scoreText.text = Score.ToString();
        if (Score > playerdata.highScore)
        {
            highScoreText.SetActive(true);
            sfxAS.clip = highScoreClip;
            sfxAS.Play();
            playerdata.highScore = Score;
        }
        Save();

    }
    int Fib(int N)
    {
        if (N == 2)
        {
            return 1;
        }
        int n1 = 0, n2 = 1, nFinal = 2;
        for (int i = 2; i <= N; ++i)
        {
            nFinal = n1 + n2;
            n1 = n2;
            n2 = nFinal;
        }
        return nFinal;
    }
    public void Reset()
    {
        fading.SetTrigger("fade");
        if (levelCounter == 1)
        {
            Score = 0;
        }

        scoreText.text = Score.ToString();
        highScoreText.SetActive(false);
        player.SetActive(false);
        enemy.SetActive(false);
        protection.SetActive(false);
        playerdata = Load();
        playerdata.score = Score;
        musicAS.mute = !playerdata.music;
        sfxAS.mute = !playerdata.sfx;
        player.SetActive(true);
        enemy.SetActive(true);
        protection.SetActive(true);
        levelCounterText.text = "Level : " + levelCounter.ToString();
    }
}
