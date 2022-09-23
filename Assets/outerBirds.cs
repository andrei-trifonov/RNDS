using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class outerBirds : MonoBehaviour
{
    public GameObject Leader;
    
    public float renewTime;
    public float randomRadius;
    public float Force;
    public float normalDistance;

    private float idleAnimTimer;
    private float idleAnimGoal;
    private Vector3 Objective;
    private Vector3 Offset;
    private float Timer;
    private Animator m_Anim;
    private GameObject usingTarget;
    private bool Flipped;
    private float usingDistance;
    private bool isSitting;
    private bool isLanding;
    // Start is called before the first frame update
    void Start()
    {
        usingTarget = Leader;
        usingDistance = normalDistance;
        m_Anim = gameObject.GetComponent<Animator>();
    }

    public void OnEnable()
    {
        Timer = 0;
        idleAnimGoal = 0;
        idleAnimGoal = Random.Range(3.0f, 10.0f);
        StartCoroutine(StartFlyAnimatorCoroutine(Random.Range(0.0f, 2.0f)));
    }
    
    public void SetNewTarget(GameObject Target)
    {
        isLanding = true;
        usingTarget = Target;
        usingDistance = 0.2f;
    }
    IEnumerator StartFlyAnimatorCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (m_Anim != null)
        {
            m_Anim.SetBool("TakeOff", true);
            m_Anim.SetBool("Switch", false);
        }
    }
    

    IEnumerator SwitchCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (m_Anim != null)
        {
            m_Anim.SetBool("Switch", false);
        }
    }
    public void ResetTarget()
    {
        isLanding = false;
        isSitting = false;
        if (m_Anim != null)
        {
            m_Anim.SetBool("TakeOff", true);
            m_Anim.SetBool("Switch", false);
        }

        usingTarget = Leader;
        usingDistance = normalDistance;
    }

    void Flip()
    {
        Flipped = !Flipped;
        gameObject.GetComponent<SpriteRenderer>().flipX = Flipped; 
    }
    void FixedUpdate()
    {
        if (isSitting)
        {
            if (idleAnimTimer < idleAnimGoal)
            {
                idleAnimTimer += Time.deltaTime;
            }
            else
            {
                idleAnimTimer = 0;
                idleAnimGoal = Random.Range(3.0f, 10.0f);
                if (m_Anim != null)  
                    m_Anim.SetBool("Switch", true);
                StartCoroutine(SwitchCoroutine());
            }
        }
        else
        {


            if ((usingTarget.transform.position.x - gameObject.transform.position.x < 0) && Flipped)
            {
                Flip();
            }

            if ((usingTarget.transform.position.x - gameObject.transform.position.x > 0) && !Flipped)
            {
                Flip();
            }


        }
        if ((usingTarget.transform.position - gameObject.transform.position).magnitude > usingDistance)
        {
            if (Timer < renewTime)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                Timer = 0;
                if (usingTarget == Leader)
                {
                    Offset = new Vector3(
                        Random.Range(-randomRadius,
                            randomRadius),
                        Random.Range(-randomRadius,
                            randomRadius));
                }
                else
                {
                    Offset = Vector3.zero;
                }
            }

            Objective = usingTarget.transform.position + Offset;
            gameObject.transform.position += (Objective - transform.position) /
                (Objective - transform.position).magnitude * (Force / 100);
        }
        else
        {
            if (!isSitting && m_Anim != null && isLanding)
            {
                m_Anim.SetBool("TakeOff", false);
                m_Anim.SetBool("Switch", false);
                isSitting = true;
            }
        }
    }
}
