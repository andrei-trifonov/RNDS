using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ParallaxLevel
{
    [SerializeField] public List<GameObject> parallaxObjects;
    [SerializeField] public float xmaxOffset;
    [SerializeField] public float ymaxOffset;
    [HideInInspector] public List <Vector3> startPositions;
}

public class CoverParallax : MonoBehaviour
{

    [SerializeField] private List<ParallaxLevel> ParallaxLevels;
    [SerializeField] private GameObject opticCenter;
    [SerializeField] private float symmetricBounds;
    
    private GameObject Camera;
    private float xOffset;
    private float yOffset;
    private float Percentage;
    private float xtranslitedOffset;
    private float ytranslitedOffset;
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        for (int i = 0; i < ParallaxLevels.Count; i++)
        {

            for (int j = 0; j < ParallaxLevels[i].parallaxObjects.Count; j++)
            {
                if (ParallaxLevels[i].parallaxObjects[j]!= null)     
                  ParallaxLevels[i].startPositions.Add(ParallaxLevels[i].parallaxObjects[j].transform.localPosition);
            }
        }
        
        
       
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < ParallaxLevels.Count; i++)
        {
            var camPos = Camera.transform.position;
            var centerPos = opticCenter.transform.position;
            xOffset = camPos.x - centerPos.x;
            xtranslitedOffset = ParallaxLevels[i].xmaxOffset * Mathf.Clamp(xOffset / symmetricBounds, -1, 1) * (-1);
            yOffset = camPos.y - centerPos.y;
            ytranslitedOffset = ParallaxLevels[i].ymaxOffset * Mathf.Clamp(yOffset / symmetricBounds, -1, 1) * (-1);

            for (int j = 0; j < ParallaxLevels[i].parallaxObjects.Count; j++)
            {
                if (ParallaxLevels[i].parallaxObjects[j] != null)
                {
                    var position = ParallaxLevels[i].parallaxObjects[j].transform.localPosition;
                    position = new Vector3(ParallaxLevels[i].startPositions[j].x + xtranslitedOffset,
                        ParallaxLevels[i].startPositions[j].y + ytranslitedOffset,
                        ParallaxLevels[i].startPositions[j].z);
                    ParallaxLevels[i].parallaxObjects[j].transform.localPosition = position;
                }
            }
        }
    }
}
