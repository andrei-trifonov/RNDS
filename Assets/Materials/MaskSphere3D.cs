using System;
using System.Collections.Generic;
using UnityEngine;

public class MaskSphere3D : MonoBehaviour
{
    [SerializeField] private Material transparentMat;
    private List<Material> transparent = new List<Material>();
    private Material[] objMat;

    private void Start()
    {
        for(int i=0; i < GetComponent<MeshRenderer>().materials.Length; i++)
        {
            transparent.Add(transparentMat);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Front3D"))
        {
            objMat  = GetComponent<MeshRenderer>().materials;
       
                GetComponent<MeshRenderer>().materials  = transparent.ToArray();     
            
           
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Front3D"))
        {
            GetComponent<MeshRenderer>().materials  = objMat;     
        }
    }
}