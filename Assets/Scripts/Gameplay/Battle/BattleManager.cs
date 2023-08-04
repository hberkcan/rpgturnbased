using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public enum BattleStateType { Start, Ready, PlayerTurn, EnemyTurn, Won, Lose}
public class BattleManager : MonoBehaviour
{
    private BattleStateType currentState;
    private Dictionary<BattleStateType, BattleState> battleStates;

    [SerializeField] private List<UnitController> alivePlayerUnits = new List<UnitController>();
    public UnitController ClickedUnit { get; private set; }

    [SerializeField] private List<UnitDataSO> enemyUnits;
    [SerializeField] private List<UnitController> aliveEnemyUnits = new List<UnitController>();

    [Range(1, 3)]
    [SerializeField] private int enemyCount;

    [SerializeField] private Vector3[] playerSpawnPositions;
    [SerializeField] private Vector3[] enemySpawnPositions;

    [SerializeField] private BattleHUD battleHUD;
    public BattleHUD BattleHUD => battleHUD;

    public bool IsBattleOn { get; private set; }

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

    private void Start()
    {
        SetupBattle(GameDataManager.GetSelectedUnits());
    }

    private void SetupBattle(List<Unit> selectedUnits)
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            var unit = Instantiate(GameDataManager.GetUnitPrefabByName(selectedUnits[i].Name), playerSpawnPositions[i], Quaternion.identity);
            alivePlayerUnits.Add(unit);
            unit.AssignTargetUnits(aliveEnemyUnits);
            unit.Clicked = UnitClicked;
            unit.UnitDiedEvent += UnitHasDied;
            unit.Init(GameDataManager.GetDataByName(selectedUnits[i].Name));
        }

        for (int i = 0; i < enemyCount; i++)
        {
            var unit = Instantiate(enemyUnits[i].UnitPrefab, enemySpawnPositions[i], Quaternion.identity);
            aliveEnemyUnits.Add(unit);
            unit.AssignTargetUnits(alivePlayerUnits);
            unit.FlipX();
            unit.UnitDiedEvent += UnitHasDied;
            unit.Init(GameDataManager.GetDataByName(enemyUnits[i].BaseData.Name));
        }

        SetState(BattleStateType.Start);
        IsBattleOn = true;
        MessagingSystem.Instance.Dispatch(new BattleStartEvent());
    }

    public void SetState(BattleStateType state) 
    {
        currentState = state;
        StartCoroutine(battleStates[currentState].Execute());
    }

    private void UnitClicked(UnitController clickedUnit)
    {
        if (currentState != BattleStateType.Ready)
            return;

        ClickedUnit = clickedUnit;
        SetState(BattleStateType.PlayerTurn);
    }

    public UnitController GetRandomEnemyUnit()
    {
        var random = UnityEngine.Random.Range(0, enemyCount);
        return aliveEnemyUnits[random];
    }

    void UnitHasDied(UnitController deadUnit)
    {
        RemoveUnitFromAliveUnits(deadUnit);
    }

    void RemoveUnitFromAliveUnits(UnitController unit)
    {
        for (int i = 0; i < alivePlayerUnits.Count; i++)
        {
            if (unit == alivePlayerUnits[i])
            {
                alivePlayerUnits[i].UnitDiedEvent -= UnitHasDied;
                alivePlayerUnits.RemoveAt(i);
                RemoveUnitFromEnemyTeamTargets(unit);
            }
        }

        for (int i = 0; i < aliveEnemyUnits.Count; i++)
        {
            if (unit == aliveEnemyUnits[i])
            {
                aliveEnemyUnits[i].UnitDiedEvent -= UnitHasDied;
                aliveEnemyUnits.RemoveAt(i);
                RemoveUnitFromHeroTeamTargets(unit);
            }
        }

        CheckRemainingTeams();
    }

    void RemoveUnitFromHeroTeamTargets(UnitController unit)
    {
        for (int i = 0; i < alivePlayerUnits.Count; i++)
        {
            alivePlayerUnits[i].RemoveTargetUnit(unit);
        }
    }

    void RemoveUnitFromEnemyTeamTargets(UnitController unit)
    {
        for (int i = 0; i < aliveEnemyUnits.Count; i++)
        {
            aliveEnemyUnits[i].RemoveTargetUnit(unit);
        }
    }

    void CheckRemainingTeams()
    {

        if (alivePlayerUnits.Count == 0)
        {
            SetBattleDefeat();
            IsBattleOn = false;
        }

        if (aliveEnemyUnits.Count == 0)
        {
            SetBattleVictory();
            IsBattleOn = false;
        }
    }

    void SetBattleVictory()
    {
        SetState(BattleStateType.Won);
        foreach (UnitController unit in alivePlayerUnits)
            unit.AddXP();
    }

    void SetBattleDefeat()
    {
        SetState(BattleStateType.Lose);
    }
}
