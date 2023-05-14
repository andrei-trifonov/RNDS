using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class moduleHook : commonMagneticPlace
{
  

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
        Debug.Log(PlayerPrefs.GetInt(hookName ) + hookName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);   
    }
    private void OnTriggerExit2D(Collider2D other)
    { 
        base.OnTriggerExit2D(other);   
    }
    

    public  void OnClick()
    {
       base.OnClick();
    }

    void FixedUpdate()
    {
        base.OnFixedUpdate();
    }
    // Update is called once per frame

}
