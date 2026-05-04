using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{

    public GameObject cardPrefab; 
    public Transform handTransform;
    List<CardData> hand = new List<CardData>();

    public void AddCard(CardData data)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform);
        CardView view = newCard.GetComponent<CardView>();
        view.Init(data);
        hand.Add(data);
    }

    public void MakeTurn(CardData data)
    {
        hand.Remove(data);
        if (hand.Count == 0) 
        {
            StartCoroutine(WaitAndDrawRoutine());
        }
    }

    private IEnumerator WaitAndDrawRoutine()
    {
        yield return new WaitForSeconds(4.0f);

        GameManager.Instance.DrawCards(6);
    }
}
