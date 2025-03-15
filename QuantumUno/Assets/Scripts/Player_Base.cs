using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    public List<Card> hand;
   public virtual IEnumerator TakeTurn(ref List<GameObject> deck, ref List<GameObject> discard_pile, ref int turnOrder)
    {

    }
}
