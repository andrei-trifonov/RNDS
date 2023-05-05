using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TessStartAnimation : MonoBehaviour
{
    public GameObject[] TessStartPack;
    public PanZoom Cam;
    public GameObject Dialogue;
    public Animator Anim;
    public Animator Point;
    public GameObject Trigger;
    public PlatformerCharacter2D PC2D;
    public float t1;
    public float t2;
    public float t3;
    public float Zoom;
    public float oldZoom;
    public AudioClip Sounds;
    private AudioSource m_AudioSource;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void StartC()
    {
        m_AudioSource.PlayOneShot(Sounds);
        StartCoroutine(C());        
    }
    public void DestroyMe()
    {
        foreach (GameObject go in TessStartPack)
        {
            Destroy(go);
        }
        
        PC2D.GetComponent<Platformer2DUserControl>().useBeacon = true;
        Destroy(Dialogue);
    }
    IEnumerator C()
    {
        PC2D.Block();
        Cam.ChangeZoom(Zoom);
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
        PC2D.UnBlock();
        Dialogue.SetActive(true);
        Cam.ChangeZoom(oldZoom);
        Destroy(Trigger);
    }

}
