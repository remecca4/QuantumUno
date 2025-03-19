using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<GameObject> deck;
    public List<GameObject> discard_pile;
    public List<Player_Base> players;
    public GameObject norm_card_red;
    public GameObject norm_card_blue;
    public GameObject norm_card_green;
    public GameObject norm_card_yellow;
    public GameObject rev_card;
    public GameObject skip_card;
    public GameObject x_card;
    public GameObject y_card;
    public GameObject z_card;
    public GameObject h_card;
    public Transform deck_pos;
    private int currentPlayerIndex = 0;
    //for reverse cards cw: player index increases, ccw: player index decreases
    public int turn_order = 1;

    // Start is called before the first frame update
    void Start()
    {
        initialize_deck();
        //StartCoroutine(SetupGame());


    } // Start()

    IEnumerator SetupGame()
    {
        // Shuffle with potential animation
        shuffle();
        Debug.Log("Shuffling...");
        yield return new WaitForSeconds(1.5f); // Pause for shuffle animation

        // Deal after shuffle
        deal();
        Debug.Log("Dealing cards...");
    } // SetupGame()

    IEnumerator GameLoop()
    {
        bool gameOver = false;
        while (!gameOver)
        {
            Player_Base currentPlayer = players[currentPlayerIndex];
            Debug.Log($"Player {currentPlayerIndex + 1}'s turn");
            
            //add this coroutine to player classes or game manager?
            yield return StartCoroutine(currentPlayer.TakeTurn(deck,discard_pile,turn_order));
  
            // Check for win condition
            if (currentPlayer.hand.Count==0)
            {
                Debug.Log($"Player {currentPlayerIndex + 1} wins!");
                gameOver = true;
                break;
            } // if

            currentPlayerIndex += turn_order;

            yield return new WaitForSeconds(0.5f); // Small delay between turns
        } // while

    } // GameLoop()

    void initialize_deck()
    {
        // Adds 2 of each normal card 0-9 and 5 of each wild card to the deck and makes the cards visible in the center
        for (int num = 0; num < 10; num++)
        {
            for (int x = 0; x < 2; ++x)
            {
                GameObject red = Instantiate(norm_card_red,deck_pos);
                red.SetActive(true);
                red.GetComponent<Normal_Card>().number[0] = num;
                red.GetComponent<Normal_Card>().ShowBack();

                GameObject blue = Instantiate(norm_card_blue, deck_pos);
                blue.GetComponent<Normal_Card>().number[0] = num;
                blue.GetComponent<Normal_Card>().ShowBack();
                blue.SetActive(true);

                GameObject yellow = Instantiate(norm_card_yellow, deck_pos);
                yellow.GetComponent<Normal_Card>().number[0] = num;
                yellow.GetComponent<Normal_Card>().ShowBack();
                yellow.SetActive(true);

                GameObject green = Instantiate(norm_card_green, deck_pos);
                green.GetComponent<Normal_Card>().number[0] = num;
                green.GetComponent<Normal_Card>().ShowBack();
                green.SetActive(true);

                deck.Add(red);
                deck.Add(blue);
                deck.Add(green);
                deck.Add(yellow);
            } //for   
        } //for 

        for(int i = 0; i < 5; ++i)
        {
            GameObject rev = Instantiate(rev_card, deck_pos);
            rev.SetActive(true);
            rev.GetComponent<ReverseCard>().ShowBack();
            deck.Add(rev);

            GameObject skip = Instantiate(skip_card,deck_pos);
            skip.GetComponent<SkipCard>().ShowBack();
            skip.SetActive(true);
            deck.Add(skip);

            GameObject x = Instantiate(x_card,deck_pos);
            x.SetActive(true);
            x.GetComponent<X_Gate_Card>().ShowBack();
            deck.Add(x);

            GameObject y = Instantiate(y_card, deck_pos);
            y.GetComponent<Y_Gate_Card>().ShowBack();
            y.SetActive(true);
            deck.Add(y);

            GameObject z = Instantiate(z_card, deck_pos);
            z.GetComponent<Z_Gate_Card>().ShowBack();
            z.SetActive(true);
            deck.Add(z);

            GameObject h = Instantiate(h_card, deck_pos);
            h.SetActive(true);
            h.GetComponent<H_Gate_Card>().ShowBack();
            deck.Add(h);

        } // for
    } // initialize_deck()

    // fisher-yates shuffle algorithm
    // iteratres through the list and swappes each element with a 
    // randomly chosen element from the remaining unshuffled portion of the list
    void shuffle()
    {
        for(int i = deck.Count - 1; i > 0; i--){
            int randomIndex = randomIndex.Range(0, i + 1);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        Debug.Log("Deck shuffled");
    } // shuffle()

    // distribute cards to each plaer at beginning of game
    // determine how many cards to deal based on the number of players
    void deal()
    {
        int numberOfCardsPerPlayer = 7; // Standard UNO starting hand size
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < numberOfCardsPerPlayer; j++)
            {
                if (deck.Count > 0)
                {
                    GameObject card = deck[0];
                    deck.RemoveAt(0);
                    players[i].hand.Add(card);
                    card.SetActive(false); // Hide the card in the player's hand
                }
                else
                {
                    Debug.LogWarning("Not enough cards in the deck to deal.");
                    break;
                }
            }
        }
        Debug.Log("Cards dealt to players.");
    } // deal()
}

