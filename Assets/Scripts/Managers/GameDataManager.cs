using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;
using System.Linq;

[RequireComponent(typeof(SavingWrapper))]
public class GameDataManager : MonoBehaviour, ISaveable, IMessagingSubscriber<UnitSelectedEvent>, IMessagingSubscriber<UnitDeselectedEvent>, IMessagingSubscriber<BattleStartEvent>, IMessagingSubscriber<BattleEndEvent>
{
    public static GameDataManager Instance;
    [SerializeField] private UnitBaseData[] unitDataSOs;
    private Unit[] units;
    private Dictionary<string, Unit> unitsDictionary;
    private List<string> selectedUnitsNames;
    private List<Unit> selectedUnits;
    public bool IsBattleActive { get; private set; }
    public bool IsInitialised { get; private set; }
    private int battleCount;
    private const int stepForProgress = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        units = new Unit[unitDataSOs.Length];
        unitsDictionary = new Dictionary<string, Unit>(unitDataSOs.Length);
        selectedUnitsNames = new List<string>();
        selectedUnits = new List<Unit>();

        Init();
    }

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Subscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleStartEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleEndEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleStartEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleEndEvent>(this);
    }

    private void Init()
    {
        for (int i = 0; i < unitDataSOs.Length; i++)
        {
            UnitData baseData = unitDataSOs[i].Data;
            UnitData data = new UnitData(baseData.Name, baseData.UnitType, baseData.MaxHealth, baseData.MaxHealth, baseData.AttackPower, baseData.Experience, baseData.IsUnlocked);
            Unit unit = new Unit(data);
            units[i] = unit;
        }

        for (int i = 0; i < unitDataSOs.Length; i++)
        {
            unitsDictionary.Add(units[i].Name, units[i]);
        }
    }

    public JToken CaptureState()
    {
        List<UnitData> datas = new List<UnitData>();

        for(int i = 0; i < units.Length; i++)
        {
            datas.Add(units[i].GetData());
        }

        var gameData = new GameData
        {
            UnitDatas = datas.ToArray(),
            SelectedUnits = selectedUnitsNames,
            IsBattleActive = IsBattleActive,
            BattleCount = battleCount
        };

        return JToken.FromObject(gameData);
    }

    public void RestoreFromState(JToken state)
    {
        if (IsInitialised)
            return;

        IsInitialised = true;

        var gameData = state.ToObject<GameData>();

        for (int i = 0; i < gameData.UnitDatas.Length; i++)
        {
            UnitData data = gameData.UnitDatas[i];
            units[i].SetData(new UnitData(data.Name, data.UnitType, data.MaxHealth, data.CurrentHealth, data.AttackPower, data.Experience, data.IsUnlocked));
        }

        selectedUnitsNames = gameData.SelectedUnits;

        foreach (string name in selectedUnitsNames)
            selectedUnits.Add(GetUnitByName(name));

        IsBattleActive = gameData.IsBattleActive;
        battleCount = gameData.BattleCount;
    }

    public Unit GetUnitByName(string name)
    {
        return unitsDictionary[name];
    }

    public Unit[] GetUnitDatas()
    {
        return units;
    }

    public List<Unit> GetSelectedUnits()
    {
        return selectedUnits;
    }

    public void OnReceiveMessage(UnitSelectedEvent message)
    {
        selectedUnitsNames.Add(message.Unit.Name);
        selectedUnits.Add(message.Unit);
    }

    public void OnReceiveMessage(UnitDeselectedEvent message)
    {
        selectedUnitsNames.Remove(message.Unit.Name);
        selectedUnits.Remove(message.Unit);
    }

    public void OnReceiveMessage(BattleStartEvent message)
    {
        IsBattleActive = true;
    }

    public void OnReceiveMessage(BattleEndEvent message)
    {
        IsBattleActive = false;
        battleCount++;

        foreach (UnitController unit in message.AlivePlayerUnits)
            unit.AddXP();

        StartCoroutine(ResetUnitsOnBattleground());

        if (battleCount % stepForProgress != 0)
            return;

        int unlockedCount = units.Count(entry => entry.IsUnlocked);

        if (unlockedCount > units.Length - 1)
            return;

        units[unlockedCount].IsUnlocked = true;
    }

    IEnumerator ResetUnitsOnBattleground()
    {
        yield return null; // For race conditions like update and animation events cycles

        for(int i = 0; i < selectedUnitsNames.Count; i++)
        {
            GetUnitByName(selectedUnitsNames[i]).ResetHealth();
        }
    }
}
