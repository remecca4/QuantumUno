using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public List<GameObject> deck;
    public List<GameObject> discard_pile;
    public List<GameObject> players;
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
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public Transform deck_pos;
    private int currentPlayerIndex = 0;
    public GameObject discard_pos;
    //for reverse cards cw: player index increases, ccw: player index decreases
    public int turn_order = 1;


    // Start is called before the first frame update
    void Start()
    {
        setup_players();
        initialize_deck();
        
        StartCoroutine(SetupGame());


    } // Start()

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
        else
        {
            Instance = this;
            // Optional: Uncomment if GameManager should persist across scenes
            // DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator SetupGame()
    {
        // Shuffle with potential animation
        shuffle();
        Debug.Log("Shuffling...");
        yield return new WaitForSeconds(1.5f); // Pause for shuffle animation

        // Deal after shuffle
        deal();
        Debug.Log("Dealing cards...");

        while (true)
        {
            GameObject firstCard = deck[deck.Count - 1];
            firstCard.GetComponent<RectTransform>().position = discard_pos.transform.position;
            firstCard.GetComponent<Card>().ShowFront();
            discard_pile.Add(firstCard);
            deck.RemoveAt(deck.Count - 1);
            if (firstCard.GetComponent<Card>().card_type != "gate")
                break;
        }
        StartCoroutine(GameLoop());
    } // SetupGame()

    IEnumerator GameLoop()
    {
        bool gameOver = false;
        while (!gameOver)
        {
            Debug.Log(currentPlayerIndex);
            Debug.Log(currentPlayerIndex%players.Count);
            Player_Base currentPlayer = players[currentPlayerIndex%players.Count].GetComponent<Player_Base>();
            Debug.Log($"Player {currentPlayerIndex % players.Count + 1}'s turn");
            
            //add this coroutine to player classes or game manager?
            yield return StartCoroutine(currentPlayer.TakeTurn());
  
            // Check for win condition
            // if (currentPlayer.hand.Count==0)
            // {
            //     bool human = currentPlayer is Human_Player;
            //     GameResult.humanWon  = human; 
            //     GameResult.playScene = SceneManager.GetActiveScene().name;
            //     SceneManager.LoadScene("End_Screen");
            //     yield break;    
            // }
            if (currentPlayer.hand.Count == 0)
            {
                GameResult.humanWon  = currentPlayer.isHuman; 
                GameResult.playScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("End_Screen");
                yield break;
            }

            currentPlayerIndex += turn_order;
            
            if (currentPlayerIndex < 0)
            {
                currentPlayerIndex = players.Count-1;
            }
            currentPlayerIndex %= players.Count;

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
                red.GetComponent<Normal_Card>().card_text = red.GetComponentInChildren<TextMeshProUGUI>();
                red.GetComponent<Normal_Card>().card_text.text = num.ToString();
                red.GetComponent<Normal_Card>().ShowBack();
                

                GameObject blue = Instantiate(norm_card_blue, deck_pos);
                blue.GetComponent<Normal_Card>().number[0] = num;
                blue.GetComponent<Normal_Card>().card_text = blue.GetComponentInChildren<TextMeshProUGUI>();
                blue.GetComponent<Normal_Card>().card_text.text = num.ToString();
                blue.GetComponent<Normal_Card>().ShowBack();
                blue.SetActive(true);

                GameObject yellow = Instantiate(norm_card_yellow, deck_pos);
                yellow.GetComponent<Normal_Card>().number[0] = num;
                yellow.GetComponent<Normal_Card>().card_text = yellow.GetComponentInChildren<TextMeshProUGUI>();
                yellow.GetComponent<Normal_Card>().card_text.text = num.ToString();
                yellow.GetComponent<Normal_Card>().ShowBack();
                yellow.SetActive(true);

                GameObject green = Instantiate(norm_card_green, deck_pos);
                green.GetComponent<Normal_Card>().number[0] = num;
                green.GetComponent<Normal_Card>().card_text = green.GetComponentInChildren<TextMeshProUGUI>();
                green.GetComponent<Normal_Card>().card_text.text = num.ToString();
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
    void setup_players()
    {
        players.Add(player1);
        players.Add(player2);
        players.Add(player3);
        players.Add(player4);
    }
    // fisher-yates shuffle algorithm
    // iteratres through the list and swappes each element with a 
    // randomly chosen element from the remaining unshuffled portion of the list
    void shuffle()
    {
        for(int i = deck.Count - 1; i > 0; i--){
            int randomIndex = Random.Range(0, i + 1);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        Debug.Log("Deck shuffled");
    } // shuffle()

    // distribute cards to each plaer at beginning of game
    // determine how many cards to deal based on the number of players


    const float HUMAN_SPACING_FACTOR = 0.15f;
    const float AI_SPACING_FACTOR    = -0.30f;
    void deal()
    {
        int numberOfCardsPerPlayer = 7;

        Vector3[] handAnchors = new Vector3[] {
            new Vector3(-6f,-6f,0),   // P1 (bottom) → spread right
            new Vector3(-12f, 5f,0),  // P2 (left)   → stack down
            new Vector3(-4f, 6f,0),   // P3 (top)    → spread right
            new Vector3(12f, 5f,0)    // P4 (right)  → stack down
        };

        for (int p = 0; p < players.Count; ++p)
        {
            Player_Base player = players[p].GetComponent<Player_Base>();
            Vector3 anchor = handAnchors[p];
            bool isHuman = (p == 0);               // bottom player
            float hFactor = isHuman ? HUMAN_SPACING_FACTOR : AI_SPACING_FACTOR;

            for (int j = 0; j < numberOfCardsPerPlayer && deck.Count > 0; ++j)
            {
                GameObject cardGO = deck[0];
                deck.RemoveAt(0);
                player.hand.Add(cardGO);

                RectTransform rt = cardGO.GetComponent<RectTransform>();
                float cardW = rt.rect.width  * rt.lossyScale.x;
                float cardH = rt.rect.height * rt.lossyScale.y;

                Vector3 offset =
                    (p == 1 || p == 3)                         // vertical piles
                        ? new Vector3(0, -j * cardH * (1f + hFactor), 0)
                        : new Vector3(j * cardW * (1f + hFactor), 0, 0);

                rt.position = anchor + offset;
                cardGO.SetActive(true);
            }
        }
    }


        
    // }

}

