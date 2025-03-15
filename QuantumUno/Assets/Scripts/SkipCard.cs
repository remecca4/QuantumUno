using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "SkipCard", menuName = "Cards/Skip Card")]
public class SkipCard : Card
{
    public override void  Play(ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
        turnOrder *=2;
        
    }

}

