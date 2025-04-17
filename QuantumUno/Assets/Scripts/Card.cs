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
    public string card_type = "normal";
    //all different normal card color images
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
    //all different skip card color images
    public Sprite redCard_skip;
    public Sprite blueCard_skip;
    public Sprite greenCard_skip;
    public Sprite yellowCard_skip;
    public Sprite red_yellowCard_skip;
    public Sprite red_blueCard_skip;
    public Sprite red_greenCard_skip;
    public Sprite blue_yellowCard_skip;
    public Sprite blue_greenCard_skip;
    public Sprite green_yellowCard_skip;
    //all different reverse card color images
    public Sprite redCard_rev;
    public Sprite blueCard_rev;
    public Sprite greenCard_rev;
    public Sprite yellowCard_rev;
    public Sprite red_yellowCard_rev;
    public Sprite red_blueCard_rev;
    public Sprite red_greenCard_rev;
    public Sprite blue_yellowCard_rev;
    public Sprite blue_greenCard_rev;
    public Sprite green_yellowCard_rev;

    void Awake()
    {
        cardImage = GetComponent<Image>();
        card_text = GetComponentInChildren<TextMeshProUGUI>();
        color[1] = "";


    }

    public void ShowFront()
    {
        cardImage.sprite = frontSprite;

        if (card_text != null && number[0] < 10)
        {
            card_text.enabled = true; ;
        }
    }

    public void ShowBack()
    {
        cardImage.sprite = backSprite;

        if (card_text != null)
        {
            card_text.enabled = false;
        }
    }
    public void setColor()
    {
        if (color[1] == color[0])
            color[1] = "";
        if (card_type == "gate")
            print("ERROR");
        if (card_type == "normal")
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
        else if (card_type == "skip")
        {
            if (color[0] == "red" && color[1] == "")
                frontSprite = redCard_skip;
            else if (color[0] == "blue" && color[1] == "")
                frontSprite = blueCard_skip;
            else if (color[0] == "green" && color[1] == "")
                frontSprite = greenCard_skip;
            else if (color[0] == "yellow" && color[1] == "")
                frontSprite = yellowCard_skip;
            else if ((color[0] == "red" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "red"))
                frontSprite = red_yellowCard_skip;
            else if ((color[0] == "red" && color[1] == "blue") || (color[0] == "blue" && color[1] == "red"))
                frontSprite = red_blueCard_skip;
            else if ((color[0] == "red" && color[1] == "green") || (color[0] == "green" && color[1] == "red"))
                frontSprite = red_greenCard_skip;
            else if ((color[0] == "blue" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "blue"))
                frontSprite = blue_yellowCard_skip;
            else if ((color[0] == "blue" && color[1] == "green") || (color[0] == "green" && color[1] == "blue"))
                frontSprite = blue_greenCard_skip;
            else if ((color[0] == "yellow" && color[1] == "green") || (color[0] == "green" && color[1] == "yellow"))
                frontSprite = green_yellowCard_skip;
        }
        else if (card_type == "rev")
        {
            if (color[0] == "red" && color[1] == "")
                frontSprite = redCard_rev;
            else if (color[0] == "blue" && color[1] == "")
                frontSprite = blueCard_rev;
            else if (color[0] == "green" && color[1] == "")
                frontSprite = greenCard_rev;
            else if (color[0] == "yellow" && color[1] == "")
                frontSprite = yellowCard_rev;
            else if ((color[0] == "red" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "red"))
                frontSprite = red_yellowCard_rev;
            else if ((color[0] == "red" && color[1] == "blue") || (color[0] == "blue" && color[1] == "red"))
                frontSprite = red_blueCard_rev;
            else if ((color[0] == "red" && color[1] == "green") || (color[0] == "green" && color[1] == "red"))
                frontSprite = red_greenCard_rev;
            else if ((color[0] == "blue" && color[1] == "yellow") || (color[0] == "yellow" && color[1] == "blue"))
                frontSprite = blue_yellowCard_rev;
            else if ((color[0] == "blue" && color[1] == "green") || (color[0] == "green" && color[1] == "blue"))
                frontSprite = blue_greenCard_rev;
            else if ((color[0] == "yellow" && color[1] == "green") || (color[0] == "green" && color[1] == "yellow"))
                frontSprite = green_yellowCard_rev;
        }
        Debug.Log("color changed");
    }
    public void setNumText()
    {
        card_text.text = number[0].ToString();
        Debug.Log("number changed to " + number[0]);
    }
    public void Collapse()
    {
        if (color[0] != "" && color[1] != "")
        {
            int randomNum = Random.Range(0, 2);
            color[0] = color[randomNum];
            setColor();
            ShowFront();
        }
    }
    public virtual void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {

    }
}
