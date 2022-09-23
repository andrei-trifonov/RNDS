using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleLadder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Animator m_Animator2;
    [SerializeField] private GameObject rotObject;
    [SerializeField] private float borderAngle;
     private float rotZ;

    void FixedUpdate()
    {
        rotZ = Mathf.Abs(rotObject.transform.rotation.z * Mathf.Rad2Deg);
        if (rotZ > borderAngle)
        {
            m_Animator.SetBool("Activate", true);
            m_Animator2.SetBool("Activate", true);
        }
        else{
            m_Animator.SetBool("Activate", false);
            m_Animator2.SetBool("Activate", false);
        }



    }
}
