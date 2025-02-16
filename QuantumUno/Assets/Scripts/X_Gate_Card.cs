using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewXGateCard", menuName = "Cards/X Gate Card")]
public class X_Gate_Card : Card
{

    public void Initialize(Sprite sprite)
    {
        type = "wild";
        cardSprite = sprite;

    }


}
