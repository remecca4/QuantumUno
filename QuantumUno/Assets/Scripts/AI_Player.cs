using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Player : Player_Base
{
    public override IEnumerator TakeTurn()
    {
        Debug.Log("AI is thinking...");
        yield return new WaitForSeconds(1.0f); // Simulate thinking time

        // Access the top card from GameManager
        GameObject topCard = GameManager.Instance.discard_pile[GameManager.Instance.discard_pile.Count - 1];
        Card topCardComponent = topCard.GetComponent<Card>(); // Get the Card component
        bool cardPlayed = false;

        // Check for a valid card to play
        foreach (GameObject cardObject in hand)
        {
            Card card = cardObject.GetComponent<Card>(); // Get the Card component
        
            if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]))
            {
                PlayCard(cardObject); // Pass the GameObject, not the Card component
                cardPlayed = true;
                break;
            }
        }

        // If no valid card, draw a card
        if (!cardPlayed)
        {
            DrawCard(GameManager.Instance.deck);
        }

        Debug.Log("AI's turn is over.");
    }

    private void PlayCard(GameObject cardObject) {
        Card card = cardObject.GetComponent<Card>();
        
        // Let the card handle its own logic (e.g., ReverseCard flips turn_order)
        card.Play(ref GameManager.Instance.deck, ref GameManager.Instance.discard_pile, 
                  ref GameManager.Instance.turn_order);

        
        foreach (GameObject discardCard in GameManager.Instance.discard_pile) {
            discardCard.SetActive(false);
        }

        card.ShowFront();

        // Move card to discard pile position
        cardObject.GetComponent<RectTransform>().position = GameManager.Instance.discard_pos.transform.position;
        
        // Update GameManager's discard pile (centralized)
        GameManager.Instance.discard_pile.Add(cardObject);
        cardObject.SetActive(true); // Show the card
        
        // Remove from hand
        hand.Remove(cardObject);
        Debug.Log($"AI played a card: {card.color[0]} {card.number[0]}");
    }

    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count > 0)
        {
            GameObject drawnCard = deck[0];
            Card currentCard = drawnCard.GetComponent<Card>();
            deck.RemoveAt(0);
            hand.Add(drawnCard);
            currentCard.ShowBack();
            Debug.Log("AI drew a card.");
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }
    
}