using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTextUpdater : TextUpdater
{
    protected override void SubscribeToEvent()
    {
        PointsManager.OnPointsChanged += UpdateText;
    }

    protected override void UnsubscribeFromEvent()
    {
        PointsManager.OnPointsChanged -= UpdateText;
    }
}
