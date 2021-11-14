using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // static instance of the MusicManager

    /// <summary>
    /// Start method where we implement the singleton pattern
    /// We check if there's an instance of the MusicManager
    /// If there is one, we destroy this one
    /// If there isn't, we set this as the instance and we make it not get destroyed when we load a different scene
    /// </summary>
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
