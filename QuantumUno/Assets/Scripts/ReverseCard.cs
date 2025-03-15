using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "ReverseCard", menuName = "Cards/Reverse Card")]
public class ReverseCard : Card
{
    public override void Play(ref GameObject topcard, ref GameObject topDiscard,  ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
        turnOrder = -turnOrder;
        
    }
}
