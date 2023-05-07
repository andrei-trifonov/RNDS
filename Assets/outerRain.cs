using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class outerRain : MonoBehaviour
{
  
    private itemBucket o_itemBucket;
  
    private bool Inside;
    private HandsHolds HH;
    private float timeLeft = 0f;
    private void Start()
    {
        HH = GameObject.FindObjectOfType<HandsHolds>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            Inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inside = false;
        }
    }

    private void FixedUpdate () {
        if(timeLeft <= 0){
            // Выполняем функцию здесь

            if (HH.ItemNum() == 3 && Inside)
            {
                o_itemBucket = HH.Item().GetComponentInChildren<itemBucket>();
                o_itemBucket.AddWater(20);
            }
            timeLeft = 2f;
        }

        timeLeft -= Time.deltaTime;
    }
    
}
