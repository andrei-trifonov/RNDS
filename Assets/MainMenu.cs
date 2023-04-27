using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Slider Music;
    public Slider Audio;
    public Slider Master;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
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
    public void SetMusicVolume(float num)
    {

        PlayerPrefs.SetFloat("Music", num);
    }
    public void SetSoundVolume(float num)
    {

        PlayerPrefs.SetFloat("Sound", num);
    }
    public void SetMasterVolume(float num)
    {

        PlayerPrefs.SetFloat("Master", num);
    }
    public void GetMusicVolume()
    {

        Music.value = PlayerPrefs.GetFloat("Music");
    }
    public void GetSoundVolume()
    {

        Music.value = PlayerPrefs.GetFloat("Sound");
    }
    public void GetMasterVolume()
    {

        Music.value = PlayerPrefs.GetFloat("Master");
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
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
