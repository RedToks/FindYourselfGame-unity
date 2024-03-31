using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player))
        {
            if (!PuzzleTile.allTilesActivated)
            {
                ResetAllTiles();
            }
        }
    }
    private void ResetAllTiles()
    {
        foreach (PuzzleTile tile in PuzzleTile.allTiles)
        {
            tile.ResetTile();
        }
    }
}
