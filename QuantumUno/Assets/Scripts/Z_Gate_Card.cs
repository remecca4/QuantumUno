using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "NewZGateCard", menuName = "Cards/Z Gate Card")]
public class Z_Gate_Card : Card
{
    public override void Play(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {
        /* red=|0>, yellow=-|0>, blue=|1>, green=-|1> 
         **/
        int pile_size = discard_pile.Count;
        GameObject discard_top = discard_pile[pile_size - 1];

        if (turnOrder > 1 || turnOrder < -1)
            turnOrder /= 2;

        if (discard_top.GetComponent<Card>().color[0] == "blue")
            discard_top.GetComponent<Card>().color[0] = "green";
        else if (discard_top.GetComponent<Card>().color[0] == "green")
            discard_top.GetComponent<Card>().color[0] = "blue";

        if (discard_top.GetComponent<Card>().color[1] == "blue")
            discard_top.GetComponent<Card>().color[1] = "green";
        else if (discard_top.GetComponent<Card>().color[1] == "green")
            discard_top.GetComponent<Card>().color[1] = "blue";
    }

}
