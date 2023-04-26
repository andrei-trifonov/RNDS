using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityStandardAssets.CrossPlatformInput;



    public class Platformer2DUserControl : MonoBehaviour
    {
        
        [SerializeField] private GameObject OneStickMovement;
       
        [SerializeField] private float latency;
        [SerializeField] private GameObject Beacon;
        [SerializeField] private float maxDistance;
        public bool useBeacon;
        private bool crouch;
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private Vector2 Delta;
        private float h;
        private bool click;
        private float hTranslate;
        private Vector2 fp; // first finger position
        private Vector2 lp; // l
        private float j_level;
    private int controls;
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        controls = PlayerPrefs.GetInt("Controls");
        switch (controls)
        {
          
            case 1: OneStickMovement.SetActive(true); break;
        }
        }


        private void Update()
        {
            if (!m_Jump)
            {
            // Read the jump input in Update so button presses aren't missed.
            // 


            if (controls == 0) {
                if (j_level > 0.5f)
                    m_Jump = true;

                else
                    m_Jump = false;
            }

            if (controls == 1 )
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            

        }



    }

        private void ChooseDirection()
        {
        if (controls == 1)
            hTranslate = 0;
           
            if ( h<-0.5f)
            {
                hTranslate = -1f;
            }
            if ( h>0.5f)
            {
                hTranslate = 1f;
            }
        if (controls == 0)
        {
            if (Input.touches.Length != 1 && !click)
                hTranslate = 0;
        }

    }

    private void FixedUpdate()
        {
            // Read the inputs.

            if (Input.GetMouseButtonDown(0))
            {
                click = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                click = false;
            }
        if (controls == 1)
             h = CrossPlatformInputManager.GetAxis("Horizontal");


        if (controls == 0)
        {
            if (Input.touches.Length == 1)
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                        fp = touch.position;
                        lp = touch.position;
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        lp = touch.position;
                        if ((fp.x - lp.x) > latency) // left swipe
                        {
                            h = -1;
                        }
                        else if ((fp.x - lp.x) < -latency) // right swipe
                        {
                            h = 1;
                        }
                        if ((fp.y - lp.y) < -latency) // up swipe
                        {
                            j_level = 1;
                        }
                        else if ((fp.y - lp.y) > latency) // down swipe
                        {
                            j_level = -1;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        h = 0;
                        j_level = 0;

                    }

                }
            else
            {
                h = 0;
                j_level = 0;
            }
        }
        if (useBeacon)
            {
                Delta = gameObject.transform.position - Beacon.transform.position; 
                if (Delta.magnitude > maxDistance && h!=0)
                {
                    if (Delta.x > 0)
                        if (h < 0)
                            ChooseDirection();
                        else 
                            hTranslate =0;
                    else
                        if (h > 0)
                            ChooseDirection();
                        else
                            hTranslate=0;
                }
                else
                {
                   ChooseDirection();
                }
                
            }
            else
            {
                ChooseDirection();
            }
            

            
                // Pass all parameters to the character control script.
            m_Character.Move(hTranslate, crouch, m_Jump);
            m_Jump = false;


        }
    }

