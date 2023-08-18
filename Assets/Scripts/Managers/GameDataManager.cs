using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

[RequireComponent(typeof(SavingWrapper))]
public class GameDataManager : MonoBehaviour, ISaveable, IMessagingSubscriber<UnitSelectedEvent>, IMessagingSubscriber<UnitDeselectedEvent>, IMessagingSubscriber<BattleStartEvent>, IMessagingSubscriber<BattleWonEvent>, IMessagingSubscriber<BattleLostEvent>
{
    public static GameDataManager Instance;
    [SerializeField] private UnitBaseData[] unitDataSOs;
    [SerializeField] private Unit[] units;
    private Dictionary<string, Unit> unitsDictionary;
    [SerializeField]private List<string> selectedUnits;
    public static bool isBattleActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        units = new Unit[unitDataSOs.Length];
        unitsDictionary = new Dictionary<string, Unit>(unitDataSOs.Length);
        selectedUnits = new List<string>();

        Init();
    }

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Subscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleStartEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleLostEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleStartEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleLostEvent>(this);
    }

    private void Init()
    {
        for (int i = 0; i < unitDataSOs.Length; i++)
        {
            Unit unit = new Unit(unitDataSOs[i]);
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
            SelectedUnits = selectedUnits,
            IsBattleActive = isBattleActive
        };

        return JToken.FromObject(gameData);
    }

    public void RestoreFromState(JToken state)
    {
        var gameData = state.ToObject<GameData>();

        for (int i = 0; i < gameData.UnitDatas.Length; i++)
        {
            UnitData data = gameData.UnitDatas[i];
            units[i].SetData(new UnitData(data.Name, data.MaxHealth, data.CurrentHealth, data.AttackPower, data.Experience));
        }

        selectedUnits = gameData.SelectedUnits;
        isBattleActive = gameData.IsBattleActive;
    }

    public Unit GetUnitByName(string name)
    {
        return unitsDictionary[name];
    }

    public Unit[] GetUnitDatas()
    {
        return units;
    }

    public List<string> GetSelectedUnits()
    {
        return selectedUnits;
    }

    public void OnReceiveMessage(UnitSelectedEvent message)
    {
        selectedUnits.Add(message.unit.Name);
    }

    public void OnReceiveMessage(UnitDeselectedEvent message)
    {
        selectedUnits.Remove(message.unit.Name);
    }

    public void OnReceiveMessage(BattleWonEvent message)
    {
        isBattleActive = false;
    }

    public void OnReceiveMessage(BattleLostEvent message)
    {
        isBattleActive = false;
    }

    public void OnReceiveMessage(BattleStartEvent message)
    {
        isBattleActive = true;
    }
}
