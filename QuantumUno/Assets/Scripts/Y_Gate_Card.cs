using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewyGateCard", menuName = "Cards/Y Gate Card")]

// take top card off of the discard pile, get the card object and look at the color
// following Y gate behavior in QM
public class Y_Gate_Card : Card
{
    public override void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        if (turnOrder > 1 || turnOrder < -1) turnOrder /= 2;

        GameObject topCard = discard_pile[discard_pile.Count - 1];
        Card cardComponent = topCard.GetComponent<cardComponent>();
        
        if(cardComponent != null && cardComponent.color[0]){
            string currentColor = cardComponent.color[0];

            // flip the color and add a phase change
            switch (currentColor)
            {
                case "red":
                    cardComponent.color[0] = "blue"; //|0> to |1>
                    break;
                case "blue":
                    cardComponent.color[0] = "yellow"; // |1> to -|0>
                    break;
                case "yellow":
                    cardComponent.color[0] = "green"; // -|0> to -|1>
                    break;
                case "green":
                    cardComponent.color[0] = "red"; // -|1> to |0>
                    break;
            }

            //for super position card
            currentColor = cardComponent.color[1];
            switch (currentColor)
            {
                case "red":
                    cardComponent.color[1] = "blue"; //|0> to |1>
                    break;
                case "blue":
                    cardComponent.color[1] = "yellow"; // |1> to -|0>
                    break;
                case "yellow":
                    cardComponent.color[1] = "green"; // -|0> to -|1>
                    break;
                case "green":
                    cardComponent.color[1] = "red"; // -|1> to |0>
                    break;
            }

            Debug.Log($"Y Gate applied! Color changed from {currentColor} to {cardComponent.color[0]}");

        }
        else
        {
            Debug.LogWarning("Top card in discard pile has no valid color.");
        }
    }
}