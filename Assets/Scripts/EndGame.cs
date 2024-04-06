using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private float secondsToDisplay = 3;
    [SerializeField] private float secondsToExitMainMenu = 5;
    [SerializeField] private AudioSource backgroundAudioSource;

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f; 
    }

    public void EndGameSequence()
    {
        StartCoroutine(FadeOutAndLoadMainMenu());
        Debug.Log("Coroutine Started");
    }

    private IEnumerator FadeOutAndLoadMainMenu()
    {
        yield return new WaitForSeconds(secondsToDisplay);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            float normalizedTime = timer / fadeDuration;
            float smoothedVolume = Mathf.SmoothStep(0.5f, 0f, normalizedTime);
            backgroundAudioSource.volume = smoothedVolume;

            timer += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(secondsToExitMainMenu);

        SceneManager.LoadScene(mainMenuSceneName);
    }
}
