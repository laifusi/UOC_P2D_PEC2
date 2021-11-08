using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsTextUpdater : TextUpdater
{
    protected override void SubscribeToEvent()
    {
        CoinsManager.OnCoinCollected += UpdateText;
    }

    protected override void UnsubscribeFromEvent()
    {
        CoinsManager.OnCoinCollected -= UpdateText;
    }
}
