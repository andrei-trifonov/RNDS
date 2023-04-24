using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerStopZone : MonoBehaviour
{
    private ShipMove SM;
    void OnTriggerEnter2D (Collider2D other){
        
        if (other.CompareTag("Machine"))
        {
            SM = other.GetComponent<ShipMove>();
            SM.SetEngineStarted(false);
            SM.SetMachineBlocked(true);
        }
    
    }

    public void UnblockMachine()
    {
        SM.SetMachineBlocked(false);
        Destroy(gameObject);
    }
}
