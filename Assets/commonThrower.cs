using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonThrower : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject garbagePrefab;
    public GameObject garbageSmoke;
    public  float garbageSpeed;
    public  float throwAngle;
   
    protected PanZoom panZoomCamera;

    public void FindCamera()
    {
        panZoomCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PanZoom>();
    }
    public IEnumerator ThrowerCoroutine(GameObject spawnObject, float delayBefore, float delayAfter)
    {
        BeforeSpawn();
        yield return new WaitForSeconds(delayBefore);
        garbageSmoke.SetActive(true);
        GameObject garbage;
        garbage = Instantiate(spawnObject,throwPoint.position, throwPoint.rotation, throwPoint);
        inSpawn(garbage);
        garbage.GetComponent<Rigidbody2D>().AddForce((Vector2.up - new Vector2 (  Mathf.Cos(throwAngle * Mathf.Deg2Rad),0 )) * garbageSpeed);
        if (garbage.GetComponentInChildren<Canvas>())
            panZoomCamera.Canvases.Add(garbage.GetComponentInChildren<Canvas>().gameObject);
        yield return new WaitForSeconds(delayAfter);
        garbageSmoke.SetActive(false);
        AfterSpawn();
    }
    public virtual  void DoAction(int itemID){}
    public virtual  void DoAction(int itemID, float par1){}


    public virtual void BeforeSpawn()
    {
    }
    public virtual void inSpawn(GameObject garbage)
    {
    }
    public virtual void AfterSpawn()
    {
    }

}

