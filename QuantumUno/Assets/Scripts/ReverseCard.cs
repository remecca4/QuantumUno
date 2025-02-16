using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ReverseCard", menuName = "Cards/Reverse Card")]
public class ReverseCard : Card
{
    public void Initialize(Sprite sprite)
    {
        type = "wild";
        cardSprite = sprite;
    }

}
