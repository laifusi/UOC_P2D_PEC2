using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    private Text text;

    [SerializeField] TypeOfScores typeOfHighscore;

    public enum TypeOfScores
    {
        Time, Points, Coins
    }

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = PlayerPrefs.GetInt(typeOfHighscore.ToString()).ToString();
    }
}
