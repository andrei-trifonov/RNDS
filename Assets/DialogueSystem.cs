using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using Color = System.Drawing.Color;
using Random = System.Random;

public enum charsDropdown { 
      Psye,
      Lilly,
      Hans,
      Tess,
      Clayman,
      Nobody,
      Broadcaster
};

[System.Serializable]
public struct chatWindow {
    public charsDropdown Char;
    public AudioClip[] Clips;
    public Text textAuthor;
    public Text textLine;
    public Animator chatCloud;
    public GameObject Actor;
}
[System.Serializable]
public struct chatLine
{
    public charsDropdown Char;
    [TextArea(15, 5)]
    public string textLine;

}
[System.Serializable]
public struct chatVariant
{
    public chatLine[] chatLines;
    public GameObject dialogueTrigger;
    public bool noRepeat;
}

[System.Serializable]

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private chatWindow[] chatWindows;
    [SerializeField] private List<chatVariant> chatVariants;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private float textSpeed = 0.01f;
    public bool Translated;
    private List<GameObject> actorsList = new List<GameObject>();
    private string Language;
    private HandsHolds HH;
    private int stringCounter;
    private int currChat;
    private AudioSource m_AudioSource;
    private bool casting;
    private int scTemp;
    private int cnTemp;
    private Text currentChat;
    private Tuple<UnityEngine.Color, string> ReturnCharName(int Char)
    {


        switch (Language) {
            case "RUS":
                {
                    switch (Char) {
                        case 0:
                            return Tuple.Create(new UnityEngine.Color(0.7f, 0.6f,0.1f), "Сай"); break;
                        case 1:
                            return Tuple.Create(new UnityEngine.Color(0.7f,0.2f,0.3f), "Лилли"); break;
                        case 2:
                            return Tuple.Create(new UnityEngine.Color(0.2f, 0.2f, 0.2f), "Ханс"); break;
                        case 3:
                            return Tuple.Create(new UnityEngine.Color(0.2f, 0.1f, 0.3f), "Тесс"); break;
                        case 4:
                            return Tuple.Create(new UnityEngine.Color(0.35f, 0.2f, 0.2f), "Черепок"); break;
                        case 5:
                            return Tuple.Create(new UnityEngine.Color(0.35f, 0.2f, 0.2f), "???"); break;
                        case 6:
                            return Tuple.Create(new UnityEngine.Color(0.35f, 0.2f, 0.2f), "Вещатель"); break;

                    }

                }
                break;
        }
        return Tuple.Create(UnityEngine.Color.black, "Unknown"); ;

    }

    [SerializeField] private bool DEBUG;
    private void Start()
    {
        if (chatVariants.Count > 0)
        {
            if (startButton)
            {
                startButton.SetActive(true);
            }
        }
        else
        {
            if (startButton)
            {
                startButton.SetActive(false);
            }
        }
        m_AudioSource = GetComponent<AudioSource>();

        for (int i = 0; i < chatWindows.Length; i++)
        {
            chatWindows[i].chatCloud.gameObject.SetActive(false);
        }
        Language = PlayerPrefs.GetString("Language");
        HH = GameObject.FindObjectOfType<HandsHolds>();
    }
    IEnumerator CastString(string str, Text text) {
        casting = true;
        text.text = "";
        for (int i = 0; i < str.Length; i++) {
            text.text += RandomSymbol();
            yield return new WaitForSeconds(textSpeed);
            if (HH.ItemNum() == 14 || Translated) //Транскриптор
            {
                text.text = text.text.Substring(0, text.text.Length - 1);
                text.text += str[i];
                yield return new WaitForSeconds(textSpeed);
            }
          
        }

        casting = false;

    }
    char RandomSymbol()
    {
        string symbols = "@#$%^&*()~`<>/+=";

        return symbols[UnityEngine.Random.Range(0, symbols.Length)];
    }

    public void ClearDialogue()
    {
        chatVariants.Clear();
    }
    public void AddVariant(chatVariant chat)
    {
        if (!chatVariants.Contains(chat))
        {
            chatVariants.Add(chat);
            startButton.SetActive(true);
        }
    }
    public void ContinueDialogue()
    {
        if(startButton)
            startButton.SetActive(false);
        if(continueButton)
            continueButton.SetActive(true);
        if (casting)
        {
            StopAllCoroutines();
            casting = false;
            if (HH.ItemNum() == 14 || Translated) //Транскриптор
            {
                currentChat.text = chatVariants[cnTemp].chatLines[scTemp].textLine;
            }
            else
            {
                StringBuilder tmp = new StringBuilder();
                for (int smb = 0; smb < chatVariants[cnTemp].chatLines[scTemp].textLine.Length; smb++)
                    tmp.Append(RandomSymbol());
                currentChat.text = tmp.ToString();
            }

        }
        else
        {
           
       
        if (stringCounter < chatVariants[currChat].chatLines.Length)
        {
            
            

            for (int i = 0; i < chatWindows.Length; i++)
            {
                chatWindows[i].chatCloud.gameObject.SetActive(false);
                if (chatWindows[i].Char == chatVariants[currChat].chatLines[stringCounter].Char)
                {
                    currentChat = chatWindows[i].textLine; 
                    chatWindows[i].chatCloud.gameObject.SetActive(true);
                    Tuple<UnityEngine.Color, string> tuple = ReturnCharName((int)chatWindows[i].Char);
                    chatWindows[i].textAuthor.color = tuple.Item1;
                    chatWindows[i].textAuthor.text = tuple.Item2;
                   Debug.Log(tuple.Item2);
                    chatWindows[i].chatCloud.SetBool("PopUp", true);
                    StopAllCoroutines();
                    try
                    {
                        m_AudioSource.PlayOneShot(chatWindows[i]
                            .Clips[UnityEngine.Random.Range(0, chatWindows[i].Clips.Length)]);
                    }
                    catch 
                    {
                    
                    }

                    scTemp = stringCounter;
                    cnTemp = currChat;
                    StartCoroutine(CastString(chatVariants[currChat].chatLines[stringCounter].textLine, chatWindows[i].textLine));

                    break;
                }
            }

            stringCounter++;



        }
        else
        {
            EndDialogue();
        } }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Actor") || collision.CompareTag("Player"))
        {
            foreach (chatWindow cw in chatWindows)
                if (cw.Actor == collision.gameObject)
                {
                    cw.chatCloud.transform.position = new Vector2(collision.gameObject.transform.position.x, cw.chatCloud.transform.position.y);
                }
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            EndDialogue();
       
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(startButton)
                startButton.SetActive(true);
        }
    }
    private void EndDialogue()
    {
      
        stringCounter = 0;
        foreach (chatWindow chat in chatWindows)
        {
            chat.chatCloud.gameObject.SetActive(false);
           
            
        }

        if (chatVariants[currChat].noRepeat)
        {
            Destroy(chatVariants[currChat].dialogueTrigger);
            if (startButton && chatVariants.Count < 2)
            {
                startButton.SetActive(false);
                Debug.Log("Здесь");
            }
            else
            {
                startButton.SetActive(true);
            }

            if (continueButton)
                continueButton.SetActive(false);
            chatVariants.Remove(chatVariants[currChat]);
        }
        else
        {
            currChat = UnityEngine.Random.Range(0, chatVariants.Count);
            if (startButton)
                startButton.SetActive(true);
            if (continueButton)
                continueButton.SetActive(false);
        }
    }
}
