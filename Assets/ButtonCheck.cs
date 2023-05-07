using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCheck : MonoBehaviour
{
   
    [SerializeField] private Image targetGraphic;
    public void Check()
    {
   
            targetGraphic.color = new Color(0.6f, 0.6f, 0.6f);

      
   
        
    }

    public void UnCheck()
    {
     

        targetGraphic.color = new Color(1, 1, 1);

    }
}
