using UnityEngine;

public enum Condition
{
    Win,
    Lose,
    Draw
}
public static class CardLogic
{
    public static Condition Beats(CardData a, CardData b)
    {
        if(a.element == b.element)
        {
            if(a.value > b.value) return Condition.Win;
            if(a.value < b.value) return Condition.Lose;
            return Condition.Draw;
        }
        if ((a.element == ElementType.Fire && b.element == ElementType.Ice) ||
               (a.element == ElementType.Ice && b.element == ElementType.Water) ||
               (a.element == ElementType.Water && b.element == ElementType.Fire) ||
               (a.element == ElementType.Universal && b.element == ElementType.Fire) ||
               (a.element == ElementType.Universal && b.element == ElementType.Water) ||
               (a.element == ElementType.Universal && b.element == ElementType.Ice)) 
               return Condition.Win;
        return Condition.Lose;
    }
}
