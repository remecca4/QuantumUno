using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class Card : MonoBehaviour

{
    //if color or number doesn't matter leave both blank, color and number lists will always have size two
    public List<string> color;
    public List<int> number;
    public Sprite frontSprite;
    public Sprite backSprite;
    public TextMeshProUGUI card_text;
    private Image cardImage;    // The UI Image component

    //all different color images
    public Sprite redCard;
    public Sprite blueCard;
    public Sprite greenCard;
    public Sprite yellowCard;
    public Sprite red_yellowCard;
    public Sprite red_blueCard;
    public Sprite red_greenCard;
    public Sprite blue_yellowCard;
    public Sprite blue_greenCard;
    public Sprite green_yellowCard;

    void Awake()
    {
        cardImage = GetComponent<Image>();
       
    }

    public void ShowFront()
    {
        cardImage.sprite = frontSprite;

        if (card_text != null)
        {
            card_text.gameObject.SetActive(true);  // Make text visible on front
        }
    }

    public void ShowBack()
    {
        cardImage.sprite = backSprite;

        if (card_text != null)
        {
            card_text.gameObject.SetActive(false);  // Hide text on back
        }
    }
    public void setColor()
    {
        if (color[0] == "red" && color[1] == "")
            frontSprite = redCard;
        else if (color[0] == "blue" && color[1] == "")
            frontSprite = blueCard;
        else if (color[0] == "green" && color[1] == "")
            frontSprite = greenCard;
        else if (color[0] == "yellow" && color[1] == "")
            frontSprite = yellowCard;
        else if ((color[0] == "red" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "red"))
            frontSprite = red_yellowCard;
        else if ((color[0] == "red" && color[1] == "blue") || (color[0] == "blue" && color[1] == "red"))
            frontSprite = red_blueCard;
        else if ((color[0] == "red" && color[1] == "green") || (color[0] == "green" && color[1] == "red"))
            frontSprite = red_greenCard;
        else if ((color[0] == "blue" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "blue"))
            frontSprite = blue_yellowCard;
        else if ((color[0] == "blue" && color[1] == "green") || (color[0] == "green" && color[1] == "blue"))
            frontSprite = blue_greenCard;
        else if ((color[0] == "yellow" && color[1] == "green") || (color[0] == "green" && color[1] == "yellow"))
            frontSprite = green_yellowCard;


    }
    public virtual void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
       
    }
}
