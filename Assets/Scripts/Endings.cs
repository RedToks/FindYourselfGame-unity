using UnityEngine;

public class EndingsManager : MonoBehaviour
{
    public GameObject MotherEnding;
    public GameObject FatherEnding;
    public GameObject GoodForAllEnding;
    public GameObject BadForAllEnding;

    private MemoryPuzzle memoryPuzzle;
    private PuzzleTile puzzleTile;

    private void Start()
    {
        memoryPuzzle = FindObjectOfType<MemoryPuzzle>();
        puzzleTile = FindObjectOfType<PuzzleTile>();
    }

    private void Update()
    {
        DisplayEnding();
    }

    private void DisplayEnding()
    {
        bool memoryPuzzleCompleted = memoryPuzzle != null && memoryPuzzle.gameWon;
        bool puzzleTileCompleted = puzzleTile != null && PuzzleTile.allTilesActivated;

        if (memoryPuzzleCompleted && puzzleTileCompleted)
        {
            GoodForAllEnding.SetActive(true);
            MotherEnding.SetActive(false);
            FatherEnding.SetActive(false);
            BadForAllEnding.SetActive(false);
        }
        else if (memoryPuzzleCompleted)
        {
            MotherEnding.SetActive(true);
            FatherEnding.SetActive(false);
            BadForAllEnding.SetActive(false);
            GoodForAllEnding.SetActive(false);
        }
        else if (puzzleTileCompleted)
        {
            MotherEnding.SetActive(false);
            FatherEnding.SetActive(true);
            BadForAllEnding.SetActive(false);
            GoodForAllEnding.SetActive(false);
        }
        else
        {
            MotherEnding.SetActive(false);
            FatherEnding.SetActive(false);
            BadForAllEnding.SetActive(true);
            GoodForAllEnding.SetActive(false);
        }
    }
}
