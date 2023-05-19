using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleFinger : MonoBehaviour
{
    public AudioClip finishSound;
    public outerStopZone SZ;
    public PlatformerCharacter2D PC2D;
    public Transform CameraPoint;
    public bool Laser1;
    public bool Laser2;
    public GameObject[] finishPuzzle;
    public GameObject[] itemsPuzzle;
    public PanZoom Camera;
    private Vector3 campos;
    private AudioSource m_AudioSource;
    private bool ended;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        int  save = Int32.Parse(PlayerPrefs.GetString("Save"));
        if (save % 10 > 5)
        {
            SZ.UnblockMachine();
            campos = PC2D.transform.position + new Vector3(0f, 0.12f, -9.51f);
            foreach (GameObject obj in finishPuzzle)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in itemsPuzzle)
            {
                obj.SetActive(false);
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() 
    {

        if (Laser1 && Laser2)
        {

            EndPuzzle();
        }
        Laser1 = false;
        Laser2 = false;
    }
    private void EndPuzzle()
    {
        if (!ended)
        {
            ended = true;
            m_AudioSource.PlayOneShot(finishSound);
            StartCoroutine(EndPuzzleCoroutine());
        }
    }
    IEnumerator EndPuzzleCoroutine()
    {
       
        SZ.UnblockMachine();
        campos = PC2D.transform.position + new Vector3(0f, 0.12f, -9.51f);
        Camera.MoveToPosition(CameraPoint.position);
        Camera.ChangeZoom(19);
        PC2D.Block();
        yield return new WaitForSeconds(2);
        foreach (GameObject obj in finishPuzzle)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(3);
        Camera.MoveToPosition(campos);
        Camera.ChangeZoom(2.6f);
        yield return new WaitForSeconds(4);
        PC2D.UnBlock();
       
        foreach (GameObject obj in itemsPuzzle)
        {
            obj.SetActive(false);
        }

        PlayerPrefs.SetString("Save", "06");
        Destroy(gameObject);
    }
}
