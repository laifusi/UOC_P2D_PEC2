using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action<int> OnTimeChanged;

    private float timePassed;

    public float TotalTimePassed => timePassed;

    private void Start()
    {
        ResetTime();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        OnTimeChanged?.Invoke((int)timePassed);
    }

    public void ResetTime()
    {
        timePassed = 0;
    }
}
