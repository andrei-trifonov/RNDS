using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("Rendering/Material Control")]
public class MaskObject : MonoBehaviour
{


    private Material[] materials;
   
    [SerializeField] private Material transparent;
    private Material[] opaque;

    void OnStart()
    {
       
        materials = GetComponent<MeshRenderer>().materials;
        opaque = materials;
    }

    public void SetMask(bool value)
    {
        if (value)
        {
            for(int i = 0; i< materials.Length; i++)
            {
                materials[i] = transparent;

            }
            
          
        }
        else
        {
            for(int i = 0; i< materials.Length; i++)
            {
                materials[i] = opaque[i];

            }

        }
    }
}


