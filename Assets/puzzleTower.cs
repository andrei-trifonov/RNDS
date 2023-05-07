using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;



public class puzzleTower : MonoBehaviour
{


    public void Shuffle(List<int> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0,n + 1);  
            int t = list[k];  
            list[k] = list[n];  
            list[n] = t;  
        }  
    }

    [SerializeField] public AudioClip buttonPressSound;
    [SerializeField] public AudioClip wrongSound;
    [SerializeField] public AudioClip finishSound;
    [SerializeField] public AudioClip turnSound;
    [SerializeField] public outerStopZone SZ;
    [SerializeField] public List<SpriteRenderer> Template;
    [SerializeField] public List<SpriteRenderer> Task;
    [SerializeField] public List<puzzleButton> Buttons;
    [SerializeField] private TextMeshPro TMP;
    [SerializeField] private upgradeManager uM;
    [HideInInspector] public List<int> templateInt;
    [HideInInspector] public List<int> taskInt;
    [HideInInspector] public List<int> answerInt;
    [SerializeField] public List<GameObject> destroyAfterPuzzle;
    [SerializeField] public List<GameObject> enableAfterPuzzle;
    private bool isPrep;
    private AudioSource m_AudioSource;
    private int winCount=0;
    private bool firstLaunch=true;
    IEnumerator RestartCoroutine()
    {
        isPrep = false;
        for (int i = 0; i < 3; i++)
        {
            Template[i].enabled = false;
            Task[i].enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
       
        for (int i = 0; i < 3; i++)
        {
            if (!firstLaunch)
              m_AudioSource.PlayOneShot(turnSound);
            Template[i].enabled = true;
            switch (templateInt[i])
            {
                case 0: Template[i].color = new Color(1f, 0.3f, 0.2f); break;
                case 1: Template[i].color = new Color(0.5f, 1f, 0.5f); break;
                case 2: Template[i].color = new Color(0.1f, 0.7f, 1f); break;
            }
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 3; i++)
        {
            if (!firstLaunch)
              m_AudioSource.PlayOneShot(turnSound);
            taskInt.Add(Random.Range(0,3));
            Task[i].enabled = true;
            switch (taskInt.Last())
            {
                case 0: Task[i].color = new Color(1f, 0.3f, 0.2f); break;
                case 1: Task[i].color = new Color(0.5f, 1f, 0.5f); break;
                case 2: Task[i].color = new Color(0.1f, 0.7f, 1f); break;
            }
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i < 3; i++)
        {
            Buttons[i].SetNumber(templateInt[i]);
        }

        firstLaunch = false;
        isPrep = true;

    }
    void Restart()
    {
        templateInt.Clear();
        taskInt.Clear();
        answerInt.Clear();
        templateInt.Add(0);
        templateInt.Add(1);
        templateInt.Add(2);
        Shuffle(templateInt);
        StartCoroutine(RestartCoroutine());
        
    }
    
    
    public void ButtonPressed(int i)
    {
        m_AudioSource.PlayOneShot(buttonPressSound);
        if (isPrep)
            answerInt.Add(i);
        if (answerInt.Count >= 3)
        {
            if (answerInt[0] == taskInt[0] && answerInt[1] == taskInt[1] && answerInt[2] == taskInt[2])
            {
                winCount++;
                TMP.text = winCount + "/3";
                if (winCount < 3)
                    Restart();
                else
                {
                    m_AudioSource.PlayOneShot(finishSound);
                    SZ.UnblockMachine();
                    uM.Upgrade(1);
                    uM.Upgrade(5);
                    foreach (var go in enableAfterPuzzle)
                    {
                        go.SetActive(true);
                    
                    }
                    foreach (var go in destroyAfterPuzzle)
                    {
                        go.SetActive(false);
                     
                    }   
                    this.enabled = false;
                }
            }
            else
            {
                m_AudioSource.PlayOneShot(wrongSound);
                Restart();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Save") == "01")
        {

            SZ.enabled = false;
          
            foreach (var go in enableAfterPuzzle)
            {
                go.SetActive(true);
                    
            }
            foreach (var go in destroyAfterPuzzle)
            {
                go.SetActive(false);
                     
            }   
            this.enabled = false;
        }
        else
        {
            m_AudioSource = GetComponent<AudioSource>();
            Restart();    
        }
        
    }

    // Update is called once per frame
    
}
