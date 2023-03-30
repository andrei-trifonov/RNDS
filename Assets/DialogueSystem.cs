using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Color = System.Drawing.Color;

public enum charsDropdown { 
      Psye,
      Lilly,
      Hans
};

[System.Serializable]
public struct chatWindow {
    public charsDropdown Char;
    public Text textAuthor;
    public Text textLine;
    public Animator chatCloud;
    public GameObject Actor;
}
[System.Serializable]
public struct chatLine
{
    public charsDropdown Char;
    [TextArea(15, 10)]
    public string textLine;

}
[System.Serializable]
public struct chatVariant
{
    public chatLine[] chatLines;

}

[System.Serializable]

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private chatWindow[] chatWindows;
    [SerializeField] private chatVariant[] chatVariants;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    private List<GameObject> actorsList = new List<GameObject>();
    private string Language;
    private HandsHolds HH;
    private int stringCounter;
    private int currChat;


    private Tuple<UnityEngine.Color, string> ReturnCharName(int Char)
    {


        switch (Language) {
            case "RUS":
                {
                    switch (Char) {
                        case 0:
                            return Tuple.Create(UnityEngine.Color.yellow, "Сай"); break;
                        case 1:
                            return Tuple.Create(UnityEngine.Color.red, "Лилли"); break;
                        case 2:
                            return Tuple.Create(UnityEngine.Color.gray, "Ханс"); break;


                    }

                }
                break;
        }
        return Tuple.Create(UnityEngine.Color.black, "Unknown"); ;

    }

    [SerializeField] private bool DEBUG;
    private void Start()
    {

        if (DEBUG) {
            PlayerPrefs.SetString("Language", "RUS");
        }
        for (int i = 0; i < chatWindows.Length; i++)
        {
            chatWindows[i].chatCloud.gameObject.SetActive(false);
        }
        Language = PlayerPrefs.GetString("Language");
        HH = GameObject.FindObjectOfType<HandsHolds>();
    }
    IEnumerator CastString(string str, Text text) {
        text.text = "";
        for (int i = 0; i < str.Length; i++) {
            text.text += RandomSymbol();
            yield return new WaitForSeconds(0.05f);
            if (HH.ItemNum() == 14) //Транскриптор
            {
                text.text = text.text.Substring(0, text.text.Length - 1);
                text.text += str[i];
                yield return new WaitForSeconds(0.05f);
            }
          
        }

    }
    char RandomSymbol()
    {
        string symbols = "@#$%^&*()~`<>/+=";

        return symbols[UnityEngine.Random.Range(0, symbols.Length)];
    }
    public void ContinueDialogue()
    {
        startButton.SetActive(false);
        continueButton.SetActive(true);
        if (stringCounter < chatVariants[currChat].chatLines.Length)
        {


            for (int i = 0; i < chatWindows.Length; i++)
            {
                chatWindows[i].chatCloud.gameObject.SetActive(false);
                if (chatWindows[i].Char == chatVariants[currChat].chatLines[stringCounter].Char)
                {

                    chatWindows[i].chatCloud.gameObject.SetActive(true);
                    Tuple<UnityEngine.Color, string> tuple = ReturnCharName(i);
                    chatWindows[i].textAuthor.color = tuple.Item1;
                    chatWindows[i].textAuthor.text = tuple.Item2;
                   
                    chatWindows[i].chatCloud.SetBool("PopUp", true);
                    StopAllCoroutines();
                    StartCoroutine(CastString(chatVariants[currChat].chatLines[stringCounter].textLine, chatWindows[i].textLine));

                    break;
                }
            }

            stringCounter++;



        }
        else
        {
            EndDialogue();
        }


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
            startButton.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
        currChat = UnityEngine.Random.Range(0, chatVariants.Length);
        startButton.SetActive(true);
        continueButton.SetActive(false);
    }
}
