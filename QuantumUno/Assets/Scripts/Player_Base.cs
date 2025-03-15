using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    public List<Card> hand;
   public virtual IEnumerator TakeTurn( ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {

    }
}
