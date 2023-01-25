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
    [SerializeField] private List<ShadowParam> charShadows; 
  
    private  Quaternion shadowRot;
    private Quaternion currShadowRot;
    float timeLeft;
    private int i;
    private Vector2 shadowVector;
    Color targetColor;
    Color currentColor;
    private Color startColor;
    
    private void Start()
    {
        
        currShadowRot = new Quaternion(0.536f,0.204f,-0.359f,0.735f);
        shadowRot = new Quaternion(0.626f,-0.009f,0.001f,0.779f);
        targetColor = tintColors[0];
        Day = true;

    }



    void FlipShadows()
    {
        for (int i = 0; i < charShadows.Count; i++)
        {

            if (charShadows[i].castVertical)
            {
                charShadows[i].Shadow.GetComponent<SpriteRenderer>().flipX =
                    !charShadows[i].Shadow.GetComponent<SpriteRenderer>().flipX;
                charShadows[i].ShadowVertical.GetComponent<SpriteRenderer>().flipX =
                    !charShadows[i].ShadowVertical.GetComponent<SpriteRenderer>().flipX;
            }
        }

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
                switch (i)
                {
                    case 0:
                    {
                        FlipShadows();
                        currShadowRot = shadowRot;
                        shadowRot = new Quaternion(0.626f,-0.009f,0.001f,0.779f);
                        shadowVector = shadowRot.eulerAngles;
                      
                        Day = true;
                        Stars.SetActive(false);
                        Sun.SetActive(false);
                    }
                        break;

                    case 1:
                    {
                       
                        currShadowRot = shadowRot;
                        shadowRot = new Quaternion(0.536f,-0.204f,0.359f,0.735f);
                        Evening = true;
                    }
                        break;
                
                    case 2:
                    {
                        FlipShadows();
                        currShadowRot = shadowRot;
                        shadowRot = new Quaternion(0.008f,-0.617f,0.786f,0.005f);
            
                        Night = true;
                        Stars.SetActive(true);
                    }
                        break;
                    case 3:
                    {
                      
                        currShadowRot = shadowRot;
                        shadowRot = new Quaternion(0.536f,0.204f,-0.359f,0.735f);
                        Mourning = true;
                        Sun.SetActive(true);
                    }
                        break;
                    default: {i = 0;
                        FlipShadows();
                        currShadowRot = shadowRot;
                        shadowRot = new Quaternion(0.626f,-0.009f,0.001f,0.779f);
                        
                        Sun.SetActive(false);
                        Stars.SetActive(false);
                        Day = true;
                    } break;
                }
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
                Quaternion rotation = Quaternion.Lerp(currShadowRot, shadowRot, timeLeft);
                
                   //if(Night || Mourning)
                   // for (int j = 0; j < Lights.Count; j++)
                //    {
                   //     Lights[j].SetFeathering(currentColor);
                 //   }
                for (int i = 0; i < charShadows.Count; i++)
                {
                  
                        if (!charShadows[i].controlledOuter)
                            charShadows[i].Shadow.transform.localRotation = rotation; // вращение горизонтальной тени
                        if (charShadows[i].castVertical)
                        {
                            /*
                        RaycastHit2D hit = Physics2D.Raycast(charShadows[i].Shadow.transform.position,
                            rotation * new Vector2(0, 1), 10, charShadows[i].shadowCollision, 0, 10);
*/
                            Quaternion rotationReal = charShadows[i].Shadow.transform.rotation;
                            RaycastHit2D hit = Physics2D.Raycast(charShadows[i].Shadow.transform.position,
                                rotationReal * new Vector2(0, 1), 10, charShadows[i].shadowCollision, 0, 10);

                        // UnityEngine.Debug.DrawRay(charShadows[i].Shadow.transform.position,
                        //     rotation * new Vector2(0,
                        //         charShadows[i].ShadowVertical.GetComponent<SpriteRenderer>().size.y *
                        //         charShadows[i].ShadowVertical.transform.localScale.y), Color.cyan, 2);

                        Vector2 maxResVector =  rotationReal * new Vector2(0,
                            charShadows[i].ShadowVertical.GetComponent<SpriteRenderer>().size.y *
                            charShadows[i].ShadowVertical.transform.localScale.y);

                        if (hit.collider != null)
                        {


                            float distance = Vector2.Distance(hit.point, charShadows[i].Shadow.transform.position);

                            if (distance < charShadows[i].maxShadowVerticalDistance)
                            {
                                charShadows[i].ShadowVertical.SetActive(true);
                                float offset = (distance / maxResVector.magnitude);
                                charShadows[i].ShadowVertical.transform.position = new Vector2(hit.point.x,
                                    hit.point.y - offset *
                                    (charShadows[i].ShadowVertical.GetComponent<SpriteRenderer>().size.y *
                                     charShadows[i].ShadowVertical.transform.localScale.y));
                            }
                            else
                            {
                                charShadows[i].ShadowVertical.SetActive(false);
                            }

                        }
                        else
                        {
                            charShadows[i].ShadowVertical.SetActive(false);
                        }
                    }
                }
            }
        }

        else
        {
           
          
            if (Day)
            {
                InstantAdjust(0);
            }
            if (Evening)
            {
                InstantAdjust(1);
            }
            if (Night)
            {
                InstantAdjust(2);
            }
            if (Mourning)
            {
                InstantAdjust(3);
            }
        }
        tintSpiteF.material.color = currentColor;
        tintSpiteB.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a/2);

    }

    private void InstantAdjust(int i)
    {
        currentColor = tintColors[i];
        PPV.profile = PPVPresets[i];
        switch (i)
        {
            case 0:
            {
                FlipShadows();
                currShadowRot = shadowRot;
                shadowRot = new Quaternion(0.626f, -0.009f, 0.001f, 0.779f);
                shadowVector = shadowRot.eulerAngles;
                Stars.SetActive(false);
                Sun.SetActive(false);
            }
                break;

            case 1:
            {

                currShadowRot = shadowRot;
                shadowRot = new Quaternion(0.536f, -0.204f, 0.359f, 0.735f);
            }
                break;

            case 2:
            {
                FlipShadows();
                currShadowRot = shadowRot;
                shadowRot = new Quaternion(0.008f, -0.617f, 0.786f, 0.005f);
                Stars.SetActive(true);
            }
                break;
            case 3:
            {

                currShadowRot = shadowRot;
                shadowRot = new Quaternion(0.536f, 0.204f, -0.359f, 0.735f);
                Sun.SetActive(true);
            }
                break;
            default:
            {
                i = 0;
                FlipShadows();
                currShadowRot = shadowRot;
                shadowRot = new Quaternion(0.626f, -0.009f, 0.001f, 0.779f);
                Sun.SetActive(false);
                Stars.SetActive(false);
            }
                break;
        }
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
