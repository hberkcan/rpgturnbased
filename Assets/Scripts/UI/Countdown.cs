using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Countdown : MonoBehaviour
{
    private Action onCountdownEnded;
    private float totalTime;

    private IEnumerator Timer()
    {
        TextMeshProUGUI timeText = GetComponent<TextMeshProUGUI>();

        float initialScaleFactor = 0.1f;
        float targetScaleFactor = 2f;
        int countdownFrom = 3;
        float half = 0.5f;
        float interval = totalTime / countdownFrom;

        transform.localScale = initialScaleFactor * Vector3.one;

        while (countdownFrom > 0)
        {
            transform.DOScale(targetScaleFactor, interval);
            timeText.text = countdownFrom.ToString();
            yield return new WaitForSeconds(interval);
            countdownFrom--;
            transform.localScale = initialScaleFactor * Vector3.one;
        }

        timeText.text = "Fight!";

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOScale(targetScaleFactor, interval * half));
        mySequence.Append(timeText.DOFade(0, interval));
        mySequence.AppendCallback(() => onCountdownEnded?.Invoke());
    }

    public void StartCountdown(float totalTime, Action onCountdownEnded = null)
    {
        this.totalTime = totalTime;
        this.onCountdownEnded = onCountdownEnded;
        StartCoroutine(Timer());
    }
}
