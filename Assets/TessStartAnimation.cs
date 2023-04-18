using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TessStartAnimation : MonoBehaviour
{
    public Animator Anim;
    public Animator Point;
    public float t1;
    public float t2;
    public float t3;
    // Start is called before the first frame update
    public void StartC()
    {
        StartCoroutine(C());        
    }
    IEnumerator C()
    {
        Point.enabled = true;
        Anim.SetBool("Jump", true);
        yield return new WaitForSeconds(0.01f);
        Anim.SetBool("Jump", false);
        yield return new WaitForSeconds(t1);
        Anim.SetBool("Run", true);
        Anim.SetBool("Jump", false);
        yield return new WaitForSeconds(0.01f);
        Anim.SetBool("Run", false);
        yield return new WaitForSeconds(t2);
        Anim.SetBool("Stop", true);
        Anim.SetBool("Run", false);
        yield return new WaitForSeconds(t3);
        Anim.SetBool("Idle", true);
        Anim.SetBool("Stop", false);
        Point.enabled = false;
       
    }

}
