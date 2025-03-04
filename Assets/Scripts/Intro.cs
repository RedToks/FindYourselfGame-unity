using UnityEngine;
using System.Collections;
using TMPro;

public class Intro : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] introTexts;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float delayBetweenTexts = 3f;
    [SerializeField] private float delayBetweenLetters = 0.1f; 

    private float timer = 0f;


    private void Start()
    {
        StartCoroutine(FadeInTexts());
    }

    IEnumerator FadeInTexts()
    {
        yield return new WaitForSeconds(delayBetweenTexts); 

        foreach (TextMeshProUGUI introText in introTexts)
        {
            Color textColor = introText.color;
            textColor.a = 0f;
            introText.color = textColor;
            timer = 0f;

            for (int i = 0; i < introText.text.Length; i++)
            {
                timer += Time.deltaTime;

                float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

                textColor.a = alpha;
                introText.textInfo.characterInfo[i].color = textColor;

                yield return new WaitForSeconds(delayBetweenLetters);
            }

            yield return new WaitForSeconds(delayBetweenTexts);
        }

        

    }
}
