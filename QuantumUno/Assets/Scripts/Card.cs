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
    void Awake()
    {
        cardImage = GetComponent<Image>();
       
    }

    public void ShowFront()
    {
        cardImage.sprite = frontSprite;
    }

    public void ShowBack()
    {
        cardImage.sprite = backSprite;
    }
    public virtual void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
       
    }
}
