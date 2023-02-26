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
/*
public class shadowDot
    {   
     
    public Vector3 coordinates;
    public Vector3 position;
    public bool needRender;
    public int i;
    
    public shadowDot(Vector3 coordinates) {
        this.position = Vector3.zero;
        this.coordinates = coordinates;
        needRender = false;
    }


}
*/
public class ListOfList
{
    public List<Vector3> VectorList;
}
public class ShadowGPT : MonoBehaviour

{
    [SerializeField] private bool Bind;
    private GameObject shadowPrototype ;
    private List<ListOfList> StartCoutours = new List<ListOfList>();
    private  List<ListOfList> AllCountours = new List<ListOfList>() ;
    private List<ListOfList> MoveCountours = new List<ListOfList>();
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
    private List<PolyDrawer> PDList = new List<PolyDrawer>();
    private Sprite lastSprite;

    public void SetSprite(Sprite newSprite)
    {
        Sprite = newSprite;
    }
    void Draw()
    {
        for (int i = 0; i < AllCountours.Count; i++)
        {

            if (AllCountours[i].VectorList.Count > 2)
            {

        
                    
                    PDList[i].RawPoints = MoveCountours[i].VectorList;
                    PDList[i].UpdateFigure();
               
            }

            
        }
    }

    private void FixedUpdate()
    {
        if (lastSprite != Sprite)
        {
            StartCoutours.Clear();
            AllCountours.Clear();
            MoveCountours.Clear();
            points.Clear();
            simplifiedPoints.Clear();
            PDList.Clear();
            lastSprite = Sprite;

            foreach (GameObject shadow  in Shadows)
            {
                Destroy(shadow);
            }
            Shadows.Clear();
            
            for (int i = 0; i < Sprite.GetPhysicsShapeCount(); i++)
            {
                Sprite.GetPhysicsShape(i, points);
                LineUtility.Simplify(points, Quality, simplifiedPoints);
                if (simplifiedPoints.Count > 2)
           
                {
               
                    StartCoutours.Add(new ListOfList());
                    StartCoutours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
               
                    for(int j=0; j<simplifiedPoints.Count; j++)
                    {
                       
                        simplifiedPoints[j] = new Vector2((simplifiedPoints[j].x), simplifiedPoints[j].y);
                       
                    }
                    simplifiedPoints.Reverse();

                    AllCountours.Add(new ListOfList());
                    AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
                    MoveCountours.Add(new ListOfList());
                    MoveCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
                   
                    GameObject sp = Instantiate(shadowPrototype, transform);
                    Shadows.Add(sp);
                    PolyDrawer pd = sp.AddComponent<PolyDrawer>()as PolyDrawer;
                    PDList.Add(sp.GetComponent<PolyDrawer>());
                    pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
                    pd.Mat = Mat;
                    pd.UpdateFigure();
                }
              
              
            
           
          
         
            }

            lastSprite = Sprite;

            
        }
        if (Bind)
        {   
            foreach (GameObject shadow  in Shadows)
            {
                shadow.transform.position = Sihouette.transform.position;
            }
        }
       
        for (int i = 0; i < AllCountours.Count; i++)
        {
            Vector3 lowestPoint = new Vector3(0,999,0);
            for (int j=0; j< AllCountours[i].VectorList.Count;j++)
            {
                AllCountours[i].VectorList[j] =  StartCoutours[i].VectorList[j] + (Sihouette.position*shiftScale-  AllCountours[i].VectorList[j]);
                AllCountours[i].VectorList[j] = new Vector2( AllCountours[i].VectorList[j].x/Scale+Correction.x,  AllCountours[i].VectorList[j].y/Scale+Correction.y);
                
                RaycastHit hit;
                Vector3 ray = (ConvertToVector3(AllCountours[i].VectorList[j]) - Light.transform.position).normalized;
                Debug.DrawRay(Light.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);
                // Does the ray intersect any objects excluding the player layer
                
                if (Physics.Raycast(Light.transform.position, ray, out hit, shadowDistance, shadowMask))
                {
                    if (lowestPoint.y > hit.point.y)
                        lowestPoint = hit.point;
                    MoveCountours[i].VectorList[j] = hit.point;
                    
                    Debug.Log("Did Hit");
                    // GetComponent<PolyDrawer>().AddPoints(Pixels_[i].position);       
                }

                else
                {
                    if (j-1>=0)
                        MoveCountours[i].VectorList[j] =    MoveCountours[i].VectorList[j-1];
                    
                  
                }

                //GetComponent<PolyDrawer>().UpdateFigure();

            }

            for (int j = 0; j < AllCountours[i].VectorList.Count; j++)
            {
                MoveCountours[i].VectorList[j] = MoveCountours[i].VectorList[j] - lowestPoint;
            }


        }
        

        try
        {
            Draw();
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
               MoveCountours.Add(new ListOfList());
               MoveCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
               StartCoutours.Add(new ListOfList());
               StartCoutours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
               GameObject sp = Instantiate(shadowPrototype, transform);
               Shadows.Add(sp);
               PolyDrawer pd = sp.AddComponent<PolyDrawer>()as PolyDrawer;
               PDList.Add(sp.GetComponent<PolyDrawer>());
               pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
               pd.Mat = Mat;
               pd.UpdateFigure();
           }
              
              
            
           
          
         
       

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


