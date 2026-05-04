using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public event System.Action OnTakeDamage;

    public Deck deck;
    public List<CardData> cardsForDeck = new List<CardData>();
    public int health = 3;
    public List<CardData> hand = new List<CardData>();
    public Sprite cardBack;
    private List<SpriteRenderer> healthIcons;

    private void Awake()
    {
        deck = new Deck(cardsForDeck);
        deck.Shuffle();
        for(int i = 0; i <= 5; i++)
        {
            hand.Add(deck.Draw());
        }
        healthIcons = transform.GetComponentsInChildren<SpriteRenderer>().ToList();
    }

    public CardData MakeTurn()
    {
        if (hand.Count == 0) GameManager.Instance.DrawCardsEnemy(6);
        List<CardData> cardToPlay = hand.Where(card => card.value == hand.Max(c => c.value)).ToList();
        int randomIndex = Random.Range(0, cardToPlay.Count);

        
        CardData selectedCard = cardToPlay[randomIndex];
        hand.Remove(selectedCard);
        return selectedCard;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthIcons[health].color = new Color(1, 1, 1, 0.3f);
        OnTakeDamage?.Invoke();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
