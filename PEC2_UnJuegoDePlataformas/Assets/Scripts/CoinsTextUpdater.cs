public class CoinsTextUpdater : TextUpdater
{
    /// <summary>
    /// Method inherited from TextUpdater to subscribe to the CoinManager's Action
    /// </summary>
    protected override void SubscribeToEvent()
    {
        CoinsManager.OnCoinCollected += UpdateText;
    }

    /// <summary>
    /// Method inherited from TextUpdater to unsubscribe from the CoinManager's Action
    /// </summary>
    protected override void UnsubscribeFromEvent()
    {
        CoinsManager.OnCoinCollected -= UpdateText;
    }
}
