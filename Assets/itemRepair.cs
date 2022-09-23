using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemRepair : MonoBehaviour
{
    // Start is called before the first frame update
   private bool Repaired;

   public bool GetRepaired()
   {
       return Repaired;
   }
   public void SetRepaired(bool state)
   { 
       Repaired = state;
   }
}
