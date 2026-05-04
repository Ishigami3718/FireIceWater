using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CardDatabase database;    
    public PlayerHand playerHand;
    public List<CardData> playerResetCards = new List<CardData>();
    public GameObject battleField;
    public List<Enemy> enemies;
    public GameObject health;
    public RewardManager rewardManager;
    public float speed = 1.5f;
    public GameObject player;
    public bool isClicable = true;
    public GameObject winLosePanel;
    public TextMeshProUGUI winLose;
    public TextMeshProUGUI resultScores;
    public AudioSource music;
    


    public static GameManager Instance { get; private set; }

    private Deck deck;
    private bool isMoving = false;
    public bool canPlay = true;
    private int currentEnemyIndex = 0;
    private int playerHealth = 3;
    private List<SpriteRenderer> healthIcons;
    private bool isPlayerTurn = true;
    private CardData playerCard;
    private CardData enemyCard;
    private bool isDefeated = false;
    private bool isGameOver = false;
    private LeaderBoardData dataToSave;
    [SerializeField]
    private LeaderBoard leaderBoard;
    private int cardUsedCount = 0;
    private int score = 0;
    private int healthLost = 0;
    public Animator anim;

    void Awake()
    {
        Instance = this;
        music.volume = PlayerSettings.volume;
        if(PlayerSettings.isPlayable) music.Play();
        else music.Stop();
        healthIcons = new List<SpriteRenderer>(health.GetComponentsInChildren<SpriteRenderer>());
        if(PlayerSettings.deck.Count==0 || PlayerSettings.deck == null) deck = new Deck(database.allCards);
        else deck = new Deck(PlayerSettings.deck);
        deck.Shuffle();
    }

    void Start()
    {
        battleField = GameObject.Find("BattleField");
        player = GameObject.Find("Player");
        foreach (Enemy enemy in enemies) enemy.OnTakeDamage += ChekEnemyHealth;
        DrawCards(6);
    }

    private void Update()
    {
        if (isMoving)
        {
            player.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    public void OnCardSelected(CardView card)
    {
        if (!canPlay) return;
        canPlay = false;
        AddCardInResetDeck(card);
        playerHand.MakeTurn(card.data);
        playerCard = card.data;
        SetPlayerCardOnField(card);
        isClicable = false;
        // до ужаса костильна реалізація зміни черговості ходу з корутинами
        if (isPlayerTurn)
        {
            StartCoroutine(BattleSequenceOnPlayerTurn());
            Destroy(card.gameObject);
        }
        else if(!isDefeated)
        {
            StartCoroutine(WaitCompareTime());
            Destroy(card.gameObject);

        }

    }

    private IEnumerator BattleSequenceOnPlayerTurn()
    {
        Debug.Log("Бот думає...");
        yield return new WaitForSeconds(1.5f);

        Enemy activeEnemy = enemies[currentEnemyIndex];
        enemyCard = activeEnemy.MakeTurn();

        SetEnemyCardOnField(enemyCard);

        yield return new WaitForSeconds(1.0f); 

        CompareCards();

        yield return new WaitForSeconds(1.5f);

        if (isDefeated)
        {
            ResetField();
            yield break;
        }

        ResetField();
        isPlayerTurn = false;
        StartCoroutine(BattleSequenceOnEnemyTurn());
        //isClicable = true;
        //canPlay = true;
    }

    private IEnumerator BattleSequenceOnEnemyTurn()
    {
        Debug.Log("Бот думає...");
        yield return new WaitForSeconds(1.5f);

        Enemy activeEnemy = enemies[currentEnemyIndex];
        enemyCard = activeEnemy.MakeTurn();

        SetEnemyCardOnField(enemyCard);

        yield return new WaitForSeconds(1.0f);

        isClicable = true;
        canPlay = true;
    }

    private IEnumerator WaitCompareTime()
    {
        yield return new WaitForSeconds(1.0f);
        Image enemyCardOnField = battleField.GetComponentsInChildren<Image>()[1];
        enemyCardOnField.sprite = enemyCard.image;
        CompareCards();
        yield return new WaitForSeconds(1.5f);

        ResetField();
        isPlayerTurn = true;
        isClicable = true;
        canPlay = true;
    }

    private void CompareCards()
    {
        Condition playerWins = CardLogic.Beats(playerCard, enemyCard);

        if (playerWins == Condition.Win)
        {
            enemies[currentEnemyIndex].TakeDamage(1);
        }
        else if (playerWins == Condition.Lose)
        {
            GetDamage();
        }
        cardUsedCount++;
    }


    private void SetPlayerCardOnField(CardView card)
    {
        Image playerCardOnField = battleField.GetComponentsInChildren<Image>()[0];
        playerCardOnField.sprite = card.data.image;
        playerCardOnField.color = Color.white;
    }
    private void SetEnemyCardOnField(CardData data)
    {
        Image enemyCardOnField = battleField.GetComponentsInChildren<Image>()[1];
        enemyCardOnField.sprite = isPlayerTurn ? data.image : enemies[currentEnemyIndex].cardBack;
        enemyCardOnField.color = Color.white;
    }

    private void ResetField()
    {
        foreach (var img in battleField.GetComponentsInChildren<Image>())
        {
            img.color = new Color(1, 1, 1, 0);
        }
    }
    public void AddCardInResetDeck(CardView card)
    {
        playerResetCards.Add(card.data);
    }
    public void DrawCards(int count)
    {
        if (isGameOver) return;
        for (int i = 0; i < count; i++)
        {
            CardData nextCard = deck.Draw();

            playerHand.AddCard(nextCard);
        }
    }

    public void DrawCardsEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy activeEnemy = enemies[currentEnemyIndex];
            if (activeEnemy == null) return;
            CardData nextCard = activeEnemy.deck.Draw();

            activeEnemy.hand.Add(nextCard);
        }
    }
    private void ChekEnemyHealth()
    {
        if (enemies[currentEnemyIndex].health <= 0)
        {
            isClicable = false;
            canPlay = false;
            isPlayerTurn = true;
            isDefeated = true;
            anim.SetTrigger("victory");
            enemies[currentEnemyIndex].gameObject.SetActive(false);
            StartCoroutine(ShowRewards());
            currentEnemyIndex++;
            score++;

                if (currentEnemyIndex >= enemies.Count)
                {
                    WinLoseGame("You Win");
            }
        }
    }

    private IEnumerator ShowRewards()
    {

        yield return new WaitForSeconds(1.5f);
        rewardManager.ShowRewards();
    }

    private void GetDamage()
    {
        playerHealth--;
        healthLost++;
        healthIcons[playerHealth].color = new Color(1, 1, 1, 0.3f);
        if (playerHealth <= 0)
        {
            WinLoseGame("You Lose");
        }
    }

    public void HealPlayer(int amount)
    {
        if (playerHealth >= healthIcons.Count) return;
        playerHealth = Mathf.Min(playerHealth + amount, healthIcons.Count);
        for (int i = 0; i < playerHealth; i++)
        {
            healthIcons[i].color = Color.white;
        }
    }

    public void AddCardReward(CardData card)
    {
        deck.AddCard(card);
    }

    public void StartMoving()
    {
        anim.ResetTrigger("victory");
        anim.SetBool("isWalking", true);
        isMoving = true;
    }

    public void StopMoving()
    {
        anim.SetBool("isWalking", false);
        isMoving = false;
        isDefeated = false;
    }

    private void WinLoseGame(string finalText)
    {
        isGameOver = true;
        winLosePanel.SetActive(true);
        StringBuilder sb = new StringBuilder();
        winLose.text = finalText;
        
        sb.Append($"Score: {score}\n");
        sb.Append($"Card used: {cardUsedCount}\n");
        sb.Append($"Health lost: {healthLost}\n");
        Debug.Log(sb.ToString());
        resultScores.text = sb.ToString();
        SaveScore();
        isClicable = false;
        isMoving = false;
        canPlay = false;
    }


    private void NextEnemy()
    {
        StartMoving();
        isDefeated = false;
    }

    public void StartMovingToNextEnemy()
    {
        NextEnemy();
        //enemies[currentEnemyIndex].gameObject.SetActive(true);
    }

    private void SaveScore()
    {
        leaderBoard = LoadLeaderboard();
        dataToSave = new LeaderBoardData(score,cardUsedCount,healthLost);
        dataToSave.SetDate();
        leaderBoard.AddLeader(dataToSave);

        string json = JsonUtility.ToJson(leaderBoard);

        PlayerPrefs.SetString("Leaderboard", json);

        PlayerPrefs.Save();
    }

    private LeaderBoard LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            return JsonUtility.FromJson<LeaderBoard>(json);
        }

        return new LeaderBoard();
    }
}