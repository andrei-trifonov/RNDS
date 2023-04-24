using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Manager : MonoBehaviour
{
    [SerializeField] private  puzzleScales Scale1;
    [SerializeField] private  puzzleScales Scale2;
    [SerializeField] private Animator Gate;
    [SerializeField] private GameObject toDestroy1;
    [SerializeField] private GameObject toDestroy2;
    private bool Done;
    public void CheckWeight()
    {
        int w1 = Scale1.GetWeight();
        int w2 = Scale2.GetWeight();
        if (!Done)
        {
            if (w1 > w2)
            {
                Gate.SetBool("Right", false);
                Gate.SetBool("Left", true);
            }

            if (w1 < w2)
            {
                Gate.SetBool("Left", false);
                Gate.SetBool("Right", true);
            }
            if (w1 == w2)
            {   
                Gate.SetBool("Left", false);
                Gate.SetBool("Right", false);
                if (w1 == 5)
                {
                    Done = true;
                    
                    Gate.SetBool("Open", true);
                    StartCoroutine(DestroyCoroutine());
                }
            }
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(6);
            toDestroy1.SetActive(false);
            Scale1.enabled = false;
            Scale2.enabled = false;
            toDestroy2.SetActive(false);
        }

        
    }
}
