using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_Gate_Card : Card
{
    public override void Play(ref List<GameObject> deck, 
                              ref List<GameObject> discard_pile, 
                              ref int turnOrder)
    {

        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;

        if (discard_pile.Count > 0) {
            GameObject topCard = discard_pile[discard_pile.Count - 1];
            Card topCardComponent = topCard.GetComponent<Card>();

            
            if (topCardComponent.color[0] == "red") {
                color[0] = "blue";
                
            }
            else if (topCardComponent.color[0] == "yellow") {
                color[0] = "green";
                
            }
            
        }

        setColor();
        discard_pile.Add(this.gameObject);
    }
}