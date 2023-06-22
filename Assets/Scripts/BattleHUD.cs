using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Countdown countdown;

    public void StartCountdown(float totalTime, Action onCountdownEnded = null)
    {
        var timer = Instantiate(countdown, transform);
        timer.StartCountdown(totalTime, onCountdownEnded);
    }
}
