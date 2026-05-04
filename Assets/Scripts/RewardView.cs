using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    public Image iconImage;
    public Text nameText;
    private RewardData myData;
    private RewardManager manager;

    public void Setup(RewardData data, RewardManager mngr)
    {
        myData = data;
        manager = mngr;
        iconImage.sprite = data.icon;
    }

    public void OnClickSelect()
    {
        manager.SetSelectedReward(myData, gameObject);
        Debug.Log($"Selected reward: {myData.type}");
    }
}
