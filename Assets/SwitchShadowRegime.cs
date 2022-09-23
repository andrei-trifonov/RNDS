using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShadowRegime : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Shadow;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Shadow.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    
        if(other.CompareTag("Player"))
        {
          
            Shadow.maskInteraction = SpriteMaskInteraction.None;
        }
    }
}
