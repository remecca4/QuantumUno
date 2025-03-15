using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewyGateCard", menuName = "Cards/Y Gate Card")]
public class Y_Gate_Card : Card
{
    public override void Play(ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
      
    }


}
