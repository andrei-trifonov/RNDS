using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleHayrolls : MonoBehaviour
{
    private CarryManager CM;
    public AudioClip finishPuzzle;
    public upgradeManager uM;
    public List<GameObject> toDisable;
    public List<GameObject> toEnable;
    public bool T1;
    public bool T2;
    public bool C;
    public bool B;
    public outerStopZone SZ;
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        CM = GameObject.FindObjectOfType<CarryManager>();
        m_AudioSource = GetComponent<AudioSource>();
        int  save = Int32.Parse(PlayerPrefs.GetString("Save"));
        if (save % 10 > 3)
        {
            SZ.UnblockMachine();
            foreach(GameObject obj in toDisable)
            {
                Destroy(obj);
            }
            foreach (GameObject obj in toEnable)
            {
                obj.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item")) {
            if (collision.name == "Triangle")
            {
                T1 = true;
            }
            if (collision.name == "Triangle1")
            {
                T2 = true;
            }
            if (collision.name == "Circle")
            {
                C = true;
            }
            if (collision.name == "Box")
            {
                B = true;
            }
            if (T1 && T2 && C && B)
            {
                EndPuzzle();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.name == "Triangle")
            {
                T1 = false;
            }
            if (collision.name == "Triangle1")
            {
                T2 =false;
            }
            if (collision.name == "Circle")
            {
                C = false;
            }
            if (collision.name == "Box")
            {
                B = false;
            }
        }
    }

    void EndPuzzle()
    {
        
        CM.ThrowItem();
        m_AudioSource.PlayOneShot(finishPuzzle);
        uM.Upgrade(6);
        SZ.UnblockMachine();
        foreach(GameObject obj in toDisable)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in toEnable)
        {
            obj.SetActive(true);
        }
        Destroy(gameObject);
    }
    public void StartPuzzle()
    {
        foreach (GameObject obj in toDisable)
        {
            obj.SetActive(true);
        }
    }

}
