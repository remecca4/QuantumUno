using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkipCard", menuName = "Cards/Skip Card")]
public class SkipCard : Card
{
    public void Initialize(Sprite sprite)
    {
        type = "wild";
        cardSprite = sprite;
    }


}

