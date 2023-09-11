using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;
using Newtonsoft.Json.Linq;

public enum BattleStateType { Start, PlayerTurn, EnemyTurn, Won, Lose}
public class BattleManager : MonoBehaviour, ISaveable
{
    private BattleStateType currentState;
    private Dictionary<BattleStateType, BattleState> battleStates;

    [SerializeField] private List<UnitController> alivePlayerUnits = new List<UnitController>();
    public UnitController ClickedUnit { get; private set; }

    [SerializeField] private List<UnitController> aliveEnemyUnits = new List<UnitController>();
    [SerializeField] private List<UnitData> enemyUnitDatas = new List<UnitData>();
    private List<UnitController> enemyUnitsCreated = new List<UnitController>();
    [Range(1, 3)]
    [SerializeField] private int enemyCount;

    [SerializeField] private Vector3[] playerSpawnPositions;
    [SerializeField] private Vector3[] enemySpawnPositions;

    [SerializeField] private BattleHUD battleHUD;
    public BattleHUD BattleHUD => battleHUD;

    public bool IsBattleOn { get; private set; }
    public bool IsInitialised { get; private set; }

    [SerializeField] private EnemyUnitCreator enemyUnitCreator;
    public bool WaitingForInput { get; set; } = true;
    public bool IsPlayerTurn { get; set; } = true;

    private void Awake()
    {
        battleStates = new Dictionary<BattleStateType, BattleState>(4)
        {
            { BattleStateType.Start, new StartState(this) },
            { BattleStateType.PlayerTurn, new PlayerTurnState(this) },
            { BattleStateType.EnemyTurn, new EnemyTurnState(this) },
            { BattleStateType.Won, new WonState(this) },
            { BattleStateType.Lose, new LoseState(this) }
        };
    }

    private void Start()
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        List<Unit> selectedUnits = GameDataManager.Instance.GetSelectedUnits();

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            var unit = selectedUnits[i];
            var unitController = unit.CreateUnitObject(playerSpawnPositions[i], Quaternion.identity);

            if (!unit.IsAlive)
            {
                continue;
            }

            alivePlayerUnits.Add(unitController);
            unitController.AssignTargetUnits(aliveEnemyUnits);
            unitController.Clicked = UnitClicked;
            unitController.UnitDiedEvent += UnitHasDied;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Unit unit;

            if (!IsInitialised)
            {
                unit = enemyUnitCreator.GetEnemyUnit();
            }
            else
            {
                unit = enemyUnitCreator.GetEnemyUnit(enemyUnitDatas[i]);
            }


            var unitController = unit.CreateUnitObject(enemySpawnPositions[i], Quaternion.identity);

            if (!unit.IsAlive)
            {
                enemyUnitsCreated.Add(unitController);
                continue;
            }

            aliveEnemyUnits.Add(unitController);
            enemyUnitsCreated.Add(unitController);
            unitController.AssignTargetUnits(alivePlayerUnits);
            unitController.FlipX();
            unitController.UnitDiedEvent += UnitHasDied;
        }

        StartBattle();
    }

    private void StartBattle()
    {
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
        if (currentState != BattleStateType.PlayerTurn || !WaitingForInput)
            return;

        WaitingForInput = false;
        ClickedUnit = clickedUnit;
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
        MessagingSystem.Instance.Dispatch(new BattleEndEvent(alivePlayerUnits));
        MessagingSystem.Instance.Dispatch(new BattleWonEvent());
    }

    void SetBattleDefeat()
    {
        SetState(BattleStateType.Lose);
        MessagingSystem.Instance.Dispatch(new BattleEndEvent(alivePlayerUnits));
        MessagingSystem.Instance.Dispatch(new BattleLostEvent());
    }

    public JToken CaptureState()
    {
        var battleData = new BattleData();
        battleData.IsPlayerTurn = IsPlayerTurn;

        for(int i = 0; i < enemyUnitsCreated.Count; i++)
        {
            enemyUnitDatas.Add(enemyUnitsCreated[i].GetData());
        }

        battleData.EnemyUnits = enemyUnitDatas;
        return JToken.FromObject(battleData);
    }

    public void RestoreFromState(JToken state)
    {
        if (IsInitialised)
            return;

        IsInitialised = true;

        var battleData = state.ToObject<BattleData>();

        IsPlayerTurn = battleData.IsPlayerTurn;

        enemyUnitDatas = battleData.EnemyUnits;
    }
}
