using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "SkipCard", menuName = "Cards/Skip Card")]
public class SkipCard : Card
{
    void Start()
    {
        card_type = "skip";
    }
    public override void  Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;
        turnOrder *=2;
        
    }

}

