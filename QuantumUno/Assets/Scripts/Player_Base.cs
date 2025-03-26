using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    public List<GameObject> hand;
   public virtual IEnumerator TakeTurn() { 
        yield return new WaitForSeconds(1.0f);
    }
}
