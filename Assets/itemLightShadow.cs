using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemLightShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Shadow;
    [SerializeField] private SpriteRenderer verticalShadow;
    [SerializeField] private float maxDistance;
    private GameObject Master;
    private float masterDistance;
    private bool isBusy;
    
    private void Start()
    {
       
        masterDistance = 999.0f;
        Shadow.enabled = false;
        if (verticalShadow)
            verticalShadow.enabled = false;
    }

    public void SetBusy(bool state)
    {
        if (isBusy != state)
        {

            Shadow.enabled = state;
            if (verticalShadow)
                verticalShadow.enabled = state;
            isBusy = state;
        }

    }
    public bool isPriority(float distance, GameObject master)
    {
        if (distance<maxDistance && (distance < masterDistance || Master == master || !isBusy ))
        {
            Master = master;
            masterDistance = distance;
            return true;
        }
 
       
        return false;

    }
}
