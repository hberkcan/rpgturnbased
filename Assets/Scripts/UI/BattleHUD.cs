using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Countdown countdown;
    [SerializeField] private GameObject wonBanner;
    [SerializeField] private GameObject lostBanner;

    public void StartCountdown(float totalTime, Action onCountdownEnded = null)
    {
        var timer = Instantiate(countdown, transform);
        timer.StartCountdown(totalTime, onCountdownEnded);
    }

    public void BattleWon()
    {
        wonBanner.SetActive(true);
    }

    public void BattleLost()
    {
        lostBanner.SetActive(true);
    }
}
