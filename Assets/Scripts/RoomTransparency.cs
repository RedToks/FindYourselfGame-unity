using System;
using System.Collections;
using UnityEngine;

public class RoomTransparency : MonoBehaviour
{
    [SerializeField] private SpriteRenderer roomMask;
    [SerializeField] private float transitionSpeed = 1f;
    [SerializeField] private float targetAlpha = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            StartCoroutine(ChangeTransparency());
        }
    }

    private IEnumerator ChangeTransparency()
    {
        while (Mathf.Abs(roomMask.color.a - targetAlpha) > 0.01f)
        {
            float newAlpha = Mathf.MoveTowards(roomMask.color.a, targetAlpha, Time.deltaTime * transitionSpeed);
            roomMask.color = new Color(roomMask.color.r, roomMask.color.g, roomMask.color.b, newAlpha);
            yield return null;
        }
    }
}
