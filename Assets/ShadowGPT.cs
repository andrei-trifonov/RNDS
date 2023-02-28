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
   
    public bool Flipped;
    Vector3 closestVect ;
    public float pixelModifier;
    public float ScaleStart;
    public Vector2 CorrectionStart;
    [SerializeField] private bool Bind;
    private GameObject shadowPrototype ;
   
    private  List<ListOfList> AllCountours = new List<ListOfList>() ;

    [SerializeField] private Material Mat;
  
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    [SerializeField] private GameObject Light;
    [SerializeField] private  Sprite Sprite;
    [SerializeField] private float Quality = 5;
    [SerializeField] private Transform Sihouette;
    private List<GameObject> Shadows = new List<GameObject>();
    [SerializeField] private LayerMask shadowMask;
    [SerializeField] private float shadowDistance;
    public Vector3 Center;
    ListOfList targetCountour = new ListOfList();
    private bool hasCenter;
  
   
    public void SetSprite(Sprite newSprite, bool flipped)
    {
            Flipped = flipped;
            Sprite = newSprite;
    }
    void Draw(ListOfList target)
    {
        if (target.VectorList.Count > 2 ) 
        {
            target.PD.RawPoints = target.MoveList ; 
            target.PD.UpdateFigure();
        }
    }

    private void Update()
    {
        bool Found = false;
        foreach (var countour in AllCountours)
        {
            if (countour.shadowSprite == Sprite  && countour.shadowSprite!=null)
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

                GameObject sp = Instantiate(shadowPrototype, transform);
                Shadows.Add(sp);
                
                for(int j=0; j<simplifiedPoints.Count; j++)
                {
           
                    simplifiedPoints[j] = sp.transform.TransformPoint(simplifiedPoints[j]);
                    simplifiedPoints[j] += CorrectionStart;
                    simplifiedPoints[j] *= ScaleStart;

                }

                simplifiedPoints.Reverse();

                AllCountours.Add(new ListOfList());
                AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
                AllCountours.Last().StartList = (Vector2ListToVector3List(simplifiedPoints));
                AllCountours.Last().MoveList  = (Vector2ListToVector3List(simplifiedPoints));
                AllCountours.Last().shadowObj = sp;
                AllCountours.Last().shadowSprite = Sprite;

                PolyDrawer pd = sp.AddComponent<PolyDrawer>() as PolyDrawer;
                AllCountours.Last().PD = (sp.GetComponent<PolyDrawer>());
                pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
                pd.Mat = Mat;
                if (AllCountours.Last().shadowSprite!=null)
                    targetCountour = AllCountours.Last();
            }

           
        }
   

        Vector3 lowestPoint = new Vector3(0,999,0);
        Vector3 rightPoint = new Vector3(-999,0,0);
        Vector3 leftPoint = new Vector3(999,0,0);

        for (int j=0; j< targetCountour.VectorList.Count;j++)
        {
            
            targetCountour.VectorList[j] =   new Vector3((Sihouette.position+  targetCountour.StartList[j]).x,  (Sihouette.position+  targetCountour.StartList[j]).y ,0);

            RaycastHit hit;
            Vector3 ray = (ConvertToVector3(targetCountour.VectorList[j]) - Light.transform.position).normalized;
            
            Debug.DrawRay(Light.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);

            if (Physics.Raycast(Light.transform.position, ray, out hit, shadowDistance, shadowMask))
            {

                if (rightPoint.x < hit.point.x)
                    rightPoint = new Vector3(hit.point.x, 0, 0);
                if (leftPoint.x > hit.point.x)
                    leftPoint = new Vector3(hit.point.x, 0, 0);
                if (lowestPoint.y > hit.point.y)
                    lowestPoint = new Vector3(0, hit.point.y, 0);
                targetCountour.MoveList[j] = new Vector3(hit.point.x, hit.point.y, 0);
            }

            else
            {
                if (j-1>=0)
                    targetCountour.MoveList[j] =    targetCountour.MoveList[j-1];
            }
        }
        
        Center = (leftPoint + rightPoint) / 2;
        Center.y = lowestPoint.y;
        float nearestDist = 999;
       
        for (int j = 0; j < targetCountour.VectorList.Count; j++)
        {
            if (Vector2.Distance(targetCountour.VectorList[j], Center) < nearestDist )
            {
                nearestDist = Vector2.Distance(targetCountour.VectorList[j], Center);
                closestVect = targetCountour.VectorList[j];
            }
        }
        
        Vector3 Offset = closestVect - Center;
        

        foreach (GameObject shadow in Shadows)
        {
            shadow.SetActive(false);
        }
        if (Bind)
        {
            foreach (GameObject shadow in Shadows)
            {
                if (!Flipped)
                {
                   shadow.transform.localScale = new Vector3(  1,  1,   -1);
                   shadow.transform.rotation = Quaternion.Euler(0,180,0);
                   shadow.transform.position = Sihouette.transform.position + new Vector3(-Offset.x, Offset.y, 0) - (new Vector3(-closestVect.x, closestVect.y)); 
                }
                if (Flipped)
                {
                    shadow.transform.localScale = new Vector3(  1,  1,   1);
                    shadow.transform.rotation = Quaternion.Euler(0,0,0);
                    shadow.transform.position = Sihouette.transform.position + Offset - closestVect; 
                }
                
            }
        }
        else
        {
            foreach (GameObject shadow in Shadows)
            {

                    shadow.transform.localScale = new Vector3(  1,  1,   1);
                    shadow.transform.rotation = Quaternion.Euler(0,0,0);
                    shadow.transform.position = Sihouette.transform.position + Offset - closestVect;
            }
        }
      
        
        targetCountour.shadowObj.SetActive(true);
        Draw(targetCountour);

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
           GameObject sp = Instantiate(shadowPrototype, transform);
           Shadows.Add(sp);
           for(int j=0; j<simplifiedPoints.Count; j++)
           {
               simplifiedPoints[j] = sp.transform.TransformPoint(simplifiedPoints[j]);
               simplifiedPoints[j] += CorrectionStart;
               simplifiedPoints[j] *= ScaleStart;
           }
           simplifiedPoints.Reverse();

           AllCountours.Add(new ListOfList());
           AllCountours.Last().VectorList = (Vector2ListToVector3List(simplifiedPoints));
           AllCountours.Last().StartList = (Vector2ListToVector3List(simplifiedPoints));
           AllCountours.Last().MoveList  = (Vector2ListToVector3List(simplifiedPoints));

           PolyDrawer pd = sp.AddComponent<PolyDrawer>()as PolyDrawer;
           AllCountours.Last().PD = sp.GetComponent<PolyDrawer>();
        
           pd.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
           pd.Mat = Mat;
           pd.UpdateFigure();
       }

       targetCountour = AllCountours.Last();

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


