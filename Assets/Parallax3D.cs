using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Parallax3D : MonoBehaviour{

     
    [SerializeField] private GameObject opticCenter;
    [SerializeField] private float symmetricBounds;
    [FormerlySerializedAs("xmaxOffset")] [SerializeField] private float maxRot;
    private GameObject Camera;
    private float xOffset;
    private float Percentage;
    private float xtranslitedOffset;
    
    private Transform startTransform;

    private float startScale;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
            startTransform = gameObject.transform;
            startScale = startTransform.localScale.x;

    }
    
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        var camPos = Camera.transform.position;
        var centerPos = opticCenter.transform.position;
        xOffset = camPos.x - centerPos.x;
        xtranslitedOffset = maxRot * Mathf.Clamp(xOffset / symmetricBounds, -1, 1) * (-1);
        var rotation = gameObject.transform.rotation;
        rotation = quaternion.EulerXYZ(startTransform.rotation.x , (startTransform.rotation.y + xtranslitedOffset ), startTransform.rotation.z);
        gameObject.transform.rotation = rotation;
 
       
    }
}
