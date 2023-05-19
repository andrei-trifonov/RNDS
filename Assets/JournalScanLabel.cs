using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalScanLabel : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Text Label;

    [SerializeField] private Text descriptionLabel;
    [SerializeField] private Image descriptionImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject Mark;
    private string Text;
    private int num;
    public void OnSpawn(Sprite image, string label, string text, int num)
    {
        
        Icon.sprite = image;
        Label.text = label;
        Text = text;
        this.num = num;
        if (  PlayerPrefs.GetInt(num +"Mark") == 1)
        {
            Mark.SetActive(false);
        }
    }
    public void OnClick()
    {
        Mark.SetActive(false);
        PlayerPrefs.SetInt(num +"Mark", 1);
        descriptionImage.sprite = Icon.sprite;
        descriptionLabel.text = Label.text;
        descriptionText.text = Text;
        descriptionText.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionText.transform.parent.GetComponent<RectTransform>().sizeDelta.x, descriptionText.text.Length / 20 * 50);
    }
}
