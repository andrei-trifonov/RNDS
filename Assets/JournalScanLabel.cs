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
    private string Text;

    public void OnSpawn(Sprite image, string label, string text)
    {
        Icon.sprite = image;
        Label.text = label;
        Text = text;
    }
    public void OnClick()
    {
        descriptionImage.sprite = Icon.sprite;
        descriptionLabel.text = Label.text;
        descriptionText.text = Text;
    }
}
