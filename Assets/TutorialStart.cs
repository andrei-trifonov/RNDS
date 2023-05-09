using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    public GameObject[] destroyBeforeStartObjects;
    public GameObject[] tutorialSigns;
    public GameObject[] destroyAfterObjects;
    public Transform transportPoint;
    public PanZoom Cam;
    public PlatformerCharacter2D PC2D;
    public float t1; 
    public float Zoom;
    public float oldZoom;
    private Vector3 campos;

    private void Start()
    {
        foreach (GameObject go in tutorialSigns)
        {
            go.SetActive(false);
        }
    }

    // Start is called before the first frame update
    public void StartC()
    {
      
      
       
        StartCoroutine(C());        
    }

    IEnumerator C()
    {
        PC2D.Block();
        destroyBeforeStartObjects[7].SetActive(false);
        Cam.ChangeZoom(12);
        Cam.MoveToPosition(transportPoint.position);
        yield return new WaitForSeconds(5f);
        foreach (GameObject go in destroyBeforeStartObjects)
        {
            go.SetActive(false);
        }
        Cam.ChangeZoom(Zoom);
        for (int i = 0; i < tutorialSigns.Length; i++)
        {
            foreach (GameObject go in tutorialSigns)
            {
                go.SetActive(false);
            }
            tutorialSigns[i].SetActive(true);
            campos = PC2D.transform.position + new Vector3(0f, 0.12f, -9.51f);
            Cam.MoveToPosition(tutorialSigns[i].transform.position);
            yield return new WaitForSeconds(5f);
        }
        foreach (GameObject go in tutorialSigns)
        {
            Destroy(go);
        }
        foreach (GameObject go in destroyAfterObjects)
        {
            Destroy(go);
        }
        PC2D.UnBlock();
        Cam.ChangeZoom(oldZoom);
        Cam.MoveToPosition(campos);
        foreach (GameObject go in destroyBeforeStartObjects)
        {
            go.SetActive(true);
        }
        Destroy(this);
    }

}
