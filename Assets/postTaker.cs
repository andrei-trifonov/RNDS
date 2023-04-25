using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class postTaker : commonMagneticPlace
{


    [SerializeField] private MonoBehaviour Script;
    [SerializeField] private string Function;

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    public void TakePost()
    {

        if (HH.ItemNum() == 28)
        {

            hookedItem = HH.Item();
           
            o_CManager.ThrowItem();
            hookedItem.SetActive(false);
            Script.Invoke(Function, 0);
            
                
            
        }

        }
        
    }

    // Update is called once per frame


