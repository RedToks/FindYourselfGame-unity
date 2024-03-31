using UnityEngine;

public class StartMemoryPuzzle : MonoBehaviour
{
    private MemoryPuzzle memoryPuzzle;

    private void Start()
    {
        memoryPuzzle = FindObjectOfType<MemoryPuzzle>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            memoryPuzzle.StartPuzzle();
        }
    }
}
