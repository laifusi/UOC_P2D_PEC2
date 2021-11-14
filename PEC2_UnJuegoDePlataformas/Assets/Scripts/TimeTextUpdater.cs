public class TimeTextUpdater : TextUpdater
{
    /// <summary>
    /// Method inherited from TextUpdater to subscribe to the TimeManager's Action
    /// </summary>
    protected override void SubscribeToEvent()
    {
        TimeManager.OnTimeChanged += UpdateText;
    }

    /// <summary>
    /// Method inherited from TextUpdater to unsubscribe from the TimeManager's Action
    /// </summary>
    protected override void UnsubscribeFromEvent()
    {
        TimeManager.OnTimeChanged -= UpdateText;
    }
}
