using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemRadio : MonoBehaviour
{
    [SerializeField] ASFade audioPlayer;
    [SerializeField] DialogueSystem ds;
    [SerializeField] List<AudioClip> Music;
    [SerializeField] AudioClip Noise;

    private void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<ASFade>();
    }
    public void playMusic()
    {
        StopAllCoroutines();
        audioPlayer.Fade(Music[Random.Range(0,Music.Count)]);
    }

    public void playNews()
    {
        audioPlayer.Fade();
        StopAllCoroutines();
        StartCoroutine(newsCoroutine());
    }

    IEnumerator newsCoroutine()
    {
        while (true)
        {
            ds.ContinueDialogue();
            yield return new WaitForSeconds(5);
        }
    }
    public void playNoise()
    {
        StopAllCoroutines();
        audioPlayer.Fade(Noise);
    }
}
