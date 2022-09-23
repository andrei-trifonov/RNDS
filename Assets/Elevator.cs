using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private bool isMovingParent;
    [SerializeField] private Rigidbody2D parentRigidbody;
    [SerializeField] private Rigidbody2D machineRigidbody;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] Stages;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Menu;
    [SerializeField] private AudioClip Clip;
    
    private AudioSource m_AudioSource;
    private float m_Position_y;
    private float o_Position_y;
    private int State = 0;
    private Animator m_Anim;
    private bool Blocked;
    private Vector3 m_Vector;
    private Rigidbody2D startRigidbody;
    private bool playerEnter;
    // Start is called before the first frame update


    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        Menu.GetComponent<Canvas>().enabled = false;
        m_Anim = gameObject.GetComponent<Animator>();
        if (isMovingParent)
           startRigidbody = parentRigidbody;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            playerEnter = true;
            Player.transform.SetParent(gameObject.transform);
            Menu.GetComponent<Canvas>().enabled = true;
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.transform.SetParent(null);
            Player.transform.parent = null;
            playerEnter = false;
            Menu.GetComponent<Canvas>().enabled = false;
        }
    }

    void Common()
    {
       
        m_AudioSource.Stop();
        m_AudioSource.PlayOneShot(Clip);
        if (playerEnter)
            Player.transform.SetParent(gameObject.transform);
        Blocked = true;
        m_Anim.SetBool("Move", true);
        
    }
    public void Down()
    {
        
        if (State > 0 && !Blocked)
        {
            Common();
          
            State -= 1;
            m_Vector = Vector3.down;
           
        }
    }
    public void GetHere(int Level)
    {
        
        if (Level >= 0 &&  Level < Stages.Length && !Blocked)
        {
            Common();
            if (Level < State)
                m_Vector = Vector3.down;
            else
            {
                m_Vector = Vector3.up;
            }
            State = Level;
        }
    }
    public void Up()
    {
       
        if (State < Stages.Length-1 && !Blocked)
        {
           
            Common();
            
            State += 1;
            m_Vector = Vector3.up;
           
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMovingParent)
        {
            if (playerEnter)
                parentRigidbody.velocity = machineRigidbody.velocity;
            else
                parentRigidbody.velocity = startRigidbody.velocity;
        }
        m_Position_y = gameObject.transform.position.y;
        o_Position_y = Stages[State].transform.position.y;
        if (!(((m_Position_y + 0.05f) > o_Position_y) && ((m_Position_y - 0.05f) < o_Position_y)))
        {
            transform.Translate(m_Vector * Speed / 100, transform.parent);
           
        }
        else
        {
            if (Blocked)
            {
                m_AudioSource.Stop();
                Blocked = false;
              
               
                m_Anim.SetBool("Move", false);
            }
        }

    }
    }

