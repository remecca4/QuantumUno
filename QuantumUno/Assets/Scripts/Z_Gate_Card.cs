using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewZGateCard", menuName = "Cards/Z Gate Card")]
public class Z_Gate_Card : Card
{
    public override void Play(ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
        
    }

}
