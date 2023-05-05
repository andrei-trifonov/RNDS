using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBirdsSummon : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       FindObjectOfType<upgradeManager>().Upgrade(4);     
    }
    
}
