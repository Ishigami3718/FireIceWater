using UnityEngine;

public enum ElementType
{
    Fire,
    Ice,
    Water,
    Universal
}

[CreateAssetMenu(menuName = "Cards/Card")]
public class CardData: ScriptableObject
{
    public ElementType element;
    public int value;
    public Sprite image;
}
