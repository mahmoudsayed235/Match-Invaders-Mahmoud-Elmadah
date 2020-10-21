using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Text highScore, sfx, music;
    [SerializeField] private AudioSource musicAS, sfxAS;
    private string dataPath = "";
    private playerData playerdata;
    private void Awake()
    {
        dataPath = Path.Combine(Application.dataPath, "data.txt");








        playerdata = Load();
        highScore.text = "Highscore : " + playerdata.highScore.ToString();
        updateSettings();
    }
    void updateSettings()
    {
        musicAS.mute = !playerdata.music;
        sfxAS.mute = !playerdata.sfx;
        sfx.text = (playerdata.sfx) ? "ON" : "OFF";
        music.text = (playerdata.music) ? "ON" : "OFF";

    }
    public void Play()
    {
        sfxAS.Play();
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }


    playerData Load()
    {
        playerData pData = new playerData();
        string dataJson = File.ReadAllText(dataPath);
        JsonUtility.FromJsonOverwrite(dataJson, pData);
        return pData;
    }
    public void changeMusic()
    {
        sfxAS.Play();
        playerdata.music = !playerdata.music;
        updateSettings();
        Save();
    }
    public void changeSFX()
    {
        playerdata.sfx = !playerdata.sfx;
        updateSettings();
        Save();
        sfxAS.Play();
    }

    public void Save()
    {
        File.WriteAllText(dataPath, playerdata.saveToString());
    }


}
class playerData
{
    public int highScore;
    public int score;
    public bool sfx;
    public bool music;


    public string saveToString()
    {
        return JsonUtility.ToJson(this);
    }
}