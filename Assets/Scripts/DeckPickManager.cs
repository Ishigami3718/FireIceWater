using System.Collections.Generic;
using UnityEngine;

public class DeckPickManager : MonoBehaviour
{
    public CardDatabase cardDatabase;
    public Transform grid;
    public GameObject cardPrefab;
    public List<CardData> deckHolder;
    private List<CardData> cards;
    private List<CardData> cardsToPlay = new List<CardData>(9);

    private void Awake()
    {
        cards = new List<CardData>(cardDatabase.allCards);
    }

    private void Start()
    {
        SpawnCards();
    }

    void SpawnCards()
    {
        foreach (CardData card in cards)
        {
            GameObject obj = Instantiate(cardPrefab, grid);
            obj.GetComponent<MenuCardView>().Setup(card, this);
        }
    }

    public void ToggleCardSelection(MenuCardView cardItem)
    {
        if (cardItem.isSelected)
        {
            cardItem.isSelected = false;
            cardItem.selectionFrame.SetActive(false);
            cardsToPlay.Remove(cardItem.cardData);
        }
        else
        {
            if (cardsToPlay.Count < 9)
            {
                cardItem.isSelected = true;
                cardItem.selectionFrame.SetActive(true);
                Debug.Log("Clicked");
                cardsToPlay.Add(cardItem.cardData);
            }
        }
    }

    public void Save()
    {
        if (cardsToPlay.Count == 9)
        {
            PlayerSettings.deck = new List<CardData>(cardsToPlay);
        }
        else
        {
            PlayerSettings.deck = new List<CardData>(deckHolder);
        }
    }
}
