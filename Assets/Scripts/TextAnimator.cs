using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _texts;
    [SerializeField] private TextMeshProUGUI _instructionText;
    [SerializeField] private float _betweenHalf = 0.05f;
    [SerializeField] private float _betweenChar = 0.03f;
    [SerializeField] private float _smoothTime = 0.1f;

    private List<float> _leftAlphas;
    private List<float> _rightAlphas;
    private int _currentTextIndex = 0;

    private bool _isAnimating = false;
    private bool _allTextsAnimated = false;

    public delegate void CanvasDestroyedAction();
    public static event CanvasDestroyedAction OnCanvasDestroyed;


    private void Awake()
    {
        _instructionText.gameObject.SetActive(false);
        InitializeAlphasList();
    }

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    private void OnDisable()
    {
        _leftAlphas.Clear();
        _rightAlphas.Clear();
    }

    private void Update()
    {
        if (_isAnimating)
            SwitchColor();

        if (_allTextsAnimated && Input.anyKeyDown)
        {
            StartCoroutine(FadeOutCanvas());
        }
    }

    private IEnumerator AnimateText()
    {
        yield return new WaitForSeconds(3f);

        while (_currentTextIndex < _texts.Length)
        {
            InitializeAlphasList();

            _isAnimating = true;
            yield return StartCoroutine(Smooth(0));

            yield return new WaitForSeconds(_betweenChar);

            _currentTextIndex++;
        }

        _isAnimating = false;
        _allTextsAnimated = true;

        if (_allTextsAnimated)
        {
            _instructionText.gameObject.SetActive(true);
        }
    }

    private void InitializeAlphasList()
    {
        if (_currentTextIndex < _texts.Length)
        {
            string text = _texts[_currentTextIndex].text;
            _leftAlphas = new List<float>(text.Length);
            _rightAlphas = new List<float>(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                _leftAlphas.Add(0f);
                _rightAlphas.Add(0f);
            }
        }
    }

    private void SwitchColor()
    {
        if (_leftAlphas != null && _rightAlphas != null)
        {
            for (int i = 0; i < _leftAlphas.Count; i++)
            {
                if (_texts[_currentTextIndex].textInfo.characterInfo[i].character != '\n' &&
                    _texts[_currentTextIndex].textInfo.characterInfo[i].character != ' ')
                {
                    int meshIndex = _texts[_currentTextIndex].textInfo.characterInfo[i].materialReferenceIndex;
                    int vertexIndex = _texts[_currentTextIndex].textInfo.characterInfo[i].vertexIndex;

                    Color32[] vertexColors = _texts[_currentTextIndex].textInfo.meshInfo[meshIndex].colors32;

                    vertexColors[vertexIndex + 0].a = (byte)_leftAlphas[i];
                    vertexColors[vertexIndex + 1].a = (byte)_leftAlphas[i];
                    vertexColors[vertexIndex + 2].a = (byte)_rightAlphas[i];
                    vertexColors[vertexIndex + 3].a = (byte)_rightAlphas[i];
                }
            }

            _texts[_currentTextIndex].UpdateVertexData();
        }
    }

    private IEnumerator Smooth(int i)
    {
        if (_leftAlphas != null && _rightAlphas != null)
        {
            while (i < _leftAlphas.Count)
            {
                DOTween.To(
                    () => _leftAlphas[i],
                    x => _leftAlphas[i] = x,
                    255,
                    _smoothTime)
                    .SetEase(Ease.Linear)
                    .SetId(1);

                yield return new WaitForSeconds(_betweenHalf);

                DOTween.To(
                    () => _rightAlphas[i],
                    x => _rightAlphas[i] = x,
                    255,
                    _smoothTime)
                    .SetEase(Ease.Linear)
                    .SetId(1);

                yield return new WaitForSeconds(_betweenChar);

                i++;
            }
        }
    }

    private IEnumerator FadeOutCanvas()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        float duration = 1f;
        float targetAlpha = 0f;

        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, targetAlpha, duration)
            .SetEase(Ease.Linear);

        yield return new WaitForSeconds(duration);

        if (OnCanvasDestroyed != null)
            OnCanvasDestroyed();

        Destroy(gameObject);
    }
}
