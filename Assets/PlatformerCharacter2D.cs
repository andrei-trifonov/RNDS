using System;
using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private float rotDelay;
       // [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;      // A mask determining what is ground to the character
        [SerializeField] private GameObject IZpoint;         
        [SerializeField] private GameObject Pivot;
        [SerializeField] private GameObject dustEffect;
        [SerializeField] private Rigidbody2D Empty;
        [SerializeField] private Animator m_Anim;            // Reference to the player's animator component.
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip landSound;
        [SerializeField] private Transform shadowPos;
        [SerializeField] private Transform VshadowPos;
        [SerializeField] private Transform shadowPoint;
       
        private bool jumpBlocked;
        private AudioSource m_AudioSource;
        private bool Blocked;
        private Transform targetTransform;
        private bool AutoBlocked;
        private float Distance;
        private Vector2 o_Velocity;
        private Vector2 targetPosition;
        private Vector2 currentPosition;
        private Vector2 velocity;
        private Animator effectAnimator;
        private bool autoSequenceStarted;
        private int workType;
        private float Difference;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool renewEffect = false;
        private Rigidbody2D movingObject = new Rigidbody2D() ;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private float startPos;
        private Animator shadowAnim;
        public Animator ShareAnimator()
        {
            return m_Anim;
        }

        public void Block()
        {
            Blocked = true;
            m_Anim.SetFloat("Speed", 0);
            m_Anim.SetFloat("vSpeed", 0);
          
        }

        public void UnBlock()
        {
            Blocked = false;
        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Moving"))
            {
                movingObject = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                movingObject = Empty;
            }
           
        }


        private void Awake()
        {
           
            effectAnimator = dustEffect.GetComponent<Animator>();
            m_AudioSource = gameObject.GetComponent<AudioSource>();
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            shadowAnim = shadowPos.GetComponent<Animator>();
        }


        private void FixedUpdate()
        {
            if(targetTransform)
                targetPosition = targetTransform.position; //Определить позицию
            if (movingObject)
                o_Velocity = new Vector2(movingObject.velocity.x, 0);
             
            currentPosition = IZpoint.transform.position;
            Difference = (targetPosition.x - currentPosition.x);
            Distance = Mathf.Abs(Difference); //Определить сторону движения и дистанцию до цели
            if (autoSequenceStarted)
            {
                if (Difference > 0)
                {
                    if (!m_FacingRight)
                    {
                        // ... flip the player.


                        Flip(true);
                    }

                    velocity = new Vector2(5f + o_Velocity.x, 0); //движение вправо, если цель справа
                }
                else
                {
                    if (m_FacingRight)
                    {
                        // ... flip the player.


                        Flip(true);
                    }

                    velocity = new Vector2(-5f + o_Velocity.x, 0); //движение влево, если цель слева
                }

                if (Distance > 0.5f &&
                    !AutoBlocked) //Когда до цели далеко и игрок не поворачивается автоматически в Sequence 
                {
                    m_Anim.SetFloat("Speed", 5f); //Вкючить анимацию ходьбы
                    m_Rigidbody2D.velocity = velocity; //Придать скорость 
                }
                else
                {
                    if (!(Distance > 0.5f)) //Когда игрок приблизился на расстояние меньше 0.5 .юнита до цели
                    {
                        autoSequenceStarted = false; //Закончить Sequence
                        AutoWork(); //Перейти к занятию
                    }
                }

            }


            m_Grounded = false;
            
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (renewEffect == false)
                    {
                        m_AudioSource.PlayOneShot(landSound);
                        dustEffect.transform.position = m_GroundCheck.transform.position;
                        StartCoroutine(DustCoroutine());
                        renewEffect = true;
                    }

                }
            }
            
            m_Anim.SetBool("Ground", m_Grounded);
            if (m_Grounded == false)
            {
                
                renewEffect = false;
                float deltaY = Mathf.Abs(gameObject.transform.position.y - startPos);
                deltaY = Mathf.Clamp(deltaY, 0.3f, 0.6f);
                // Set the vertical animation
                m_Anim.SetFloat("vSpeed", Mathf.Abs(deltaY));
                
            }
            
        }

        IEnumerator DustCoroutine()
        {
            effectAnimator.SetBool("Ground", true);
            yield return new WaitForSeconds(0.5f);
            effectAnimator.SetBool("Ground", false);
        }



        void AutoWork()
        {
            targetTransform.parent.GetComponent<ActionZone>().StartBar(); //Запустить круговой StatusBar
            m_Anim.SetFloat("Speed", 0f); //Остановить анимацию движения
            m_Rigidbody2D.velocity = new Vector2(0, 0); //Останосить игрока
            switch (workType) //Выбор анимации в зависимости от занятия
            {
                case 1:
                    m_Anim.SetBool("Rotor", true);
                   
                    break;
                case 2:
                    m_Anim.SetBool("Rope", true);
                 
                    break;
                case 3:
                    m_Anim.SetBool("Button", true);
                  
                    break;
                case 4:
                    m_Anim.SetBool("Punch", true);
                                  
                    break;
            }
        }
        
        void AutoMove()
        {
            
            
            autoSequenceStarted = true; //Переменная отвечающая за Sequence движение в FixedUpdate
            
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (!Blocked){
            // If crouching, check to see if the character can stand up
                if (!crouch && m_Anim.GetBool("Crouch"))
                {
                    // If the character has a ceiling preventing them from standing up, keep them crouching
                    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                    {
                        crouch = true;
                    }
                }

                // Set whether or not the character is crouching in the animator
                m_Anim.SetBool("Crouch", crouch);

                //only control the player if grounded or airControl is turned on
                if (m_Grounded || m_AirControl)
                {
                     
                     m_Anim.SetFloat("vSpeed", 0f);
                    // Reduce the speed if crouching by the crouchSpeed multiplier
                    //move = (crouch ? move*m_CrouchSpeed : move);
                    // The Speed animator parameter is set to the absolute value of the horizontal input.
                    m_Anim.SetFloat("Speed", Mathf.Abs(move));

                    // Move the character
                    m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed + o_Velocity.x, m_Rigidbody2D.velocity.y+ o_Velocity.y) ;

                    // If the input is moving the player right and the player is facing left...
                    if (move > 0 && !m_FacingRight )
                    {
                        // ... flip the player.
                        Flip(false);
                    }
                        // Otherwise if the input is moving the player left and the player is facing right...
                    else if (move < 0 && m_FacingRight  )
                    {
                        // ... flip the player.
                        Flip(false);
                    }
                } 
                RaycastHit2D hit = Physics2D.Raycast(shadowPoint.transform.position,  -Vector2.up, 20, m_WhatIsGround, 0, 10 );
                if (hit.collider!= null)
                {
                  
                    shadowPos.transform.position = 
                        new Vector2(shadowPoint.transform.position.x, hit.point.y);
                }
                // If the player should jump...
            if (m_Grounded && jump && !jumpBlocked  && m_Anim.GetBool("Ground"))
            {

                StartCoroutine(JumpCoroutine());
                m_AudioSource.PlayOneShot(jumpSound);
                startPos = gameObject.transform.position.y;
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f , m_JumpForce));
            }
            }
        }

        IEnumerator JumpCoroutine()
        {
            jumpBlocked = true;
            yield return new WaitForSeconds(0.5f);
            jumpBlocked = false;
        }
       IEnumerator FlipCoroutine(bool auto){
            AutoBlocked = true;
            Blocked = true;
            m_Rigidbody2D.velocity = new Vector2(0.0f + o_Velocity.x, 0.0f);
            m_Anim.SetFloat("Speed", 0f);    
            yield return new WaitForSeconds(rotDelay);
            shadowPos.GetComponent<SpriteRenderer>().flipX = !shadowPos.GetComponent<SpriteRenderer>().flipX;
            VshadowPos.GetComponent<SpriteRenderer>().flipX = !VshadowPos.GetComponent<SpriteRenderer>().flipX;
            Vector3 theScale = Pivot.transform.localScale;
            theScale.x *= -1;
            Pivot.transform.localScale = theScale;
            m_Anim.SetBool("Switch", false);
            
            AutoBlocked = false;
            if (!auto)
            {

                Blocked = false;
            }
       }



       public void StartWorking(int type, GameObject Target) //Получен сигнал о начале занятия 
       {
           targetTransform = Target.transform; //Данные о месте нахождения занятия
           workType = type; //Данные о типе занятия
           if (m_Grounded)
           {
               Blocked = true; //Блокировать управление
               AutoMove(); //Запустить движение к цели
           }

       }

       public void StopWorking()
       {
           m_Anim.SetBool("Rotor", false);
           m_Anim.SetBool("Button", false);
           m_Anim.SetBool("Rope", false);
           m_Anim.SetBool("Punch", false);
           autoSequenceStarted = false;
           m_Anim.SetFloat("Speed", 0f);
           Blocked = false;
       }
       private void Flip(bool auto)
        {

            m_FacingRight = !m_FacingRight;
           
            m_Anim.SetBool("Switch", true);
            
            StartCoroutine (FlipCoroutine(auto));
            // Multiply the player's x local scale by -1.

        }

}
