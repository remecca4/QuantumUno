using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCard : Card
{
   void Start()
    {
        card_type = "rev";
    } 
    public override void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
        turnOrder = -turnOrder;
        
    }
}
