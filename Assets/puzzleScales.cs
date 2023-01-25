using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleScales : MonoBehaviour
{
    [SerializeField] private int summaryWeight;
   public List<itemWeight> Items;
    [SerializeField] private MonoBehaviour Manager;

    public int GetWeight()
    {
        return summaryWeight;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Item"))
        {
            var component = other.transform.GetChild(0).GetComponent<itemWeight>();
            if (component && !Items.Contains(component))
            {
                Items.Add(component);
                summaryWeight += component.GetWeight();
                Manager.Invoke("CheckWeight",0);
            }
        }
     
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        var component = other.transform.GetChild(0).GetComponent<itemWeight>();
        if (Items.Contains(component))
        {
            Items.Remove(component);
            summaryWeight -= component.GetWeight();
            Manager.Invoke("CheckWeight",0);
        }
        
    }

}
