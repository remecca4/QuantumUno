using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Player : Player_Base
{

    public override IEnumerator TakeTurn()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
