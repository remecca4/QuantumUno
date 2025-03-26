using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewHGateCard", menuName = "Cards/H Gate Card")]
public class H_Gate_Card : Card
{
    public override void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        /* 
         red=|0>, yellow=-|0>, blue=|1>, green=-|1> 
        */
        int pile_size = discard_pile.Count;
        GameObject discard_top = discard_pile[pile_size - 1];

        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;

        Card cardComponent = discard_top.GetComponent<Card>();

        // H|0> = |0> + |1>
        if (cardComponent.color[0] == "red" && cardComponent.color[1] == " ")
        {
            cardComponent.color[0] = "red";
            cardComponent.color[1] = "blue";
        }
        // H|1> = |0> - |1>
        else if (cardComponent.color[0] == "blue" && cardComponent.color[1] == " ")
        {
            cardComponent.color[0] = "red";
            cardComponent.color[1] = "green";
        }
        // H -|0> = -|0> - |1>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == " ")
        {
            cardComponent.color[0] = "yellow";
            cardComponent.color[1] = "green";
        }
        // H -|1> = -|0> + |1>
        else if (cardComponent.color[0] == "green" && cardComponent.color[1] == " ")
        {
            cardComponent.color[0] = "yellow";
            cardComponent.color[1] = "blue";
        }
        // H(|0> + |1>) = |0>
        else if (cardComponent.color[0] == "red" && cardComponent.color[1] == "blue")
        {
            cardComponent.color[0] = "red";
            cardComponent.color[1] = " ";
        }
        // H(-|0> - |1>) = -|0>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == "green")
        {
            cardComponent.color[0] = "yellow";
            cardComponent.color[1] = " ";
        }
        // H(|0> - |1>) = |1>
        else if (cardComponent.color[0] == "red" && cardComponent.color[1] == "green")
        {
            cardComponent.color[0] = "blue";
            cardComponent.color[1] = " ";
        }
        // H(-|0> + |1>) = -|1>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == "blue")
        {
            cardComponent.color[0] = "green";
            cardComponent.color[1] = " ";
        }
    }










}
