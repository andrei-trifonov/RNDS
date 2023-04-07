using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.PlayerLoop;

public class itemLight : MonoBehaviour
{
   
    private float Pos;
    int last;
   
    //[SerializeField] private SpriteRenderer Feathering;
    [SerializeField] private GameObject Mask;
    [SerializeField] private GameObject lightSource;
    [SerializeField] private GameObject lightEffect;
    private bool isOn = true;
    private Quaternion[] baseQuartenion = new Quaternion[4];
    private List<GameObject> ShadowsObj = new List<GameObject>();
    private List<itemLightShadow> Shadows = new List<itemLightShadow>();
    private List<bool> ShadowsB = new List<bool>();
    private ShadowGPT[]  Shadows3D;
  
    private void Awake()
    {
        ShadowsObj = GameObject.FindGameObjectsWithTag("ShadowCaster").ToList();
        for (int i = 0; i < ShadowsObj.Count; i++) {

            ShadowsB.Add(false);
            Shadows.Add(ShadowsObj[i].GetComponent<itemLightShadow>());
        }
        Shadows3D = GameObject.FindObjectsOfType<ShadowGPT>();
        foreach(ShadowGPT shadow in Shadows3D)
        {
            if (!shadow.isGlobalLight)
            shadow.AddLight(lightSource) ;
        }
        baseQuartenion[3] = new Quaternion(0.626f,-0.009f,0.001f,0.779f);
        baseQuartenion[2] = new Quaternion(0.536f,-0.204f,0.359f,0.735f); //Evening
        baseQuartenion[1] = new Quaternion(0.008f,-0.617f,0.786f,0.005f);//Night
        baseQuartenion[0] = new Quaternion(0.536f,0.204f,-0.359f,0.735f); //Mourning
        GameObject maskParent = GameObject.FindGameObjectWithTag("Tint");

        Mask = Instantiate(Mask, maskParent.transform);
        Mask.GetComponent<FixedPos>().SetPoint( gameObject);
        Mask.GetComponent<itemLightCol>().lightParent = gameObject.transform.parent.gameObject;

    }





    private void FixedUpdate()
    {
       if (isOn)
        for (int j = 0; j < Shadows.Count; j++)
        {

           
            if (Shadows[j].GetComponent<itemLightShadow>().isPriority( Vector2.Distance(Shadows[j].transform.position, gameObject.transform.position) , gameObject))
            {
                Shadows[j].SetBusy(true);
                ShadowsB[j] = true;
                Vector2 distance = Shadows[j].transform.position - gameObject.transform.position;
                distance = distance.normalized;
                float point = Vector2.Dot(distance, transform.right);
                Pos = Mathf.Acos(point) * Mathf.Rad2Deg;


                Quaternion leftBorder = baseQuartenion[0];
                Quaternion rightBorder = baseQuartenion[0];

                int i = (int) Pos / 90;
                if (i == 3)
                {
                    leftBorder = baseQuartenion[i];
                    rightBorder = baseQuartenion[0];
                }

                if (i>= 0 && i <3)
                {
                    leftBorder = baseQuartenion[i];
                    rightBorder = baseQuartenion[i + 1];
                }



                Shadows[j].transform.rotation = Quaternion.Lerp(leftBorder, rightBorder, (Pos % 90) / 90);



            }
            else
                if (ShadowsB[j] == true)
                {
                    ShadowsB[j] = false;
                    Shadows[j].SetBusy(false);
                }
        }
    }

    private void OnDisable()
    {
        if (Mask)
            Mask.SetActive(false);
        for (int j = 0; j < Shadows.Count; j++)
        {
            Shadows[j].SetBusy(false);
            
        }
        foreach (ShadowGPT shadow in Shadows3D)
        {
            if (!shadow.isGlobalLight)
                shadow.RemoveLight(lightSource);
        }

    }
  


    public void OnOff()
    {
        if (isOn) {
            lightEffect.SetActive(false);
            Mask.SetActive(false);
            for (int j = 0; j < Shadows.Count; j++)
            {
                ShadowsB[j] = false;
                Shadows[j].SetBusy(false);
               
            }
            foreach (ShadowGPT shadow in Shadows3D)
            {
                if (!shadow.isGlobalLight)
                    shadow.RemoveLight(lightSource);
            }
        }
        else
        {
            lightEffect.SetActive(true);
            Mask.SetActive(true);
            for (int j = 0; j < Shadows.Count; j++)
            {
                ShadowsB[j] = true;
                Shadows[j].SetBusy(true);
              
            }
            foreach (ShadowGPT shadow in Shadows3D)
            {
                if (!shadow.isGlobalLight)
                    shadow.AddLight(lightSource);
            }
        }
        isOn = !isOn;
    }




}
