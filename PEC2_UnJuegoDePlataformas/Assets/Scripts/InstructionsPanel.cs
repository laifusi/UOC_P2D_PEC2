using System.Collections;
using UnityEngine;

public class InstructionsPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup; // CanvasGroup for the instructions

    /// <summary>
    /// Start method where we get the canvas group and we start the fading coroutine
    /// </summary>
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeInstructionsOut());
    }

    /// <summary>
    /// Coroutine to fade out the instructions after 2 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeInstructionsOut()
    {
        yield return new WaitForSeconds(2f);
        float alpha = 1;
        while(alpha >= 0)
        {
            yield return new WaitForSeconds(0f);
            alpha -= Time.deltaTime;
            canvasGroup.alpha = alpha;
        }
    }
}
