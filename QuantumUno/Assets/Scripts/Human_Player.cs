using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human_Player : Player_Base
{
    private GameObject selectedCard;

    public override IEnumerator TakeTurn()
    {
        Debug.Log("Human player's turn. Waiting for input...");
        bool turnOver = false;

        // Enable input for player actions
        EnablePlayerInput(true);
        for(int i=0;i<hand.Count;i++)
        {
            hand[i].GetComponent<Card>().ShowFront();
        }
        while (!turnOver)
        {
            if (selectedCard != null)
            {
                Card card = selectedCard.GetComponent<Card>();
                GameObject topCard = GameManager.Instance.discard_pile[GameManager.Instance.discard_pile.Count - 1];
                Card topCardComponent = topCard.GetComponent<Card>();

                // Check if the selected card is valid
                if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]))
                {
                    PlayCard(selectedCard);
                    selectedCard = null;
                    turnOver = true;
                    break;
                }
                else
                {
                    Debug.Log("Invalid card selection. Try again.");
                    selectedCard = null;
                }
            }

            yield return null;
        }

        // If no valid card was played, draw automatically
        if (!turnOver)
        {
            DrawCard(GameManager.Instance.deck);
            turnOver = true;
        }

        // Disable player input after turn
        EnablePlayerInput(false);

        Debug.Log("Human player's turn is over.");
    }

    private void EnablePlayerInput(bool enable)
    {
        foreach (GameObject cardObject in hand)
        {
            Button cardButton = cardObject.GetComponent<Button>();
            if (cardButton != null)
            {
                cardButton.interactable = enable;
                if (enable)
                {
                    cardButton.onClick.AddListener(() => OnCardClicked(cardObject));
                }
                else
                {
                    cardButton.onClick.RemoveAllListeners();
                }
            }
        }
    }

    private void OnCardClicked(GameObject cardObject)
    {
        Debug.Log("Card clicked: " + cardObject.name);
        selectedCard = cardObject;
    }

    private void PlayCard(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<Card>();

        // Let the card handle its own logic (e.g., ReverseCard flips turn_order)
        card.Play(ref GameManager.Instance.deck, ref GameManager.Instance.discard_pile,
                  ref GameManager.Instance.turn_order);

        // Update GameManager's discard pile (centralized)
        GameManager.Instance.discard_pile.Add(cardObject);
        cardObject.SetActive(true);

        // Remove from hand
        hand.Remove(cardObject);
        Debug.Log($"Human played a card: {card.color[0]} {card.number[0]}");
    }

    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count > 0)
        {
            GameObject drawnCard = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawnCard);
            drawnCard.SetActive(false);  // Keep it hidden initially
            Debug.Log("Human drew a card: " + drawnCard.name);
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }
}
