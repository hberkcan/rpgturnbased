using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyMessagingSystem;

public class BattleEndMenu : Menu<BattleEndMenu>, IMessagingSubscriber<BattleWonEvent>, IMessagingSubscriber<BattleLostEvent>
{
    [SerializeField] private GameObject battleWonBanner;
    [SerializeField] private GameObject battleLostBanner;

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Subscribe<BattleLostEvent>(this);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<BattleWonEvent>(this);
        MessagingSystem.Instance.Unsubscribe<BattleLostEvent>(this);
    }

    public void OnReceiveMessage(BattleWonEvent message)
    {
        battleWonBanner.SetActive(true);
    }

    public void OnReceiveMessage(BattleLostEvent message)
    {
        battleLostBanner.SetActive(false);
    }
}
