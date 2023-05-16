using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleDrip : MonoBehaviour
{
    [SerializeField] private float addWaterAmount;
    [SerializeField] private GameObject Module;
    [SerializeField] private BreakManager o_breakManager;


    private AudioSource o_audioSource;
    private GameObject Bucket;
    private HandsHolds HH;

    private itemBucket o_itemBucket;
    // Start is called before the first frame update

    private void Start()
    {
   
        HH = GameObject.FindObjectOfType<HandsHolds>();
        o_audioSource = gameObject.GetComponent<AudioSource>();
        Module.SetActive(false);
    }

 

    public void CloseBreach()
    {
       
        if (HH.ItemNum() == 3)
        {
            if (o_itemBucket = HH.Item().GetComponentInChildren<itemBucket>()){
                o_itemBucket.AddWater(addWaterAmount);
                o_breakManager.LeakStopped();
                Module.SetActive(false);
                
            }
        }
    }

   
}
