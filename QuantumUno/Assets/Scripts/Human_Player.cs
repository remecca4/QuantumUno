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

    // Show cards face-up
    for (int i = 0; i < hand.Count; i++)
    {
        hand[i].GetComponent<Card>().ShowFront();
    }

    // Check if there's any valid card to play
    GameObject topCard = GameManager.Instance.discard_pile[GameManager.Instance.discard_pile.Count - 1];
    Card topCardComponent = topCard.GetComponent<Card>();

    bool hasPlayableCard = false;
    foreach (GameObject cardObj in hand)
    {
        Card card = cardObj.GetComponent<Card>();
        if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]))
        {
            hasPlayableCard = true;
            break;
        }
    }

    if (!hasPlayableCard)
    {
        Debug.Log("No valid cards in hand. Drawing a card...");
        DrawCard(GameManager.Instance.deck);
        yield return new WaitForSeconds(0.5f);
        yield break; // End turn after drawing
    }

    // Enable card clicking
    EnablePlayerInput(true);

    // Wait for a valid card to be selected
    while (!turnOver)
    {
        if (selectedCard != null)
        {
            Card selected = selectedCard.GetComponent<Card>();

            if (selected.color.Contains(topCardComponent.color[0]) || selected.number.Contains(topCardComponent.number[0]))
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

    // Disable input after turn
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

        foreach (GameObject discardCard in GameManager.Instance.discard_pile) {
            discardCard.SetActive(false);
        }

        // Move card to discard pile position
        cardObject.GetComponent<RectTransform>().position = GameManager.Instance.discard_pos.transform.position;

        // Update GameManager's discard pile (centralized)
        GameManager.Instance.discard_pile.Add(cardObject);
        cardObject.SetActive(true);

        // Remove from hand
        hand.Remove(cardObject);
        Debug.Log($"Human played a card: {card.color[0]} {card.number[0]}");
        ReorganizeHand();
    }

    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count > 0)
        {
            GameObject drawnCard = deck[0];
            deck.RemoveAt(0);
            hand.Add(drawnCard);
            
            // Position the card in the player's hand
            int cardIndex = hand.Count - 1;
            Vector3 handAnchor = new Vector3(-3f, -6f, 0); // Player 1 (bottom) position
            Vector3 offset = new Vector3(cardIndex * 1.2f, 0, 0); // spread right
            drawnCard.GetComponent<RectTransform>().position = handAnchor + offset;
            
            // Make card visible and show its front face to the player
            drawnCard.SetActive(true);
            drawnCard.GetComponent<Card>().ShowFront();
            
            Debug.Log("Human drew a card: " + drawnCard.name);
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
        ReorganizeHand();
    }

    private void ReorganizeHand()
    {
        Vector3 handAnchor = new Vector3(-3f, -6f, 0); // Player 1 (bottom) position
        
        for (int i = 0; i < hand.Count; i++)
        {
            Vector3 offset = new Vector3(i * 1.2f, 0, 0); // spread right
            hand[i].GetComponent<RectTransform>().position = handAnchor + offset;
        }
    }
}
