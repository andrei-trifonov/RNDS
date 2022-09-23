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
    private CarryManager o_CManager;

    private itemBucket o_itemBucket;
    // Start is called before the first frame update

    private void Start()
    {
        o_audioSource = gameObject.GetComponent<AudioSource>();
        Module.SetActive(false);
    }

 

    public void CloseBreach()
    {
       
        if (o_CManager && (Bucket = o_CManager.GetPickedItem().transform.GetChild(0).gameObject))
        {
            if (o_itemBucket = Bucket.GetComponent<itemBucket>()){
                o_itemBucket.AddWater(addWaterAmount);
                o_breakManager.LeakStopped();
                Module.SetActive(false);
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            o_CManager = col.GetComponent<CarryManager>();
        }
    }
}
