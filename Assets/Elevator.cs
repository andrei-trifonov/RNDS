using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] Stages;
   // [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Menu;
    [SerializeField] private AudioClip Clip;

    private Rigidbody2D Player;
    private AudioSource m_AudioSource;
    private float m_Position_y;
    private float o_Position_y;
    private int State = 0;
    private Animator m_Anim;
    private bool Blocked;
    private Vector3 m_Vector;
    private Rigidbody2D startRigidbody;
    private bool playerEnter;
    private bool goingDown;

    private Canvas menuCanvas;
    // Start is called before the first frame update

    public int GetState()
    {
        return State;
    }
    public bool GetStateB()
    {
        return Blocked;
    }
    private void Start()
    {
       
        Player = GameObject.Find("Player").GetComponentInChildren<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        menuCanvas = Menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;
        m_Anim = gameObject.GetComponent<Animator>();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            playerEnter = true;
            //Player.transform.SetParent(gameObject.transform);
            menuCanvas.enabled = true;
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Player.transform.SetParent(null);
            //Player.transform.parent = null;
            playerEnter = false;
            goingDown = false;
            menuCanvas.enabled = false;
        }
    }

    void Common()
    {
       
        m_AudioSource.Stop();
        m_AudioSource.PlayOneShot(Clip);
       // if (playerEnter)
       //     Player.transform.SetParent(gameObject.transform);
        Blocked = true;
        m_Anim.SetBool("Move", true);
        
    }
    public void Down()
    {
       
        if (State > 0 && !Blocked)
        {
            Common();
            goingDown = true;
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
        if (goingDown)
        {
            Player.AddForce(Vector2.down * 100);
        }
        m_Position_y = gameObject.transform.localPosition.y;
        o_Position_y = Stages[State].transform.localPosition.y;
        if (!(((m_Position_y + 0.05f) > o_Position_y) && ((m_Position_y - 0.05f) < o_Position_y)))
        {
            transform.Translate(m_Vector * Speed / 100, transform.parent);
           
        }
        else
        {
            if (Blocked)
            {
                goingDown = false;
                m_AudioSource.Stop();
                Blocked = false;
              
               
                m_Anim.SetBool("Move", false);
            }
        }

    }
    }

