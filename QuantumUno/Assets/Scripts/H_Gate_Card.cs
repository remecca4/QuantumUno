using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Gate_Card : Card
{ 
    void Start()
    {
        card_type = "gate";
    }
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
        if (cardComponent.color[0] == "red" && cardComponent.color[1] == "")
        {
            color[0] = "red";
            color[1] = "blue";
        }
        // H|1> = |0> - |1>
        else if (cardComponent.color[0] == "blue" && cardComponent.color[1] == "")
        {
            color[0] = "red";
            color[1] = "green";
        }
        // H -|0> = -|0> - |1>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == "")
        {
            color[0] = "yellow";
            color[1] = "green";
        }
        // H -|1> = -|0> + |1>
        else if (cardComponent.color[0] == "green" && cardComponent.color[1] == "")
        {
            color[0] = "yellow";
            color[1] = "blue";
        }
        // H(|0> + |1>) = |0>
        else if (cardComponent.color[0] == "red" && cardComponent.color[1] == "blue")
        {
            color[0] = "red";
            color[1] = "";
        }
        // H(-|0> - |1>) = -|0>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == "green")
        {
            color[0] = "yellow";
            color[1] = "";
        }
        // H(|0> - |1>) = |1>
        else if (cardComponent.color[0] == "red" && cardComponent.color[1] == "green")
        {
            color[0] = "blue";
            color[1] = "";
        }
        // H(-|0> + |1>) = -|1>
        else if (cardComponent.color[0] == "yellow" && cardComponent.color[1] == "blue")
        {
            color[0] = "green";
            color[1] = "";
        }
        else
        {
            color[0] = cardComponent.color[0];
            color[1] = cardComponent.color[1];
        }
        number[0] = cardComponent.number[0];
        number[1] = cardComponent.number[1];
        setNumText();
        card_type = cardComponent.card_type;
        setColor();
        ShowFront();
    }










}
