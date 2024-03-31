using System.Collections;
using UnityEngine;

public class MemoryPuzzleTrigger : MonoBehaviour
{
    [SerializeField] private MemoryPuzzle memoryPuzzle;
    [SerializeField] private Color tileHighlightColor;
    [SerializeField] private float timeToReturnColor = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            if (memoryPuzzle.waitingForInput)
            {
                memoryPuzzle.AddTileToInputList(gameObject);

                StartCoroutine(HighlightTile());
            }
        }
    }

    IEnumerator HighlightTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = tileHighlightColor;

        yield return new WaitForSeconds(timeToReturnColor);

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
