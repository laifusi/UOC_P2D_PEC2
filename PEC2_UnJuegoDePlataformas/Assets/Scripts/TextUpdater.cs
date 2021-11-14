using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public abstract class TextUpdater : MonoBehaviour
{
    private Text text; // Text component

    /// <summary>
    /// Start method to get the Text component and call the SubscribeToEvent method
    /// </summary>
    protected void Start()
    {
        text = GetComponent<Text>();
        SubscribeToEvent();
    }

    protected abstract void SubscribeToEvent(); // abstract method to define which Action each TextUpdater should subscribe to

    /// <summary>
    /// UpdateText method called when the Actions are invoked, to update the text with a number
    /// </summary>
    /// <param name="number"></param>
    protected void UpdateText(int number)
    {
        text.text = number.ToString();
    }

    protected abstract void UnsubscribeFromEvent(); // abstract method to define which Action each TextUpdater should unsubscribe from

    /// <summary>
    /// OnDisable method to call the UnsubscribeFromEvent method
    /// </summary>
    protected void OnDisable()
    {
        UnsubscribeFromEvent();
    }
}
