using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewNormalCard", menuName = "Cards/Normal Card")]
public class Normal_Card : Card
{
   
    public List<string> color;
    public List<int> number;

    public void Initialize(List<string> color_in, List<int> number_in, Sprite sprite)
    {
        type = "normal";  
        color = color_in;
        number = number_in;
        cardSprite = sprite;
    }

}

