using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleButton : MonoBehaviour
{
    [SerializeField] private puzzleTower pT;
    private int buttonNum;
    private bool isStay;
    public void SetNumber(int i)
    {
        buttonNum = i;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Press", false);
            isStay = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StayCoroutine());
        }
    }

    IEnumerator StayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (isStay)
        {
            GetComponent<Animator>().SetBool("Press", true);
            pT.ButtonPressed(buttonNum);
        }
    }

    private void Start()
    {
        GetComponent<Animator>().runtimeAnimatorController =
            Instantiate(GetComponent<Animator>().runtimeAnimatorController);
    }
}
