using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    // normal, wild, superposition, entangled
    public string type;
    public Sprite cardSprite;
    public void Initialize(string type, Sprite sprite)
    {
        this.type = type;
        cardSprite = sprite;
    }

}
