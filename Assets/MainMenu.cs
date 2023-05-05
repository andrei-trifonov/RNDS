using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject startText;
    public GameObject promptText;
    public GameObject Settings_;
    public Slider Music;
    public Slider Audio;
    public Slider Master;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    private bool start;
    private float backupMusic;
    private float backupSound;
    private float backupMaster;
    private int backupControls;
    public void FirstLaunch()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Controls", 1);
        PlayerPrefs.SetInt("Quality", 2);
        PlayerPrefs.SetString("Save", "00");
        PlayerPrefs.SetInt("FirstLaunch", 1);
        PlayerPrefs.SetString("Language", "RUS");
        PlayerPrefs.SetFloat("Music", 1);
        PlayerPrefs.SetFloat("Sound", 1);
        PlayerPrefs.SetFloat("Master", 1);
        PlayerPrefs.SetInt("CrueCount", 1);
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            FirstLaunch();
        }
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        GetMasterVolume();
        GetMusicVolume();
        GetSoundVolume();
 
    }

    public void Start_()
    {
        if (start)
        {
            NewGame();
        }
        else
        {
            start = true;
            startText.SetActive(false);
            promptText.SetActive(true);
        }
    }
    public void NewGame()
    {
        backupControls = PlayerPrefs.GetInt("Controls");
        backupMaster = PlayerPrefs.GetFloat("Master");
        backupSound = PlayerPrefs.GetFloat("Sound");
        backupMusic = PlayerPrefs.GetFloat("Music");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Controls", backupControls );
        PlayerPrefs.SetInt("Quality", 2);
        PlayerPrefs.SetString("Save", "00");
        PlayerPrefs.SetInt("FirstLaunch", 1);
        PlayerPrefs.SetString("Language", "RUS");
        PlayerPrefs.SetFloat("Music", backupMusic);
        PlayerPrefs.SetFloat("Sound", backupSound);
        PlayerPrefs.SetFloat("Master", backupMaster);
        PlayerPrefs.SetInt("CrueCount", 1);
        Load();
    }
    public void SetQuality(int num)
    {
        QualitySettings.SetQualityLevel(num);
        PlayerPrefs.SetInt("Quality", num);
    }
    public void SetControls(int num)
    {
      
        PlayerPrefs.SetInt("Controls", num);
    }
    public void Settings()
    {
      Settings_.SetActive(!Settings_.activeSelf);
        
    }
    public void SetMusicVolume()
    {
        
        PlayerPrefs.SetFloat("Music", Music.value);
    }
    public void SetSoundVolume()
    {

        PlayerPrefs.SetFloat("Sound",  Audio.value);
    }
    public void SetMasterVolume()
    {

        PlayerPrefs.SetFloat("Master", Master.value);
    }
    public void GetMusicVolume()
    {

        Music.value = PlayerPrefs.GetFloat("Music");
    }
    public void GetSoundVolume()
    {

        Audio.value = PlayerPrefs.GetFloat("Sound");
    }
    public void GetMasterVolume()
    {

        Master.value = PlayerPrefs.GetFloat("Master");
    }
    public void SetLanguage(int num)
    {
        switch (num)
        {
            case 0: PlayerPrefs.SetString("Language", "RUS"); break;
        }
     
    }
    public void Load()
    {
        switch (PlayerPrefs.GetString("Save"))
        {
            case "00": StartCoroutine(LoadAsynchronously(1)); break;

            case "01": StartCoroutine(LoadAsynchronously(1)); break;
        }
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";

            yield return null;
        }
    }
}
