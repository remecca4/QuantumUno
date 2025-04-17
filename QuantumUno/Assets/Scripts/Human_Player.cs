using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human_Player : Player_Base
{
    void Awake() { isHuman = true; }
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
        if (topCardComponent.card_type == "skip" || topCardComponent.card_type == "rev")
            {
                if (card.color[0] == topCardComponent.color[0] || card.color[1] == topCardComponent.color[0] ||
               (topCardComponent.color[1] != "" && (card.color[0] == topCardComponent.color[1] || card.color[1] == topCardComponent.color[1])))
               {
                hasPlayableCard = true;
                break;
               }
            }
            else if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]) || card.card_type == "gate" || topCardComponent.card_type == "gate")
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
            if (topCardComponent.card_type == "skip" || topCardComponent.card_type == "rev")
            {
                if (selected.color[0] == topCardComponent.color[0] || selected.color[1] == topCardComponent.color[0] ||
               (topCardComponent.color[1] != "" && (selected.color[0] == topCardComponent.color[1] || selected.color[1] == topCardComponent.color[1])))
               {
                PlayCard(selectedCard);
                selectedCard = null;
                turnOver = true;
                break;
               }
            }
            else if (selected.color.Contains(topCardComponent.color[0]) || selected.number.Contains(topCardComponent.number[0]) || selected.card_type=="gate" || topCardComponent.card_type == "gate")
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
        if (hand.Count == 0)
        {
            GameManager.Instance.EndRound(this);
            return;
        }
        ReorganizeHand();
    }


    private void DrawCard(List<GameObject> deck)
    {
        if (deck.Count == 0)
        {
            Debug.Log("No cards left in the deck.");
            return;
        }

        GameObject drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);

        drawnCard.SetActive(true);
        drawnCard.GetComponent<Card>().ShowFront();

        Debug.Log("Human drew a card: " + drawnCard.name);

        ReorganizeHand();          // ➊ reposition *after* adding
    }


        private void ReorganizeHand()
    {
        if (hand.Count == 0) return;

        // same anchor you used in deal()
        Vector3 handAnchor = new Vector3(-3f, -6f, 0);      // bottom centre

        // width‑based gap (card width * (1 + spacingFactor))
        RectTransform rt0 = hand[0].GetComponent<RectTransform>();
        float cardW       = rt0.rect.width  * rt0.lossyScale.x;

        const float spacingFactor = 0.15f;                  // ➋ keep in‑sync
        float gapX = cardW * (1f + spacingFactor);

        for (int i = 0; i < hand.Count; ++i)
        {
            Vector3 pos = handAnchor + new Vector3(i * gapX, 0, 0);
            hand[i].GetComponent<RectTransform>().position = pos;
        }
    }
    private bool IsPlayable(Card top, Card candidate)
    {
        // Gate wildcard rules override everything
        if (candidate.card_type == "gate" || top.card_type == "gate")
            return true;

        // If top card is a special card like skip/rev
        if (top.card_type == "skip" || top.card_type == "rev")
        {
            // Only same type AND same color allowed
            return candidate.card_type == top.card_type &&
                (candidate.color[0] == top.color[0] || candidate.color[1] == top.color[0] ||
                    (top.color[1] != "" && (candidate.color[0] == top.color[1] || candidate.color[1] == top.color[1])));
        }

        // If the card is normal (e.g. number 3 green), allow match by number or color
        return candidate.number[0] == top.number[0] ||
            candidate.color[0] == top.color[0] ||
            candidate.color[1] == top.color[0] ||
            candidate.color[0] == top.color[1] ||
            candidate.color[1] == top.color[1];
    }

}
