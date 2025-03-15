using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewNormalCard", menuName = "Cards/Normal Card")]
public class Normal_Card : Card
{
    public override void Play(ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
    }

}

