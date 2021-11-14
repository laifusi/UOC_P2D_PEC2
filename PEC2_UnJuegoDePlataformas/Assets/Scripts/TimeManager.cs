using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action<int> OnTimeChanged; // Action<int> for when the time changes

    private float timePassed; // amount of time that passed

    public float TotalTimePassed => timePassed; // public amount of time passed

    /// <summary>
    /// Start method to reset the time
    /// </summary>
    private void Start()
    {
        ResetTime();
    }

    /// <summary>
    /// Update method where we update the time passed and invoke the OnTimeChanged Action
    /// </summary>
    private void Update()
    {
        timePassed += Time.deltaTime;

        OnTimeChanged?.Invoke((int)timePassed);
    }

    /// <summary>
    /// Method to set the time to 0
    /// Public in case we need to call it from another class
    /// </summary>
    public void ResetTime()
    {
        timePassed = 0;
    }
}
