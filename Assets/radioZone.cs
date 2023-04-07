using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radioZone : MonoBehaviour
{
    [SerializeField] bool Music;
    [SerializeField] bool News;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemRadio iR;
            if (iR = collision.GetComponentInChildren<itemRadio>())
            {
                if (Music)
                    iR.playMusic();
                else
                {
                    if (News)
                    {
                        iR.playNews();
                    }
                }
            }
                   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            itemRadio iR;
            if (iR = collision.GetComponentInChildren<itemRadio>())
            {
               iR.playNoise();    
            }

        }
    }
}
