using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{

    public bool isHuman = false;
    public List<GameObject> hand;
    public Vector3 handAnchor;
    public bool vertical;
   public virtual IEnumerator TakeTurn() { 
        yield return new WaitForSeconds(1.0f);
    }
}
