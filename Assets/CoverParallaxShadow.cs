using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoverParallaxShadow : MonoBehaviour
{
    [SerializeField] private GameObject Light;
    [SerializeField] private GameObject opticCenter;
    [SerializeField] private float symmetricBounds;
    [SerializeField] private float xMaxOffset;
    private Vector3 startPosition;
   
    private float xOffset;
    private float yOffset;
    private float Percentage;
    private float xtranslitedOffset;
    private float ytranslitedOffset;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
                        
        startPosition = transform.position;
        
        
        
       
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       
            var camPos = Light.transform.position;
            var centerPos = opticCenter.transform.position;
            xOffset = camPos.x - centerPos.x;
            xtranslitedOffset = xMaxOffset * Mathf.Clamp(xOffset / symmetricBounds, -1, 1) * (-1);
            var position = transform.position;
            position = new Vector3(transform.parent.position.x  + xtranslitedOffset, transform.position.y, transform.position.z);
            transform.position = position;
                
         
        }
    
}
