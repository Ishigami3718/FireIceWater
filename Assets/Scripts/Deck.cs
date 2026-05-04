using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public event Action OnDeckEmpty;

    public List<CardData> cards = new List<CardData>();
    private List<CardData> temp = new List<CardData>();

    public Deck(List<CardData> cardList)
    {
        cards = new List<CardData>(cardList);
        temp = new List<CardData>(cardList);
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, cards.Count);
            CardData temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public CardData Draw()
    {
        if (cards.Count == 0) ResetDeck();
        CardData topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    public void ResetDeck()
    {
        cards = new List<CardData>(temp);
        Shuffle();
        OnDeckEmpty?.Invoke();
    }

    public void AddCard(CardData card)
    {
        cards.Add(card);
        temp.Add(card);
    }
}
