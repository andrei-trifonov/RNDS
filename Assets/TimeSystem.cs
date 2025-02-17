using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

[System.Serializable]
public struct ShadowParam
{
    public bool castVertical;
    public bool controlledOuter;
    public GameObject Shadow;
    public GameObject ShadowVertical;
    public LayerMask shadowCollision;
    public float maxShadowVerticalDistance;
}
public class TimeSystem : MonoBehaviour
{
    [SerializeField] private GameObject sunLight;
    [SerializeField] private SpriteRenderer tintSpiteF;
    [SerializeField] private SpriteRenderer tintSpiteB;
    [SerializeField] private float timeStep;
    [SerializeField] private Color[] tintColors = new Color[4] ;
    [SerializeField] private PostProcessVolume PPV;
    [SerializeField] private PostProcessProfile[] PPVPresets = new PostProcessProfile[4];
    [SerializeField] private bool Debug;
    [SerializeField] private bool Day, Evening, Night, Mourning;
    [SerializeField] private GameObject Stars;
    [SerializeField] private GameObject Sun;
    [SerializeField] private Animator Fade;
    [SerializeField] private Shadow2D _shadow2D;
    private Coroutine c;
    private bool bestQuality;
    float timeLeft;
    private int i;
        Color targetColor;
    Color currentColor;
    private Color startColor;
    
    private void Start()
    {
        timeLeft = 2;
        
        targetColor = tintColors[0];
        Day = true;
        if (PlayerPrefs.GetInt("Quality") == 2)
            bestQuality = true;
    }



    
    void FixedUpdate()
    {
      
        if (!Debug){
           
            if (timeLeft >= 1)
            {
                Day = false;
                Evening = false;
                Night = false;
                Mourning = false;
              
                
                i++;
                if (i == 4) i = 0;
                c = StartCoroutine(InstantAdjust(i));
             
                
                    // transition complete
                // assign the target colo
                timeLeft = 0;
                startColor = targetColor;
                // start a new transition
                targetColor = tintColors[i];
                PPV.profile = PPVPresets[i];
               
            }
            else
            {
                // transition in progress
                // calculate interpolated color
                timeLeft += Time.deltaTime / timeStep;
                currentColor = Color.Lerp(startColor, targetColor, timeLeft);
              //  Quaternion rotation = Quaternion.Lerp(currShadowRot, shadowRot, timeLeft);
                                                 
                
            }
        }
        else
        {
           
            if (Day) 
                c = StartCoroutine(InstantAdjust(0));
            if (Evening)
                c = StartCoroutine(InstantAdjust(1));
            if (Night)
                c = StartCoroutine(InstantAdjust(2));
            if (Mourning)
                c = StartCoroutine(InstantAdjust(3));
        }

    
        tintSpiteF.material.color = currentColor;
        tintSpiteB.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a/2);

    }

    IEnumerator InstantAdjust(int i)
    {
        Fade.SetBool("Fade", true);
        yield return new WaitForSeconds(0.3f);
        Fade.SetBool("Fade", false);
        switch (i)
        {
            case 0:
            {
                   
                    sunLight.transform.localPosition = new Vector3(0, 5.53f, -12.99f);

                    Day = true;

                    Stars.SetActive(false);
                    Sun.SetActive(false);
                    if (!bestQuality)
                       _shadow2D.SetShadow(0);
                    
            }
                
                break;

            case 1:
                {
                    sunLight.transform.localPosition = new Vector3(4.16f, 5.53f, -12.99f);


                    Evening = true;
                    if (!bestQuality)
                      _shadow2D.SetShadow(1);
                }
                break;

            case 2:
                {

                   
                    sunLight.transform.localPosition = new Vector3(0, 0, 30);


                    Night = true;

                    Stars.SetActive(true);
                    if (!bestQuality)
                        _shadow2D.SetShadow(2);
                }
                break;
            case 3:
                {
                    sunLight.SetActive(true);
                    sunLight.transform.localPosition = new Vector3(-4.16f, 5.53f, -12.99f);
                    Mourning = true;

                    Sun.SetActive(true);
                    if (!bestQuality)
                        _shadow2D.SetShadow(3);
                }
                break;
            default:
                {
                    sunLight.transform.localPosition = new Vector3(0, 5.53f, -12.99f);
                   

                    Sun.SetActive(false);
                    Stars.SetActive(false);

                    Day = true;
                    if (!bestQuality)
                      _shadow2D.SetShadow(0);
                }
                break;
            
        }
        StopCoroutine(c);
    }

    public bool isNight()
    {
        return Night;
    }

    public Color GetTintColor()
    {
        return currentColor;
    }
}
