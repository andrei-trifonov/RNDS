
using UnityEngine;
using System.Collections.Generic;
using System.Linq;



[System.Serializable]

public class ListOfList
{
    public bool Flipped;
    public PolyDrawer PD;
    public List<Vector3> VectorList;
    public Sprite shadowSprite;
    public GameObject shadowObj;
    public List<Vector3> StartList;
    public List<Vector3> MoveList;
}
public class ShadowGPT : MonoBehaviour

{
    public float distortion;
    public bool isGlobalLight;
    private bool Flipped;
    [SerializeField] CoverParallaxShadow CPS;
    [SerializeField] float zPosition = 5.32f;
    [SerializeField] private float ScaleStart;
    [SerializeField] private Vector2 CorrectionStart;
    [SerializeField] private Material Mat;
    [SerializeField] private  Sprite Sprite;
    [SerializeField] private float Quality = 0.05f;
    [SerializeField] private Transform Sihouette;
    [SerializeField] private LayerMask shadowMask;
    [SerializeField] private LayerMask blockMask;
   
    [SerializeField] private float shadowDistance;
    [SerializeField] private float disableDistance;
    [SerializeField] private Sprite[] renderSprites;
    private List<GameObject> Lights = new List<GameObject>();
    private float lightDistance =999 ;
    [SerializeField] private GameObject mainLight;
    ListOfList targetCountour = new ListOfList(); 
    private Vector3 Center;
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    private GameObject lastShadow;
    [SerializeField] private GameObject shadowPrototype;
    private List<ListOfList> AllCountours = new List<ListOfList>();

    public void AddLight(GameObject light)
    {
        if(!Lights.Contains(light))
            Lights.Add(light);
    }
    
