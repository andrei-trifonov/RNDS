using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleElevator : MonoBehaviour
{

    [SerializeField] private Animator Block;
    [SerializeField] private GameObject liftPos;
    [SerializeField] private Animator Elevator;
    private int Weight;
    private Vector3 startPos;
    private int newPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = liftPos.transform.position;

    }
    public void AddWeight(int i)
    {
        Weight += i;
        if (Weight <= 5)
        {
            newPos = 0;
        }
        if (Weight > 5 && Weight <= 10)
        {
            newPos = 1;
        }
        if (Weight >  10)
        {
            newPos = 2;
        }
    }

    public Animator GetElevator()
    {
        return Elevator;
    }
    public void RemoveWeight(int i)
    {
        Weight -= i;

        if (Weight <= 5)
        {
            newPos = 0;
        }
        if (Weight > 5 && Weight <= 10)
        {
            newPos = 1;
        }
        if (Weight >  10)
        {
            newPos = 2;
        }
    }

    public void MoveLiftDown()
    {

        
                
          
        

        switch (newPos)
        {
            case 0:
                liftPos.transform.position = new Vector3(startPos.x, startPos.y - 12, startPos.z);
                break;
            case 1:
                liftPos.transform.position = new Vector3(startPos.x, startPos.y - 5, startPos.z);
                break;
            case 2:
                liftPos.transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
                break;
        }

        if (Elevator.GetComponent<Elevator>().GetState() == 0 && Elevator.GetComponent<Elevator>().GetStateB() )
        {
            Block.SetInteger("Pos", 0);
        }
    }

    public void MoveLiftUp()
    {

           

        switch (newPos)
        {
            case 0: liftPos.transform.position = new Vector3(startPos.x, startPos.y - 12, startPos.z);
                break;
            case 1: liftPos.transform.position = new Vector3(startPos.x, startPos.y - 5, startPos.z);
                break;
            case 2: liftPos.transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
                break;
        }
        if (Elevator.GetComponent<Elevator>().GetState() == 1  )
        {
    
            switch (newPos)
            {
                case 0:  Block.SetInteger("Pos", 1);
                    break;
                case 1:  Block.SetInteger("Pos", 2);
                    break;
                case 2: Block.SetInteger("Pos", 2);
                    break;
            }
        }
        

    }








    // Update is called once per frame
    
}
