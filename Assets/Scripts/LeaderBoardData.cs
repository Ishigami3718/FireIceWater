using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class LeaderBoardData : IComparable
{
    public int score;
    public int cardUsedCount;
    public int healthLost;
    public string dateString;
    DateTime date;

    public LeaderBoardData(int s, int c, int h)
    {
        score = s;
        cardUsedCount = c;
        healthLost = h;
    }

    public void SetDate()
    {
        date = DateTime.Now;
        dateString = date.ToString("dd.MM.yyyy HH:mm:ss");
    }
    public int CompareTo(object obj)
    {
        LeaderBoardData other = obj as LeaderBoardData;
        if (this.score.CompareTo(other.score) == 0)
        {
            if (this.cardUsedCount == other.cardUsedCount)
            {
                if (this.healthLost == other.healthLost) return DateTime.Parse(this.dateString)
                        .CompareTo(DateTime.Parse(other.dateString));
                else if (this.healthLost < other.healthLost) return 1;
                else return -1;
            }
            else if (this.cardUsedCount < other.cardUsedCount) return 1;
            else return -1;
        }
        else return this.score.CompareTo(other.score);
    }
}

[Serializable]
public class LeaderBoard
{
    public List<LeaderBoardData> leaders = new List<LeaderBoardData>(5);

    public void AddLeader(LeaderBoardData data)
    {
        leaders.Add(data);
        leaders = leaders.OrderByDescending(s => s).ToList();
        if (leaders.Count > 5) leaders.RemoveAt(leaders.Count - 1);
    }
}
