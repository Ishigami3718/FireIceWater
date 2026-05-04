using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void LoadLeaders()
    {
        LeaderBoard leaders;
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            leaders = JsonUtility.FromJson<LeaderBoard>(json);
        }
        else leaders = new LeaderBoard();
        if (leaders == null) Debug.Log("null");
        StringBuilder sb = new StringBuilder();
        foreach (LeaderBoardData le in leaders.leaders.OrderByDescending(s => s))
        {
            sb.Append($"{le.score}\t{le.cardUsedCount}\t{le.healthLost}\t{le.dateString}\n");
        }
        text.text = sb.ToString();
    }
}
