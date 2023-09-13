using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyMessagingSystem;

public class StatDisplayManager : MonoBehaviour, IMessagingSubscriber<StatDisplayEvent>, IMessagingSubscriber<HideStatDisplayEvent>
{
    [SerializeField] private Canvas canvas;
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

        canvas.gameObject.SetActive(true);
        SetPositionWithOffset(message.ScreenPos);
    }

    public void OnReceiveMessage(HideStatDisplayEvent message)
    {
        canvas.gameObject.SetActive(false);
    }

    private void SetPositionWithOffset(Vector2 screenPos)
    {
        Vector2 offset = new Vector2(75, 75);
        panelRectTr.pivot = Vector2.zero;

        Vector2 apos = (screenPos / canvas.scaleFactor) + offset;
        float ypos = apos.y;
        ypos = Mathf.Clamp(ypos, 0, Screen.height / canvas.scaleFactor - panelRectTr.sizeDelta.y);
        apos.y = ypos;
        panelRectTr.anchoredPosition = apos;
    }
}
