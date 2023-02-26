using System;
using UnityEngine;
using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine.PlayerLoop;


[System.Serializable]

public class ListOfList
{
    public PolyDrawer PD;
    public List<Vector3> VectorList;
    public Sprite shadowSprite;
    public GameObject shadowObj;
    public List<Vector3> StartList;
    public List<Vector3> MoveList;
}
public class ShadowGPT : MonoBehaviour

{
    [SerializeField] private bool Bind;
    private GameObject shadowPrototype ;
   
    private  List<ListOfList> AllCountours = new List<ListOfList>() ;

    [SerializeField] private Material Mat;
    public float Scale;
    public float shiftScale;
    public Vector2 Correction;
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    [SerializeField] private GameObject Light;
    [SerializeField] private  Sprite Sprite;
    [SerializeField] private float Quality = 5;
    [SerializeField] private Transform Sihouette;
    private List<GameObject> Shadows = new List<GameObject>();
    [SerializeField] private LayerMask shadowMask;
    [SerializeField] private float shadowDistance;
    private Sprite lastSprite;
    ListOfList targetCountour = new ListOfList();
    public void SetSprite(Sprite newSprite)
    {
        Sprite = newSprite;
    }
    void Draw(ListOfList target)
    {
        if (target.VectorList.Count > 2) 
        {
            target.PD.RawPoints = target.MoveList; 
            target.PD.UpdateFigure();
        }
    }

    private void FixedUpdate()
    {
        
        if (lastSprite != Sprite)
        {
            bool Found = false;
            foreach (var countour in AllCountours)
            {
                if (countour.shadowSprite == Sprite)
                {
                    targetCountour = countour;
                    Found = true;
                    break;
                }
            }

            if (!Found)
            {

                shadowPrototype = new GameObject();
                List<Vector2> newList = new List<Vector2>();
                for (int i = 0; i < Sprite.GetPhysicsShapeCount(); i++)
                {

                    Sprite.GetPhysicsShape(i, points);
                    if (newList.Count < points.Count)
                    {
                        newList = points;
                        LineUtility.Simplify(points, Quality, simplifiedPoints);
                    }

                }


                if (simplifiedPoints.Count > 2)

                {

                    for (int j = 0; j < simplifiedPoints.Count; j++)
                    {

                        simplifiedPoints[j] = new Vector2((simplifiedPoints[j].x), simplifiedPoints[j].y);

                    }

                    simplifiedPoints.Reverse();

                    AllCountours.Add(new ListOfList());
                    AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));

                    AllCountours.Add(new ListOfList());
                    AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
                    AllCountours.Last().StartList = (Vector2ListToVector3List(simplifiedPoints));
                    AllCountours.Last().MoveList  = (Vector2ListToVector3List(simplifiedPoints));


                    GameObject sp = Instantiate(shadowPrototype, transform);

                    Shadows.Add(sp);
                    AllCountours.Last().shadowObj = sp;
                    AllCountours.Last().shadowSprite = Sprite;

                    PolyDrawer pd = sp.AddComponent<PolyDrawer>() as PolyDrawer;
                    AllCountours.Last().PD = (sp.GetComponent<PolyDrawer>());
                    pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
                    pd.Mat = Mat;
                }

                targetCountour = AllCountours.Last();
            }

            foreach (GameObject shadow in Shadows)
            {
                shadow.SetActive(false);
            }

            targetCountour.shadowObj.SetActive(true);


        }

        if (Bind)
        {   
            foreach (GameObject shadow  in Shadows)
            {
                shadow.transform.position = Sihouette.transform.position;
            }
        }
       
  
            Vector3 lowestPoint = new Vector3(0,999,0);
            for (int j=0; j< targetCountour.VectorList.Count;j++)
            {
                targetCountour.VectorList[j] =  targetCountour.StartList[j] + (Sihouette.position*shiftScale-  targetCountour.VectorList[j]);
                targetCountour.VectorList[j] = new Vector2( targetCountour.VectorList[j].x/Scale+Correction.x,  targetCountour.VectorList[j].y/Scale+Correction.y);
                
                RaycastHit hit;
                Vector3 ray = (ConvertToVector3(targetCountour.VectorList[j]) - Light.transform.position).normalized;
                Debug.DrawRay(Light.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);
                // Does the ray intersect any objects excluding the player layer
                
                if (Physics.Raycast(Light.transform.position, ray, out hit, shadowDistance, shadowMask))
                {
                    if (lowestPoint.y > hit.point.y)
                        lowestPoint = hit.point;
                    targetCountour.MoveList[j] = hit.point;
                    
                    Debug.Log("Did Hit");
                    // GetComponent<PolyDrawer>().AddPoints(Pixels_[i].position);       
                }

                else
                {
                    if (j-1>=0)
                        targetCountour.MoveList[j] =    targetCountour.MoveList[j-1];
                    
                  
                }

                //GetComponent<PolyDrawer>().UpdateFigure();

            }

            for (int j = 0; j < targetCountour.VectorList.Count; j++)
            {
                targetCountour.MoveList[j]  -= lowestPoint;
            }


        
        

        try
        {
            Draw(targetCountour);
        }
        catch
        {
            
        }
        
    
    }

  
   private void Start()

   {


        
       shadowPrototype = new GameObject();
       List<Vector2> newList = new List<Vector2>();
       for (int i = 0; i < Sprite.GetPhysicsShapeCount(); i++)
       {
           
           Sprite.GetPhysicsShape(i, points);
           if (newList.Count < points.Count)
           {
               newList = points;
               LineUtility.Simplify(points, Quality, simplifiedPoints);
           }
            
       }
       
          
       if (simplifiedPoints.Count > 2)
       
       {
           
           
           
           for(int j=0; j<simplifiedPoints.Count; j++)
           {
                   
               simplifiedPoints[j] = new Vector2((simplifiedPoints[j].x), simplifiedPoints[j].y);
              
           }
           simplifiedPoints.Reverse();

           AllCountours.Add(new ListOfList());
           AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
           AllCountours.Last().StartList = (Vector2ListToVector3List(simplifiedPoints));
           AllCountours.Last().MoveList  = (Vector2ListToVector3List(simplifiedPoints));
           
          
           GameObject sp = Instantiate(shadowPrototype, transform);
           Shadows.Add(sp);
           PolyDrawer pd = sp.AddComponent<PolyDrawer>()as PolyDrawer;
           AllCountours.Last().PD = sp.GetComponent<PolyDrawer>();
        
           pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
           pd.Mat = Mat;
           pd.UpdateFigure();
       }

       targetCountour = AllCountours.Last();
              
            
           
          
         
       

       lastSprite = Sprite;

   }

   Vector3 ConvertToVector3(Vector2 v2)
   {
       return new Vector3(v2.x, v2.y, 0);
   }
   public List<Vector3> Vector2ListToVector3List(List<Vector2> vector2List)
   {
       List<Vector3> vector3List = new List<Vector3>();
       foreach (Vector2 vector2 in vector2List)
       {
           vector3List.Add(new Vector3(vector2.x, vector2.y, 0));
       }
       return vector3List;
   }
   public List<Vector3> Vector2ListToVector3List1(List<Vector2> vector2List)
   {
       List<Vector3> vector3List = new List<Vector3>();
       foreach (Vector2 vector2 in vector2List)
       {
           vector3List.Add(new Vector3(vector2.x, vector2.y, 0));
       }
       return vector3List;
   }

       //верхняя слева направо

   
}


