using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyMessagingSystem;

public class UnitSelectionMenu : Menu<UnitSelectionMenu>
{
    [SerializeField] private UnitDataSO[] unitDatas;
    [SerializeField] private UnitSelect unitUIPrefab;
    [SerializeField] private Transform unitSelectParent;

    private int selectedUnitCount;
    public readonly int MaxCount = 3;

    [SerializeField] private Button startButton;

    protected override void Awake()
    {
        base.Awake();
        startButton.interactable = false;
    }

    private void Start()
    {
        Setup();
    }

    private void OnEnable()
    {
        UnitSelect.OnAnyUnitSelectClicked += UnitSelect_OnAnyUnitSelectClicked;
    }

    private void OnDisable()
    {
        UnitSelect.OnAnyUnitSelectClicked -= UnitSelect_OnAnyUnitSelectClicked;
    }

    private void UnitSelect_OnAnyUnitSelectClicked(UnitSelect unitSelect)
    {
        if (selectedUnitCount == MaxCount && !unitSelect.IsSelected)
            return;

        if (!unitSelect.IsSelected)
        {
            unitSelect.Select();
            MessagingSystem.Instance.Dispatch(new UnitSelectedEvent(unitSelect.Unit));
            selectedUnitCount++;

            if (selectedUnitCount == MaxCount)
                startButton.interactable = true;
        }
        else
        {
            unitSelect.Deselect();
            MessagingSystem.Instance.Dispatch(new UnitDeselectedEvent(unitSelect.Unit));
            selectedUnitCount--;

            startButton.interactable = false;
        }
    }

    private void Setup()
    {
        for (int i = 0; i < unitDatas.Length; i++)
        {
            var unitSelect = Instantiate(unitUIPrefab, unitSelectParent);
            Instantiate(unitDatas[i].UnitIcon, unitSelect.transform);

            var data = GameDataManager.GetUnitDatas()[i];
            unitSelect.Init(data);
            if (GameDataManager.GetSelectedUnits().Contains(data))
            {
                unitSelect.Select();
                selectedUnitCount++;
            }
        }

        if (selectedUnitCount == MaxCount)
            startButton.interactable = true;
    }
}
