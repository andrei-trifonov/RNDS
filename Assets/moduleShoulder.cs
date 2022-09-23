using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[ExecuteInEditMode]
public class moduleShoulder : MonoBehaviour
{
    public bool pos;
    public bool Correcting;
    // Start is called before the first frame update
    void Start()
        {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Debug"))
        {
            Correcting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Debug"))
        {
            Correcting = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!Correcting)
        {
            if (pos)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 0.1f);
            else
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 0.1f);
            }
        }

    }

    void _Correcting()
    {
      
    }
}
