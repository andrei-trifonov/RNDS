using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticItem : MonoBehaviour
{
   
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private GameObject Menu;
    [SerializeField] private bool Stashable;
    [SerializeField] private bool turnRight = false;
    private bool Picked;
    private Collider2D PlayerCollider;
    private GameObject m_Parent;
    private Rigidbody2D m_Rigidbody;
    private Collider2D m_Collider;
    private AudioSource m_AudioSource;
    private CarryManager o_CarryManager;
    

    // Start is called before the first frame update
    public bool GetPicked()
    {
        return Picked;
    }
    public bool GetStashable()
    {
        return Stashable;
    }
    public void PlaySound()
    {
        m_AudioSource.PlayOneShot(fallSound);
    }

    public void HideMenu()
    {
        Menu.SetActive (false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
             
        if (other.CompareTag("Player"))
        {
            if (PlayerCollider == null)
            {
                PlayerCollider = other;
                o_CarryManager = PlayerCollider.GetComponent<CarryManager>();
            }



        }
    }



    public void Connect()
    {
        if (turnRight)
        {
            m_Parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        m_Rigidbody.simulated = false;
        m_Collider.enabled= false;
        Picked = false;
    }
    private void Awake()
    {
        m_Parent = gameObject.transform.parent.gameObject;
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_Rigidbody = m_Parent.GetComponent<Rigidbody2D>();
        m_Collider = m_Parent.GetComponent<Collider2D>();
    }

    public void StartPick(){
        Picked = true; 
        o_CarryManager.PickItem(gameObject.transform.parent.gameObject);
    }

    public void SetCarryManager(CarryManager o_CarryManager)
    {
        this.o_CarryManager = o_CarryManager;
    }

  
    

    public void StopPick()
    {
        Picked = false;
        Menu.SetActive (true);
        m_Rigidbody.simulated = true;
        m_Collider.enabled= true;
        m_Parent.transform.parent = null;
    }


    //void FixedUpdate()
   // {
        //  // Для подвеса скорость нужна ниже (для визуала)
        // if (Picked) {
        //
        //      targetPosition = Target.transform.position;
        //      currentPosition = m_Parent.transform.position;
        //
        //
        //     if(Vector3.Distance(currentPosition, targetPosition) > .1f) {
        //         directionOfTravel = targetPosition - currentPosition;
        //         //now normalize the direction, since we only want the direction information
        //         directionOfTravel.Normalize();
        //         //scale the movement on each axis by the directionOfTravel vector components
        //         }
        //      else {
        //
        //             m_Parent.transform.SetParent(Target.transform);
        //             
        //             m_Rigidbody.simulated = false;
        //             m_Collider.enabled= false;
        //             Picked = false;
        //
        //             if (toPlayer)
        //             {
        //                 
        //                 Target.GetComponentInParent<CarryManager>().SetPickedItem(m_Parent);
        //                 magnetEffect.SetActive(true);
        //             }
        //
        //
        //
        //
        //     }
        //      m_Parent.transform.Translate(
        //                     (directionOfTravel.x * speed * Time.deltaTime),
        //                     (directionOfTravel.y * speed * Time.deltaTime),
        //                     (directionOfTravel.z * speed * Time.deltaTime),
        //                     Space.World);
        //
        //     }
   // }
   

}
