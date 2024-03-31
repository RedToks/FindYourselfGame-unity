using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public static List<PuzzleTile> allTiles = new List<PuzzleTile>();
    public static bool allTilesActivated = false;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private bool activated = false;
    [SerializeField] private Vector2 neighborColliderSize = new Vector2(8f, 6f);

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        targetObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        allTiles.Add(this); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            ToggleActivation();
        }
    }

    private void ToggleActivation()
    {
        if (!allTilesActivated)
        {
            activated = !activated;
            spriteRenderer.color = activated ? Color.green : Color.white;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, neighborColliderSize, 0f);

            foreach (Collider2D collider in colliders)
            {
                PuzzleTile neighborTile = collider.GetComponent<PuzzleTile>();

                if (neighborTile != this && neighborTile != null)
                {
                    neighborTile.activated = !neighborTile.activated;
                    neighborTile.spriteRenderer.color = neighborTile.activated ? Color.green : Color.white;
                }
            }

            CheckAllTilesActivated();
        }
    }

    private void CheckAllTilesActivated()
    {
        bool allActivated = true;

        foreach (PuzzleTile tile in allTiles)
        {
            if (!tile.activated)
            {
                allActivated = false;
                break;
            }
        }

        if (allActivated)
        {
            GameWon();
        }
    }

    public void ResetTile()
    {
        activated = false;
        spriteRenderer.color = Color.white;
    }

    private void GameWon()
    {
        allTilesActivated = true;
        targetObject.SetActive(true);
        Debug.Log("All tiles activated!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = activated ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(neighborColliderSize.x, neighborColliderSize.y, 0f));
    }
}
