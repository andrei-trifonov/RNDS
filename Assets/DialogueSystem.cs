using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum charsDropdown { 
      Psye,
      Lilly,
      Hans
};

[System.Serializable]
public struct chatWindow {
    public charsDropdown Char;
    public Text textLine;
    public Animator chatCloud;
}
[System.Serializable]
public struct chatLine
{
    public charsDropdown Char;
    [TextArea(15, 10)]
    public string textLine;

}
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private chatWindow[] chatWindows;
    [SerializeField] private chatLine[] chatLines;
    [HideInInspector] public List<int> charNumbers;
    private string Language;
    [SerializeField] private PlatformerCharacter2D Player;
    private int stringCounter;
    private bool Blocked;

    private string ReturnCharName(int Char)
    {
        switch (Language) {
            case "RUS":
                { 
                    switch (Char) {
                        case 0: return "Сай"; break;
                        case 1: return "Лилли"; break;
                        case 2: return "Ханс"; break;
                    
                                     
                    } 
                  
                }
            break;
        }
        return "";

    }

    [SerializeField] private bool DEBUG;
    private void Start()
    {
        if (DEBUG) {
            PlayerPrefs.SetString("Language", "RUS");
        }
        for(int i =0; i< chatWindows.Length; i++)
        {
            chatWindows[i].chatCloud = Instantiate(chatWindows[i].chatCloud);
        }
        Language = PlayerPrefs.GetString("Language");
    }
    public void StartDialogue()
    {
        Player.Block();

        foreach (chatWindow chat in chatWindows)
        {
            
            charNumbers.Add((int)chat.Char);            
        }

       

    }
    IEnumerator DiaSwapCoroutine()
    {
        Blocked = true;

        foreach (chatWindow chat in chatWindows)
        {
            chat.chatCloud.SetBool("PopUp", false);
        }
        yield return new WaitForSeconds(0.2f);
        foreach (chatWindow chat in chatWindows)
        {
            chat.chatCloud.SetBool("PopUp", true);
        }
        chatWindows[charNumbers[(int)chatLines[0].Char]].textLine.text = chatLines[stringCounter].textLine;
        stringCounter++;
        if (stringCounter > chatLines.Length)
        {
            EndDialogue();
        } 
        Blocked = false;
    }
    public void ContinueDialogue()
    {
        if (!Blocked)
            StartCoroutine(DiaSwapCoroutine());
       
    }
    private void EndDialogue()
    {
        Player.GetComponent<PlatformerCharacter2D>().UnBlock();
        stringCounter = 0;
        foreach (chatWindow chat in chatWindows)
        {
            chat.chatCloud.gameObject.SetActive(false);
            chat.chatCloud.SetBool("PopUp",false);
            charNumbers.Clear();
        }
    }
}
