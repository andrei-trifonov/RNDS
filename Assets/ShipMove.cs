using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class ShipMove : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody;

    [SerializeField] private Animator engineAnimator;
    [SerializeField] private Animator pipeAnimator;
    [SerializeField] private float Speed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float Fuel = 100;
    [SerializeField] private float Water = 100;
    [SerializeField] private float maxFuel;
    [SerializeField] private float maxWater;
    [SerializeField] private AudioSource pipeAudioSource ;
    [SerializeField] private AudioClip startEngineSound;
    [SerializeField] private AudioClip stopEngineSound;
    [SerializeField] private AudioSource engineAudioSource ;
    [SerializeField] private Slider fuelBar;
    [SerializeField] private Slider waterBar;
    private bool engineStarted;
    private bool pipeBroken;

    private bool Blocked;
    // Start is called before the first frame update
    public bool GetEngineState()
    {
        return engineStarted;
    }

    public void SetWater(float amount)
    {
        if (Water< maxWater)
            Water += amount;
    }

    public void SetMachineBlocked(bool state)
    {
        Blocked = state;
    }
    public void SetPipeState(bool state)
    {
        pipeBroken = state;
    }
    public void SetEngineStarted(bool state)
    {
        if (state)
        {
            StartCoroutine(StartFeedbackCoroutine());
            engineAnimator.enabled = true;
            pipeAudioSource.PlayOneShot(startEngineSound);
            engineAudioSource.enabled = true;
        }
        else
        {
            StartCoroutine(StopFeedbackCoroutine());
            engineAnimator.enabled = false;
            pipeAudioSource.PlayOneShot(stopEngineSound);
            engineAudioSource.enabled = false;
        }


        engineStarted = state;
    }

    public void AddFuel(float eff)
    {
        if (Fuel < maxFuel)
            Fuel += eff;
    }

    void Start()
    {
    
        engineAnimator.enabled = false;
        m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    
    
    public  void FireFeedbackCoroutine()
    {
      
        m_Rigidbody.AddForce(new Vector2(-10 *Speed, -0.5f));
       
       
    }
    IEnumerator StopFeedbackCoroutine()
    {
        pipeAnimator.SetBool("StartSmoke" , false);
        m_Rigidbody.AddForce(new Vector2(-1f * 4f *Speed, -0.5f));
        yield return new WaitForSeconds(0.5f);
        m_Rigidbody.AddForce(new Vector2(1f * 5f *Speed, -0.5f));
        yield return new WaitForSeconds(3f);
       
    }
    IEnumerator StartFeedbackCoroutine()
    {
        pipeAnimator.SetBool("StartSmoke", true);
        m_Rigidbody.AddForce(new Vector2( 2f *Speed, -0.5f));
        yield return new WaitForSeconds(3f);
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (pipeBroken && Fuel > 0.25f)
        {
            Fuel -= 0.25f;
        }

        fuelBar.value = Fuel / maxFuel;
        waterBar.value = Water / maxWater;
        if (Fuel >= 0.1f && Water >= 0.1f && engineStarted  && !Blocked)
        {
            Water -= 0.1f;
            Fuel -= 0.1f;
            

            if (m_Rigidbody.velocity.magnitude >= maxVelocity)
                m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * maxVelocity;
            else
                m_Rigidbody.AddForce(new Vector2(1f * Speed, 300) );
        }

     
        else
        {
            if (engineStarted)
            {
                SetEngineStarted(false);
            }
        }
    }
}