    public void checkLight(GameObject light)
    {
        float distance = Vector2.Distance(light.transform.position, Sihouette.transform.position);
        if (distance < lightDistance)
        {
            mainLight = light;
            lightDistance = distance;
            CPS.SetLight(mainLight);
        }
           

    }

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
    void CreateShadowFrame()
    {
       
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

        //simplifiedPoints = points;
        if (simplifiedPoints.Count > 2)

        {
            GameObject sp = Instantiate(shadowPrototype, transform);
            for (int j = 0; j < simplifiedPoints.Count; j++)
            {
                simplifiedPoints[j] = sp.transform.TransformPoint(simplifiedPoints[j]);
                simplifiedPoints[j] += CorrectionStart;
                simplifiedPoints[j] = new Vector2(simplifiedPoints[j].x * ScaleStart, simplifiedPoints[j].y * ScaleStart);

            }

            simplifiedPoints.Reverse();
            
            AllCountours.Add(new ListOfList());
            ListOfList lastCreated = AllCountours.Last();
            lastCreated.VectorList = (Vector2ListToVector3List(simplifiedPoints));
            lastCreated.StartList = (Vector2ListToVector3List(simplifiedPoints));
            lastCreated.MoveList = (Vector2ListToVector3List(simplifiedPoints));
            lastCreated.shadowObj = sp;
            lastCreated.shadowSprite = Sprite;
         
            lastCreated.PD = sp.GetComponent<PolyDrawer>();
            lastCreated.PD.RawPoints = Vector2ListToVector3List1(simplifiedPoints);
            lastCreated.PD.Mat = Mat;
            if (lastCreated.shadowSprite != null)
                targetCountour = lastCreated;
        }

    }
    private void FixedUpdate()
    {
        if (mainLight)
            lightDistance = Vector2.Distance(Sihouette.position, mainLight.transform.position);
        else
            lightDistance = 999;
        foreach (GameObject light in Lights)
        {
            checkLight(light);
        }
        if (mainLight != null && Vector3.Distance(mainLight.transform.position, Sihouette.transform.position)< disableDistance)
        {
          
            foreach (var countour in AllCountours)
            {
                if (countour.shadowSprite == Sprite && countour.shadowSprite != null)
                {
                    targetCountour = countour;
                    break;
                }
            }

            if (Flipped != targetCountour.Flipped)
            {
                targetCountour.Flipped = Flipped;

                for (int j = 0; j < targetCountour.VectorList.Count; j++)
                {
                    targetCountour.VectorList[j] = new Vector2(targetCountour.VectorList[j].x * -1, targetCountour.VectorList[j].y);
                    targetCountour.StartList[j] = new Vector2(targetCountour.StartList[j].x * -1, targetCountour.StartList[j].y);
                }
                targetCountour.VectorList.Reverse();
                targetCountour.StartList.Reverse();
            }

            Vector3 lowestPoint = new Vector3(0, 999, 0);
            Vector3 rightPoint = new Vector3(-999, 0, 0);
            Vector3 leftPoint = new Vector3(999, 0, 0);



            for (int j = 0; j < targetCountour.VectorList.Count; j++)
            {
              

                targetCountour.VectorList[j] = new Vector3((Sihouette.position + targetCountour.StartList[j]).x, (Sihouette.position + targetCountour.StartList[j]).y, 0);

                RaycastHit hit;

                
                Vector3 ray = (ConvertToVector3(targetCountour.VectorList[j]) - mainLight.transform.position).normalized;

                Debug.DrawRay(mainLight.transform.position, ray * shadowDistance, UnityEngine.Color.yellow);
                
                if (Physics.Raycast(mainLight.transform.position, ray, out hit, shadowDistance, blockMask) )
                {
                    if (j - 1 >= 0)
                        targetCountour.MoveList[j] = targetCountour.MoveList[j - 1];
                    else
                    {
                        targetCountour.MoveList[j] = targetCountour.MoveList.Last();
                    }
                }
                else
                {
                    if (Physics.Raycast(mainLight.transform.position, ray, out hit, shadowDistance, shadowMask))
                    {
                     
                        if (rightPoint.x < hit.point.x)
                            rightPoint = new Vector3(hit.point.x, 0, 0);
                        if (leftPoint.x > hit.point.x)
                            leftPoint = new Vector3(hit.point.x, 0, 0);
                        if (lowestPoint.y > hit.point.y)
                            lowestPoint = new Vector3(0, hit.point.y, 0);
                        targetCountour.MoveList[j] = new Vector3(hit.point.x, hit.point.y, 0);
                        if (j - 1 >= 0)
                            if (Vector2.Distance(hit.point, targetCountour.MoveList[j - 1])>distortion)
                            {
                                targetCountour.MoveList[j] = targetCountour.MoveList[j - 1];
                            }
                       
                    }

                    else
                    {
                        if (j - 1 >= 0)
                            targetCountour.MoveList[j] = targetCountour.MoveList[j - 1];
                        else
                        {
                            targetCountour.MoveList[j] = lowestPoint;
                        }
                    }
                }

                
            }

            Center = (leftPoint + rightPoint) / 2;

            if (lastShadow != null)
                lastShadow.SetActive(false);

            //Спавн меша в начале координат 
            for (int j = 0; j < targetCountour.VectorList.Count; j++)
            {
                targetCountour.MoveList[j] = new Vector3(targetCountour.MoveList[j].x - leftPoint.x, targetCountour.MoveList[j].y - lowestPoint.y, 0);
            }

            //перенос меша на праильное место тени

        
            targetCountour.shadowObj.transform.position = new Vector3(Sihouette.transform.position.x - Center.x + leftPoint.x, Sihouette.transform.position.y, zPosition);


            if (targetCountour.shadowObj != null)
                targetCountour.shadowObj.SetActive(true);

            lastShadow = targetCountour.shadowObj;

            Draw(targetCountour);
        }

        else
        {
            lastShadow.SetActive(false);
        }

    }

  
   private void Start()

   {
       foreach (var sprite in renderSprites)
       {
           Sprite = sprite;
           CreateShadowFrame();
       }

       lastShadow = targetCountour.shadowObj;
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


