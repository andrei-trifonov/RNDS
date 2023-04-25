using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerStopZone : MonoBehaviour
{
    private ShipMove SM;
    [SerializeField] private bool Fix;
    [SerializeField] private GameObject Blocker;
    void OnTriggerEnter2D (Collider2D other){
        
        if (other.CompareTag("Machine"))
            {
            SM = other.GetComponent<ShipMove>();
            SM.SetEngineStarted(false);
            SM.SetMachineBlocked(true);
            if (Fix)
                Blocker.SetActive(true);
        }
    
    }

    public void UnblockMachine()
    {
      
        SM.SetMachineBlocked(false);
        Destroy(gameObject);
    }
}
