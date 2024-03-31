using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> tiles; 
    [SerializeField] private List<int> sequence = new List<int>(); 
    [SerializeField] private List<int> playerInput = new List<int>();
    [SerializeField] private GameObject targetObject;
    public bool waitingForInput { get; private set; } = false;
    public bool gameWon { get; private set; } = false;

    private void Start()
    {
        targetObject.SetActive(false);
    }
    public void StartPuzzle()
    {
        if (gameWon)
        {
            Debug.Log("Game has already ended. Cannot start a new game.");
            return;
        }

        GenerateSequence();

        StartCoroutine(PlaySequence());
    }

    public void AddTileToInputList(GameObject tile)
    {
        if (!waitingForInput)
        {
            return;
        }

        int tileIndex = tiles.IndexOf(tile);
        if (tileIndex != -1)
        {
            playerInput.Add(tileIndex);
        }
        CheckInput();
    }

    private void GenerateSequence()
    {
        sequence.Clear();
        for (int i = 0; i < tiles.Count; i++)
        {
            sequence.Add(i);
        }
        sequence.Shuffle();
    }

    private IEnumerator PlaySequence()
    {
        waitingForInput = false;
        foreach (int index in sequence)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            ActivateTile(tiles[index], randomColor);
            yield return new WaitForSeconds(1f);
            DeactivateTile(tiles[index]);
            yield return new WaitForSeconds(0.5f);
        }
        waitingForInput = true;
        playerInput.Clear();
    }

    private void ActivateTile(GameObject tile, Color color)
    {
        tile.GetComponent<Renderer>().material.color = color;
    }

    private void DeactivateTile(GameObject tile)
    {
        tile.GetComponent<Renderer>().material.color = Color.white;
    }

    public void CheckInput()
    {
        if (playerInput.Count == sequence.Count)
        {
            bool correct = true;
            for (int i = 0; i < sequence.Count; i++)
            {
                if (sequence[i] != playerInput[i])
                {
                    correct = false;
                    break;
                }
            }
            if (correct)
            {
                GameWon();
            }
            else
            {
                GameLose();
            }
            playerInput.Clear();
        }
    }

    private void HighlightTiles(bool correct)
    {
        Color highlightColor = correct ? Color.green : Color.red;
        foreach (var tile in tiles)
        {
            ActivateTile(tile, highlightColor);
        }

        if (!correct)
        {
            StartCoroutine(ResetTileColors());
        }
    }

    private IEnumerator ResetTileColors()
    {
        yield return new WaitForSeconds(3f);

        foreach (var tile in tiles)
        {
            DeactivateTile(tile);
        }

        yield return new WaitForSeconds(3f);
        StartPuzzle();
    }

    private void GameWon()
    {
        Debug.Log("Correct sequence!");
        HighlightTiles(true);
        waitingForInput = false;
        gameWon = true;
        targetObject.SetActive(true);
    }

    private void GameLose()
    {
        Debug.Log("Incorrect sequence! You lose!");
        HighlightTiles(false);
        waitingForInput = false;
    }
}
