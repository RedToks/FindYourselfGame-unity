using System.Collections;
using TMPro;
using UnityEngine;

public class TextDisplayTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] textsToDisplay;
    [SerializeField] private float fadeInSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            StartCoroutine(DisplayTexts());
        }
    }

    private IEnumerator DisplayTexts()
    {
        foreach (TextMeshProUGUI text in textsToDisplay)
        {
            while (text.color.a < 1f)
            {
                float newAlpha = text.color.a + Time.deltaTime * fadeInSpeed;
                text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
                yield return null;
            }
        }
    }
}
