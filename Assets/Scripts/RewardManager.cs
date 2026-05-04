using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public const float commonRewardChance = 0.5f;
    public const float rareRewardChance = 0.3f;
    public const float epicRewardChance = 0.2f;
    public CardDatabase cards;
    public List<RewardData> allPossibleRewards = new List<RewardData>();
    public Button confirmButton; 
    public Transform rewardPanel;
    public Transform rewardContainer;
    public GameObject rewardPrefab;
    private RewardData selectedReward;
    bool isRewardSelected = false;
    List<CardData> cardPool;

    private void Awake()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
        cardPool = new List<CardData>(cards.allCards);
    }

    public void ShowRewards()
    {
        GameManager.Instance.canPlay = false;
        GameManager.Instance.isClicable = false;
        rewardPanel.gameObject.SetActive(true);
        isRewardSelected = false;

        foreach (Transform child in rewardContainer)  Destroy(child.gameObject);

        for (int i = 0; i < 3; i++)
        {
            RewardData randomReward = GetRandomReward();

            GameObject go = Instantiate(rewardPrefab, rewardContainer);
            go.GetComponent<RewardView>().Setup(randomReward, this);
        }
    }

    private RewardData GetRandomReward() {         
        float roll = Random.value;
        RewardType type;
        if (roll > epicRewardChance && roll <= rareRewardChance) type = RewardType.Heal;
        else if (roll > rareRewardChance) type = RewardType.AddDefaultCard;
        else type = RewardType.EpicCard;
        List<RewardData> filteredRewards = allPossibleRewards.FindAll(r => r.type == type);
        return filteredRewards[Random.Range(0, filteredRewards.Count)];
    }

    public void SetSelectedReward(RewardData data, GameObject visualObject)
    {
        selectedReward = data;
        isRewardSelected = true;

        ResetAllOptionsVisuals();
        visualObject.transform.localScale = Vector3.one * 1.2f;
    }

    private void ResetAllOptionsVisuals()
    {
        foreach (Transform child in rewardContainer)
        {
            child.localScale = Vector3.one;
        }
    }

    public void OnConfirmButtonClick()
    {

        if (selectedReward == null && !isRewardSelected) return;

        ApplyReward(selectedReward);

        rewardPanel.gameObject.SetActive(false);
        selectedReward = null;
        GameManager.Instance.StartMovingToNextEnemy();
    }

    private void ApplyReward(RewardData data)
    {
        switch (data.type)
        {
            case RewardType.Heal:
                GameManager.Instance.HealPlayer(data.healAmount);
                break;
            case RewardType.AddDefaultCard:
                GameManager.Instance.AddCardReward(data.cardToGive);
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
