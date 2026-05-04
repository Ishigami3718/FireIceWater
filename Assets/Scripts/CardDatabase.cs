using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> allCards;
    public List<CardData> bot1Cards;
    public List<CardData> bot2Cards;
    public List<CardData> bot3Cards;
    public List<CardData> bot4Cards;
    public List<CardData> bot5Cards;
    public List<CardData> bot6Cards;
}
