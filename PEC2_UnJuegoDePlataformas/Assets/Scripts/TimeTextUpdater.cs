using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTextUpdater : TextUpdater
{
    protected override void SubscribeToEvent()
    {
        TimeManager.OnTimeChanged += UpdateText;
    }
    protected override void UnsubscribeFromEvent()
    {
        TimeManager.OnTimeChanged -= UpdateText;
    }
}
