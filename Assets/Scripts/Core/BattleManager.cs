using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public enum BattleStateType { Start, Ready, PlayerTurn, EnemyTurn, Won, Lose}
public class BattleManager : MonoBehaviour, IMessagingSubscriber<StartBattleEvent>
{
    private BattleStateType currentState;
    private Dictionary<BattleStateType, BattleState> battleStates;

    private List<CharacterBrain> playerCharacters = new List<CharacterBrain>(3);
    public CharacterBrain ClickedCharacter { get; private set; }

    [SerializeField] private CharacterBrain enemyPrefab;
    public CharacterBrain EnemyCharacter { get; private set; }

    [SerializeField] private Vector3[] spawnPositions;

    [SerializeField] private BattleHUD battleHUD;
    public BattleHUD BattleHUD => battleHUD;

    private void Awake()
    {
        battleStates = new Dictionary<BattleStateType, BattleState>(4)
        {
            { BattleStateType.Start, new StartState(this) },
            { BattleStateType.Ready, new ReadyState(this) },
            { BattleStateType.PlayerTurn, new PlayerTurnState(this) },
            { BattleStateType.EnemyTurn, new EnemyTurnState(this) },
            { BattleStateType.Won, new WonState(this) },
            { BattleStateType.Lose, new LoseState(this) }
        };
    }

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<StartBattleEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<StartBattleEvent>(this);
    }

    public void OnReceiveMessage(StartBattleEvent message)
    {
        SetupBattle(message.selectedCharacters);
    }

    private void SetupBattle(List<CharacterDataSO> selectedCharacters)
    {
        for(int i = 0; i < selectedCharacters.Count; i++)
        {
            var character = Instantiate(selectedCharacters[i].CharacterPrefab, spawnPositions[i], Quaternion.identity);
            character.Clicked = HeroClicked;
            playerCharacters.Add(character);
        }

        EnemyCharacter = Instantiate(enemyPrefab, Vector2.right * 20f + Vector2.down * 2.5f, Quaternion.identity);
        EnemyCharacter.transform.Rotate(0f, 180f, 0f);

        SetState(BattleStateType.Start);
    }

    public void SetState(BattleStateType state) 
    {
        currentState = state;
        StartCoroutine(battleStates[currentState].Execute());
    }

    private void HeroClicked(CharacterBrain clickedCharacter)
    {
        if (currentState != BattleStateType.Ready)
            return;

        ClickedCharacter = clickedCharacter;
        SetState(BattleStateType.PlayerTurn);
    }

    public CharacterBrain GetRandomPlayerCharacter()
    {
        var random = UnityEngine.Random.Range(0, 3);
        return playerCharacters[random];
    }
}
