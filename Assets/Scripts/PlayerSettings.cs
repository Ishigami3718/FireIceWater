using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlayerSettings
{
    public static List<CardData> deck = new List<CardData>(9);
    public static float volume = 0.2f;
    public static bool isPlayable = true;

}
