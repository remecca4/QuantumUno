using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewZGateCard", menuName = "Cards/Z Gate Card")]
public class Z_Gate_Card : Card
{
    void Start()
    {
        card_type = "gate";
    }
    public override void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        /* red=|0>, yellow=-|0>, blue=|1>, green=-|1> 
         **/

        int pile_size = discard_pile.Count;
        GameObject discard_top = discard_pile[pile_size - 1];
        Card topCardComponent = discard_top.GetComponent<Card>();
        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;

        if (discard_top.GetComponent<Card>().color[0] == "blue")
            color[0] = "green";
            
        else if (discard_top.GetComponent<Card>().color[0] == "green")
            color[0] = "blue";
        else
        {
            color[0] = topCardComponent.color[0];
        }

        if (discard_top.GetComponent<Card>().color[1] == "blue")
           color[1] = "green";
        else if (discard_top.GetComponent<Card>().color[1] == "green")
            color[1] = "blue";
        else
        {
            color[1] = topCardComponent.color[1];
        }

        number[0] = topCardComponent.number[0];
        number[1] = topCardComponent.number[1];
        setNumText();
        card_type = topCardComponent.card_type;
        setColor();
        ShowFront();
    }


}
