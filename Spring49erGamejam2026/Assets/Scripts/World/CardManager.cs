using Ginput;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this);
        }
        instance = this;
    }

    //public information
    [Header("Card Textures")]
    [SerializeField] Sprite[] cards;
    public TextMeshProUGUI card_count;
    public Sprite blank;

    [Header("Card Objects")]
    public Image active;
    public Image backup;
    public Image[] next_cards;
    public int max_cards;

    [Header("Card Attacks")]
    [SerializeField] GameObject[] attacks;


    //the number of the current active card
    /*
     * 1 - Spade
     * 2 - Club
     * 3 - Heart
     * 4 - Diamond
     */

    private int active_attack;
    private int backup_attack;
    private int[] deck;
    private int cards_left;

    //timer for attack cooldown
    private float attack_delay;

    //swap boolean
    private bool swapped;

    private void Start()
    {
        //initialize number of cards and display it
        cards_left = 3;
        card_count.text = cards_left.ToString();

        //initialize the backup to be empty
        backup_attack = -1;
        backup.sprite = blank;

        //initialize the first active card
        active_attack = Random.Range(0, 4);
        active.sprite = cards[active_attack];

        //initialize the deck
        deck = new int[3];
        for(int i = 0; i < 3; i++)
        {
            //pick a random one for each, and change the texture
            deck[i] = Random.Range(0, 4);
            next_cards[i].sprite = cards[deck[i]];
        }

        //initialize timer
        attack_delay = 1f;

        //initialize other variables
        swapped = false;
    }

    private void Update()
    {
        //update timer
        if(attack_delay > 0)
        {
            attack_delay -= Time.deltaTime;
        }
    }

    public void ThrowCard()
    {
        //only do it if we can
        if(attack_delay < 0)
        {
            //reset timer
            attack_delay = 2f;

            //use card
            if (attacks[active_attack] != null)
            {
                GameObject go = Instantiate(attacks[active_attack]);
                go.transform.position = MagicianController.instance.player_obj.transform.position;
            }

            //update card information if there are cards left//
            if(cards_left > 0)
            {
                UpdateCards();
            }
            else if(backup_attack != -1)
            {
                //autofill the backup if its the last card left
                active_attack = backup_attack;
                active.sprite = cards[active_attack];

                backup_attack = -1;
                backup.sprite = blank;
            }
            else
            {
                //remove top card
                active_attack = -1;
                active.color = new Color(1f, 1f, 1f, 0f);
            }

            //update the swap value if needed
            if (swapped)
            {
                swapped = false;
            }
        }
    }

    //function to do a single cycle
    private void UpdateCards()
    {
        //update the count
        cards_left -= 1;
        card_count.text = cards_left.ToString();

        //set the active attack to top of deck
        active_attack = deck[0];
        active.sprite = cards[active_attack];
        active.color = new Color(1f, 1f, 1f, 1f);

        //move each up one
        for (int i = 0; i < deck.Length-1; i++)
        {
            //only do it if theres enough cards in the deck
            if (i + 1 <= cards_left)
            {
                deck[i] = deck[i + 1];
                next_cards[i].sprite = cards[deck[i]];
            }
            else
            {
                //remove the card from the deck and set it to blank
                deck[i] = -1;
                next_cards[i].color = new Color(1f, 1f, 1f, 0f);
            }
        }

        //choose new bottom card
        if (cards_left >= 3)
        {
            deck[deck.Length - 1] = Random.Range(0, 4);
            next_cards[next_cards.Length - 1].sprite = cards[deck[deck.Length - 1]];
        }
        else
        {
            //remove the card from the deck and set it to blank
            deck[deck.Length - 1] = -1;
            next_cards[deck.Length - 1].color = new Color(1f, 1f, 1f, 0f);
        }
    }

    //function to swap the backup card
    public void SwapBackup()
    {
        //only do it if you haven't already swapped before attacking, and have a card to fill the slot
        if (!swapped)
        {
            if (active_attack != -1 && backup_attack != -1) //if there is a card in the attack slot and backup slot
            {
                int temp = active_attack;

                active_attack = backup_attack;
                active.sprite = cards[active_attack];

                backup_attack = temp;
                backup.sprite = cards[backup_attack];
            }
            else if (backup_attack == -1 && active_attack != -1 && cards_left > 0) //if theres only a card in the attack slot
            {
                backup_attack = active_attack;
                backup.sprite = cards[backup_attack];
                UpdateCards();
            }

            //you have now swapped
            swapped = true;
        }
    }

    //function to add a new card
    public void AddCard()
    {
        //dont add a card if you hit your max
        if(cards_left >= max_cards)
        {
            return;
        }

        //if there is no card in the active slot immediately put it there
        if(active_attack == -1)
        {
            active_attack = Random.Range(0, 4);
            active.sprite = cards[active_attack];
            active.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            //add the card
            cards_left += 1;
            card_count.text = cards_left.ToString();

            //restock the deck if you need to
            for (int i = 0; i < deck.Length; i++)
            {
                //first card it finds thats blank
                if (deck[i] == -1)
                {
                    //fill in that card and break the loop
                    deck[i] = Random.Range(0, 4);
                    next_cards[i].sprite = cards[deck[i]];
                    next_cards[i].color = new Color(1f, 1f, 1f, 1f);
                    i = deck.Length;
                }
            }
        }
    }
}
