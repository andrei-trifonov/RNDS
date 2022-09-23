using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityStandardAssets.CrossPlatformInput;



    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField] private GameObject Beacon;
        [SerializeField] private float maxDistance;
        public bool useBeacon;
        private bool crouch;
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private Vector2 Delta;
        private float h;
        private float hTranslate;
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }



        }

        private void ChooseDirection()
        {
            if (Mathf.Abs(h) < 0.5f)
                hTranslate = 0;
            if ( h<-0.5f)
            {
                hTranslate = -1f;
            }
            if ( h>0.5f)
            {
                hTranslate = 1f;
            }  
        }

        private void FixedUpdate()
        {
            // Read the inputs.


            h = CrossPlatformInputManager.GetAxis("Horizontal");
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

