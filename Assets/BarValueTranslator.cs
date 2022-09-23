using System;
using System.Collections;
using System.Collections.Generic;
using AdultLink;
using UnityEngine;

public class BarValueTranslator : MonoBehaviour
{
    [SerializeField] private bool transRot;
    [SerializeField] private bool transPos;
    [SerializeField] private Vector3 Axis  ;
    [SerializeField] private GameObject Object;
    [SerializeField] private HealBar Bar;
    private Vector3 initialPos;
    private void Start()
    {

        initialPos = Object.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transRot)
        {
                Quaternion newrot = Quaternion.Euler(Axis.x * Bar.outputValue, Axis.y * Bar.outputValue, Axis.z * Bar.outputValue);
                Object.transform.rotation = newrot;
        }
        if (transPos)
        {
            if (Axis.y>0)
                 Object.transform.localPosition = new Vector3(Object.transform.localPosition.x,   Bar.outputValue ,Object.transform.localPosition.z);
            if (Axis.x>0)
                Object.transform.localPosition = new Vector3(Bar.outputValue , Object.transform.localPosition.y  ,Object.transform.localPosition.z);

        }
    }
}
