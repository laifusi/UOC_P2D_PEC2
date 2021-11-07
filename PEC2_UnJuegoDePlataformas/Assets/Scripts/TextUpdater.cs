using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public abstract class TextUpdater : MonoBehaviour
{
    private Text text;

    protected void Start()
    {
        text = GetComponent<Text>();
        SubscribeToEvent();
    }

    protected abstract void SubscribeToEvent();

    protected void UpdateText(int number)
    {
        text.text = number.ToString();
    }

    protected abstract void UnsubscribeFromEvent();

    protected void OnDisable()
    {
        UnsubscribeFromEvent();
    }
}
