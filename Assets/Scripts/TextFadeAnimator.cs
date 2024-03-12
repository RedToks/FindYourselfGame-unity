using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFadeAnimator : MonoBehaviour
{
    private TextMeshProUGUI _instructionText;
    [SerializeField] private float _fadeDuration = 1f;

    private void Start()
    {
        _instructionText = GetComponent<TextMeshProUGUI>();
        StartFadeAnimation();
    }

    private void StartFadeAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_instructionText.DOFade(0.3f, _fadeDuration));
        sequence.Append(_instructionText.DOFade(1f, _fadeDuration));   
        sequence.SetLoops(-1);
    }
}
