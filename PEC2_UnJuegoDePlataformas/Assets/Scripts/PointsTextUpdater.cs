public class PointsTextUpdater : TextUpdater
{
    /// <summary>
    /// Method inherited from TextUpdater to subscribe to the PointsManager's Action
    /// </summary>
    protected override void SubscribeToEvent()
    {
        PointsManager.OnPointsChanged += UpdateText;
    }

    /// <summary>
    /// Method inherited from TextUpdater to unsubscribe from the PointsManager's Action
    /// </summary>
    protected override void UnsubscribeFromEvent()
    {
        PointsManager.OnPointsChanged -= UpdateText;
    }
}
