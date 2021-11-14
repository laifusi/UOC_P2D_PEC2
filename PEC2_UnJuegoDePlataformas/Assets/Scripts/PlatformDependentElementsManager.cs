using UnityEngine;

public class PlatformDependentElementsManager : MonoBehaviour
{
    [SerializeField] GameObject[] androidElements; // array of elements that should only show in android version
    [SerializeField] GameObject[] computerElements; // array of eleemnts that should only show in computer versions

    private bool shouldShowInAndroid; // whether the elements should show or not

    /// <summary>
    /// Start method where we activate or deactivate the elements that should only show in android or out of it
    /// </summary>
    private void Start()
    {
        #if UNITY_ANDROID
                shouldShowInAndroid = true;
        #endif

        for(int i = 0; i < androidElements.Length; i++)
        {
            androidElements[i].SetActive(shouldShowInAndroid);
        }

        for(int j = 0; j < computerElements.Length; j++)
        {
            computerElements[j].SetActive(!shouldShowInAndroid);
        }
    }
}
