using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> deck;
    public Normal_Card norm_card_red;
    public Normal_Card norm_card_blue;
    public Normal_Card norm_card_green;
    public Normal_Card norm_card_yellow;
    public ReverseCard rev_card;
    public SkipCard skip_card;
    public X_Gate_Card x_card;
    public Y_Gate_Card y_card;
    public Z_Gate_Card z_card;
    public H_Gate_Card h_card;

    // Start is called before the first frame update
    void Start()
    {

        initialize_deck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void initialize_deck()
    {
        for(int num=0; num<10; num++)
        {
                for (int x = 0; x < 2; ++x)
                {
                Normal_Card red = ScriptableObject.CreateInstance<Normal_Card>();
                red.Initialize(new List<string> { "Red" }, new List<int> { num },norm_card_red.cardSprite);

                Normal_Card blue = ScriptableObject.CreateInstance<Normal_Card>();
                blue.Initialize(new List<string> { "Blue" }, new List<int> { num }, norm_card_blue.cardSprite);

                Normal_Card yellow = ScriptableObject.CreateInstance<Normal_Card>();
                yellow.Initialize(new List<string> { "Yellow" }, new List<int> { num }, norm_card_yellow.cardSprite);

                Normal_Card green = ScriptableObject.CreateInstance<Normal_Card>();
                green.Initialize(new List<string> { "Green" }, new List<int> { num }, norm_card_green.cardSprite);

                deck.Add(red);
                deck.Add(blue);
                deck.Add(green);
                deck.Add(yellow);


            }
            
        }
        for(int i=0; i<5; ++i)
        {
            ReverseCard rev = ScriptableObject.CreateInstance<ReverseCard>();
            rev.Initialize(rev_card.cardSprite);
            deck.Add(rev);
            SkipCard skip = ScriptableObject.CreateInstance<SkipCard>();
            skip.Initialize(skip_card.cardSprite);
            deck.Add(skip);
            X_Gate_Card x = ScriptableObject.CreateInstance<X_Gate_Card>();
            x.Initialize(x_card.cardSprite);
            deck.Add(x);
            Y_Gate_Card y = ScriptableObject.CreateInstance<Y_Gate_Card>();
            y.Initialize(y_card.cardSprite);
            deck.Add(y);
            Z_Gate_Card z = ScriptableObject.CreateInstance<Z_Gate_Card>();
            z.Initialize(z_card.cardSprite);
            deck.Add(z);
            H_Gate_Card h = ScriptableObject.CreateInstance<H_Gate_Card>();
            h.Initialize(h_card.cardSprite);
            deck.Add(h);

        }

    }
}
