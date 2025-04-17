
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Player : Player_Base
{
    void Awake() { isHuman = false; }
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
            if (topCardComponent.card_type == "skip" || topCardComponent.card_type == "rev")
            {
                if (card.color[0] == topCardComponent.color[0] || card.color[1] == topCardComponent.color[0] ||
               (topCardComponent.color[1] != "" && (card.color[0] == topCardComponent.color[1] || card.color[1] == topCardComponent.color[1])))
                {
                    PlayCard(cardObject); // Pass the GameObject, not the Card component
                    cardPlayed = true;
                    break;
                }
            }
            else if (card.color.Contains(topCardComponent.color[0]) || card.number.Contains(topCardComponent.number[0]) || card.card_type == "gate" || topCardComponent.card_type == "gate")
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

    private void PlayCard(GameObject cardObject)
    {
        Card card = cardObject.GetComponent<Card>();
        Card discardTop = GameManager.Instance.discard_pile[GameManager.Instance.discard_pile.Count - 1].GetComponent<Card>();
        discardTop.Collapse();
        if (!IsPlayable(card, discardTop))
        {
            print("DRAW2");
            for (int i = 0; i < 2; ++i)
                DrawCard(GameManager.Instance.deck);
        }
        else
        {
            // Let the card handle its own logic (e.g., ReverseCard flips turn_order)
            card.Play(ref GameManager.Instance.deck, ref GameManager.Instance.discard_pile,
                      ref GameManager.Instance.turn_order);
        }

        foreach (GameObject discardCard in GameManager.Instance.discard_pile)
        {
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

        if (hand.Count == 0)
        {
            GameManager.Instance.EndRound(this);
            return;
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
            currentCard.ShowBack();
            Debug.Log("AI drew a card.");
            ReorganizeHand();
        }
        else
        {
            Debug.Log("No cards left in the deck.");
        }
    }
    private void ReorganizeHand()
    {
        if (hand.Count == 0) return;

        
        // width‑based gap (card width * (1 + spacingFactor))
        RectTransform rt0 = hand[0].GetComponent<RectTransform>();
        float cardW = rt0.rect.width * rt0.lossyScale.x;
        float cardH = rt0.rect.height * rt0.lossyScale.y;
        const float spacingFactor = -0.30f;                  // keep in‑sync
        float gapX = cardW * (1f + spacingFactor);

        for (int i = 0; i < hand.Count; ++i)
        {
            Vector3 offset =
                   (vertical)                         // vertical piles
                       ? new Vector3(0, -i * cardH * (1f + spacingFactor), 0)
                       : new Vector3(i * cardW * (1f + spacingFactor), 0, 0);
            Vector3 pos = handAnchor + offset;
            hand[i].GetComponent<RectTransform>().position = pos;
        }
    }
    private bool IsPlayable(Card top, Card candidate)
    {
        // Gate wildcard rules override everything
        if (candidate.card_type == "gate" || top.card_type == "gate")
            return true;

        // If top card is a special card like skip/rev
        if (top.card_type == "skip" || top.card_type == "rev" || candidate.card_type=="skip"|| candidate.card_type == "rev")
        {
            // Only same type AND same color allowed
            return candidate.card_type == top.card_type &&
                (candidate.color[0] == top.color[0] || candidate.color[1] == top.color[0] ||
                    (top.color[1] != "" && (candidate.color[0] == top.color[1] || candidate.color[1] == top.color[1])));
        }

        // If the card is normal (e.g. number 3 green), allow match by number or color
        return candidate.number[0] == top.number[0] ||
            (candidate.color[0] == top.color[0] || candidate.color[1] == top.color[0] ||
                    (top.color[1] != "" && (candidate.color[0] == top.color[1] || candidate.color[1] == top.color[1])));

    }

}
