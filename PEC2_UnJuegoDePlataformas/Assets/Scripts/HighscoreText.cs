using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    private Text text; // Text component

    [SerializeField] TypeOfScores typeOfHighscore; // TypeOfScore for this highscore

    public enum TypeOfScores // enum that defines the different types of scores
    {
        Time, Points, Coins
    }

    /// <summary>
    /// Start method where we get the Text component and we set the highscore
    /// </summary>
    private void Start()
    {
        text = GetComponent<Text>();
        text.text = PlayerPrefs.GetInt(typeOfHighscore.ToString()).ToString();
    }
}
