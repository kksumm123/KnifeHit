using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotateZ : MonoBehaviour
{
    public float minAngle = 1000f;
    public float maxAngle = 1200f;
    public float minDuration = 3;
    public float maxDuration = 5;
    public Ease ease = Ease.InOutCubic;

    bool isComplete;
    float startAngle;
    float angle;
    float duration;
    IEnumerator Start()
    {
        while (true)
        {
            isComplete = false;
            startAngle = transform.eulerAngles.z;
            angle = startAngle + Random.Range(minAngle, maxAngle);
            duration = Random.Range(minDuration, maxDuration);
            DOTween.To(() => startAngle
                , x => transform.rotation = Quaternion.Euler(0.0f, 0.0f, x), angle, duration
                ).SetEase(ease)
                 .OnComplete(() => isComplete = true);

            while (isComplete == false)
                yield return null;
        }
    }

}
