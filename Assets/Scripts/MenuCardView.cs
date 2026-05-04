using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuCardView : MonoBehaviour
{
    public CardData cardData;
    public GameObject selectionFrame;
    public bool isSelected = false;

    private DeckPickManager manager;

    public void OnClick()
    {
        manager.ToggleCardSelection(this);
    }


    public void Setup(CardData cardData, DeckPickManager deckPickManager)
    {
        this.cardData = cardData;
        manager = deckPickManager;
        GetComponent<Image>().sprite = this.cardData.image;
        selectionFrame.SetActive(false);

    }

}
