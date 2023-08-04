using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class GameDataManager : MonoBehaviour, ISaveable, IMessagingSubscriber<UnitSelectedEvent>, IMessagingSubscriber<UnitDeselectedEvent>, IMessagingSubscriber<BattleWonEvent>, IMessagingSubscriber<BattleLostEvent>
{
    [SerializeField] private UnitDataSO[] unitDataSOs;
    private static Unit[] unitDatas;
    private static Dictionary<string, UnitDataSO> unitBaseDictionary;
    private static Dictionary<string, Unit> unitDataDictionary;
    private static List<Unit> selectedUnits;
    private bool isLoaded;

    private void Awake()
    {
        unitDatas = new Unit[unitDataSOs.Length];
        unitBaseDictionary = new Dictionary<string, UnitDataSO>(unitDataSOs.Length);
        unitDataDictionary = new Dictionary<string, Unit>(unitDataSOs.Length);
        selectedUnits = new List<Unit>();
    }

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Subscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleLostEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<UnitSelectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<UnitDeselectedEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleLostEvent>(this);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (!isLoaded)
        {
            for (int i = 0; i < unitDataSOs.Length; i++)
            {
                Unit character = new(unitDataSOs[i].BaseData.Name, unitDataSOs[i].BaseData.MaxHealth, unitDataSOs[i].BaseData.MaxHealth.GetValue(), unitDataSOs[i].BaseData.AttackPower, unitDataSOs[i].BaseData.Experience, unitDataSOs[i].BaseData.Level);
                unitDatas[i] = character;
            }
        }

        for (int i = 0; i < unitDataSOs.Length; i++)
        {
            unitBaseDictionary.Add(unitDataSOs[i].BaseData.Name, unitDataSOs[i]);
            unitDataDictionary.Add(unitDataSOs[i].BaseData.Name, unitDatas[i]);
        }
    }

    public JToken CaptureState()
    {
        var gameData = new GameData
        {
            UnitDatas = unitDatas
        };

        return JToken.FromObject(gameData);
    }

    public void RestoreFromState(JToken state)
    {
        isLoaded = true;
        var gameData = state.ToObject<GameData>();
        unitDatas = gameData.UnitDatas;
    }

    public static UnitController GetUnitPrefabByName(string name)
    {
        return unitBaseDictionary[name].UnitPrefab;
    }

    public static Unit GetDataByName(string name)
    {
        return unitDataDictionary[name];
    }

    public static Unit[] GetUnitDatas()
    {
        return unitDatas;
    }

    public static List<Unit> GetSelectedUnits()
    {
        return selectedUnits;
    }

    public void OnReceiveMessage(UnitSelectedEvent message)
    {
        selectedUnits.Add(message.unit);
    }

    public void OnReceiveMessage(UnitDeselectedEvent message)
    {
        selectedUnits.Remove(message.unit);
    }

    public void OnReceiveMessage(BattleWonEvent message)
    {
        //selectedUnits.Clear();
    }

    public void OnReceiveMessage(BattleLostEvent message)
    {
        //selectedUnits.Clear();
    }
}
