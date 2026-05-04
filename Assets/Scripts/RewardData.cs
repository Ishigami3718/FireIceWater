using UnityEngine;

public enum RewardType { AddDefaultCard, Heal, EpicCard }

[CreateAssetMenu(menuName = "Rewards/RewardData")]
public class RewardData : ScriptableObject
{
    public Sprite icon;
    public RewardType type;

    public CardData cardToGive;

    public int healAmount;

    private void OnValidate()
    {
        if ((type == RewardType.AddDefaultCard || type == RewardType.EpicCard) && cardToGive != null)
        {
            icon = cardToGive.image;
        }
    }
}
