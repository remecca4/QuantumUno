using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Player : Player_Base
{
    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TakeTurn(ref GameObject topcard, ref GameObject topDiscard, ref int turnOrder)
    {
        Debug.Log("AI is thinking...");
        yield return new WaitForSeconds(1.0f); // Simulate thinking time

        // Access the Card component of the top card
        Card topCardComponent = (Card)topCard.GetComponent(typeof(Card));
        bool cardPlayed = false;

        // Check for a valid card to play
        foreach (GameObject cardObject in hand)
        {
            Card card = (Card)cardObject.GetComponent(typeof(Card));
            if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]))
            {
                PlayCard(cardObject);
                cardPlayed = true;
                break;
            }
        }

        // If no valid card, draw a card
        if (!cardPlayed)
        {
            DrawCard(deck);
        }

        Debug.Log("AI's turn is over.");
    }

    private void PlayCard(GameObject cardObject)
    {
        Card card = (Card)cardObject.GetComponent(typeof(Card));
        card.Play();
        hand.Remove(cardObject);
        GameManager.Instance.discard_pile.Add(cardObject);

        Debug.Log($"AI played a card: {card.color[0]} {card.number[0]}");

        // Handle special cards
        if (card is WildCard)
        {
            string chosenColor = ChooseColor();
            Debug.Log($"AI chose {chosenColor} as the new color.");

            //update the top cards color to the chosenColor by the player
            GameObject topCard = GameManager.Instance.discard_pile[GameManager.Instance.discard_pile.Count - 1];
            Card topCardComponent = (Card)topCard.GetComponent(typeof(Card));
            topCardComponent.color[0] = chosenColor; 
        }
        else if (card is ReverseCard)
        {
            GameManager.Instance.ReverseTurnOrder();
            Debug.Log("AI reversed the turn order.");
        }
        else if (card is SkipCard)
        {
            GameManager.Instance.SkipNextPlayer();
            Debug.Log("AI skipped the next player.");
        }
    }

    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count > 0)
        {
            GameObject drawnCard = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawnCard);
            Debug.Log("AI drew a card.");
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }

    private string ChooseColor()
    {
        Dictionary<string, int> colorCounts = new Dictionary<string, int>();
        foreach (GameObject cardObject in hand)
        {
            Card card = (Card)cardObject.GetComponent(typeof(Card));
            if (card.color.Count > 0)
            {
                string color = card.color[0];
                if (colorCounts.ContainsKey(color))
                {
                    colorCounts[color]++;
                }
                else
                {
                    colorCounts[color] = 1;
                }
            }
        }

        string chosenColor = "red";
        int maxCount = 0;
        foreach (var pair in colorCounts)
        {
            if (pair.Value > maxCount)
            {
                chosenColor = pair.Key;
                maxCount = pair.Value;
            }
        }

        return chosenColor;
    }
}
