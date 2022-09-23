using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

public class CarryManager : MonoBehaviour
    {
        [SerializeField] private AudioClip pickSound;
        [SerializeField] private AudioClip throwSound;
        [SerializeField] private Canvas Menu;
        [SerializeField] private GameObject itemPivot;
        [SerializeField] private float Speed;
        [SerializeField] private GameObject magnetEffect;
        private AudioSource m_AudioSource;
        private bool Picked = false;
        private bool Busy = false;
        private GameObject pickedItem;
        
        private Vector3 targetPosition;
        private Vector3 currentPosition;
        private Vector3 directionOfTravel;
        
        private Animator m_Anim;
        [HideInInspector] public GameObject Item;
        // Start is called before the first frame update

        public bool isPicked()
        {
            return Busy;
        }


        public GameObject GetPickedItem()
        {
            return pickedItem;
        }

        void Start()
        {
            Menu.enabled = false;
            m_AudioSource = gameObject.GetComponent<AudioSource>();
            m_Anim = gameObject.GetComponent<PlatformerCharacter2D>().ShareAnimator();
        }
        public void ThrowItem()
        {
            if (pickedItem != null)
            {
                Busy = false;
                Picked = false;
                m_AudioSource.PlayOneShot(throwSound);
                Menu.enabled = false;
                magnetEffect.SetActive(false);
                pickedItem.GetComponentInChildren<MagneticItem>().StopPick();
                
                   
                m_Anim.SetBool("Loaded", false);
            }
            
        }

      


        public void PickItem(GameObject Item)
        {
            if (!Picked && !Busy)
            {
                Busy = true;
                Picked = true;
                this.Item = Item;
                Item.GetComponentInChildren<MagneticItem>().HideMenu();
                m_AudioSource.PlayOneShot(pickSound);
                Menu.enabled = true;
                m_Anim.SetBool("Loaded", true);
            }
        }

        private void FixedUpdate()
        {
            // Для подвеса скорость нужна ниже (для визуала)
            if (Picked) {

                targetPosition = itemPivot.transform.position;
                currentPosition = Item.transform.position;


                if(Vector3.Distance(currentPosition, targetPosition) > .1f) {
                    directionOfTravel = targetPosition - currentPosition;
                    //now normalize the direction, since we only want the direction information
                    directionOfTravel.Normalize();
                    //scale the movement on each axis by the directionOfTravel vector components
                }
                else {
     
                    Item.transform.SetParent(itemPivot.transform);
                    Item.GetComponentInChildren<MagneticItem>().Connect();
                    pickedItem = Item;
                    magnetEffect.SetActive(true);
                    Picked = false;
                }
            
                Item.transform.Translate(
                    (directionOfTravel.x * Speed * Time.deltaTime),
                    (directionOfTravel.y * Speed * Time.deltaTime),
                    (directionOfTravel.z * Speed * Time.deltaTime),
                    Space.World);

            }
        }
    }
