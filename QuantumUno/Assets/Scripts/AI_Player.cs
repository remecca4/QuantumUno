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
            GameObject cardGameObject = card.gameObject;
            if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]))
            {
                PlayCard(cardGameObject); // Pass the GameObject, not the Card component
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

    private void PlayCard(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<Card>(); // Get the Card component
        card.Play(ref GameManager.Instance.deck, ref GameManager.Instance.discard_pile, ref GameManager.Instance.turn_order);
        hand.Remove(cardObject);
        GameManager.Instance.UpdateTopCard(cardObject); // Update top card in GameManager

        Debug.Log($"AI played a card: {card.color[0]} {card.number[0]}");

        // Handle special cards
        if (card is ReverseCard)
        {
            GameManager.Instance.ReverseTurnOrder();
        }
        else if (card is SkipCard)
        {
            GameManager.Instance.SkipNextPlayer();
        }
    }

    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count > 0)
        {
            GameObject drawnCard = deck[0];
            Card currentCard = drawnCard.GetComponent<Card>();
            deck.RemoveAt(0);
            hand.Add(drawnCard);
            Debug.Log("AI drew a card.");
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }
    
}