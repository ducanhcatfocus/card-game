using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField] private List<Transform> enemyPosList;
    [SerializeField] PlayerBase player;
    bool isLose = false;
    public EnemyEncounter CurrentEncounter { get; private set; }
    EnemyDataBase enemyData;
    EnemyBase enemy;


    public PlayerBase Player => player;

    public EnemyDataBase EnemyData=> enemyData;
    public EnemyBase Enemy => enemy;
    public List<Transform> EnemyPosList => enemyPosList;

    protected DeckManager DeckManager => DeckManager.Ins;
    protected GameManager GameManager => GameManager.Ins;

    protected UIManager UIManager => UIManager.Ins;

    protected AudioManager AudioManager => AudioManager.Ins;




    private void Start()
    {
        UIManager.Ins.OpenUI<GamePlay>();

        StartCombat();
        GameManager.AlowClick(true);
    }
    public void StartCombat()
    {
        BuildPlayer();
        BuildEnemies();
      
        DeckManager.SetGameDeck();

        DeckManager.DrawCards(4);
        DeckManager.SetEnemyDeck(enemyData);
        DeckManager.EnemyDrawCards(4);
    }

    private void BuildEnemies()
    {
        CurrentEncounter = GameManager.EncounterData.GetEnemyEncounter(
            GameManager.InitGameplayData.CurrentStageId,
            GameManager.InitGameplayData.CurrentEncounterId,
            GameManager.InitGameplayData.IsFinalEncounter);

        var enemyList = CurrentEncounter.EnemyList;
        enemyData = enemyList[0];
        for (var i = 0; i < enemyList.Count; i++)
        {
            var enemy = Instantiate(enemyList[i].EnemyPrefab, EnemyPosList.Count >= i ? EnemyPosList[i] : EnemyPosList[0]);
            enemy.BuildCharacter();
            this.enemy = enemy;
        }
    }

    private void BuildPlayer()
    {
      
            player.BuildCharacter();
         
        
    }

    public void DoEffectCardEachWhenCharacterDrawACardIfHave()
    {
        Player.CharacterStats.PoisonDamage(Player.transform);
        Enemy.CharacterStats.PoisonDamage(Enemy.transform);
    }

    public void DoEffectCardEachTurn()
    {
        Player.CharacterStats.BleedDamage(Player.transform);
        Enemy.CharacterStats.BleedDamage(Enemy.transform);
    }

    public void OnEnemyDeath()
    {

        DeckManager.HandController.RemoveCardFromEnemyHand();
        if (isLose) return;
        GameManager.InitGameplayData.CurrentEncounterId++;
         GameManager.InitGameplayData.CurrentHP = player.CharacterStats.CurrentHealth;
         Invoke(nameof(DisplayWinPanel), 1);

    }

    public void OnPlayerDeath()
    {
        isLose = true;

        LoseCombat();
    }

    private void DisplayWinPanel()
    {
        AudioManager.PlayWinSound();
        UIManager.DisplayWinPanel();
    }

    private void LoseCombat()
    {
        AudioManager.PlayLoseSound();

        UIManager.DisplayLosePanel();
    }
}
