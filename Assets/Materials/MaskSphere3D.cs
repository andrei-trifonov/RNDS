using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct FrontObj
{
    public Collider2D col;
    public Material[] transparent;
    public Material[] objMat;
}

public class MaskSphere3D : MonoBehaviour
{
    [SerializeField] private Material transparentMat;
    private List<FrontObj> objects = new List<FrontObj>();
    private Camera m_Camera;

    bool Inside(Collider2D col)
    {
        foreach (FrontObj obj  in objects)
        {
            if (obj.col == col)
                return true;
        }
        return false;
    }
    FrontObj InsideObj(Collider2D col)
    {
        foreach (FrontObj obj  in objects)
        {
            if (obj.col == col)
                return obj;
        }
        return  new FrontObj();
    }

    private void Start()
    {
        m_Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Front3D")  )
        {
            if (!Inside(col))
            {
                FrontObj frontObj = new FrontObj();
                frontObj.col = col;
                frontObj.transparent = new Material[col.GetComponent<MeshRenderer>().materials.Length];
                for(int i=0; i < col.GetComponent<MeshRenderer>().materials.Length; i++)
               {
                   frontObj.transparent[i] = (transparentMat);
               }
                frontObj.objMat  = col.GetComponent<MeshRenderer>().materials;
                objects.Add(frontObj);
                
                //objects.Add(new FrontObj());
                
                
               
            }
            
        if(Camera.main.orthographicSize<5.55)
            col.GetComponent<MeshRenderer>().materials  = InsideObj(col).transparent.ToArray();     
        else
            col.GetComponent<MeshRenderer>().materials = InsideObj(col).objMat;
           
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Front3D") && InsideObj(col).col == col)
        {

            col.GetComponent<MeshRenderer>().materials = InsideObj(col).objMat;
        }
    }
}