using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyMessagingSystem;

public class StatDisplayManager : MonoBehaviour, IMessagingSubscriber<StatDisplayEvent>, IMessagingSubscriber<HideStatDisplayEvent>
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform panelRectTr;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackPowerText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<StatDisplayEvent>(this);
        MessagingSystem.Instance.Subscribe<HideStatDisplayEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<StatDisplayEvent>(this);
        MessagingSystem.Instance.Unsubscribe<HideStatDisplayEvent>(this);
    }

    public void OnReceiveMessage(StatDisplayEvent message)
    {
        nameText.text = $"Name: {message.UnitData.Name}";
        healthText.text = $"HP: {message.UnitData.MaxHealth.Value}";
        attackPowerText.text = $"AP: {message.UnitData.AttackPower.Value}";
        levelText.text = $"Lvl: {message.UnitData.CurrentLevel}";
        experienceText.text = $"Exp: %{message.UnitData.XpPercentage}";

        SetPositionWithOffset(message.ScreenPos);
        canvas.SetActive(true);
    }

    public void OnReceiveMessage(HideStatDisplayEvent message)
    {
        canvas.SetActive(false);
    }

    private void SetPositionWithOffset(Vector2 screenPos)
    {
        Vector2 offset = new Vector2(Screen.width * 0.05f, Screen.height * 0.05f);
        panelRectTr.pivot = Vector2.zero;
        panelRectTr.anchoredPosition = screenPos + offset;
    }
}
