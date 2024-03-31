using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private Canvas confirmationCanvas;
    private KeyCode yesButton = KeyCode.Y; 
    private KeyCode noButton = KeyCode.N; 
    [SerializeField] private Transform teleportTarget; 

    private EndingsManager endingsManager;
    private PlayerMovement playerMovement;

    private bool confirmationRequested = false;

    [SerializeField] private float teleportDistance = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !confirmationRequested)
        {
            confirmationRequested = true;
            Time.timeScale = 0;

            confirmationCanvas.gameObject.SetActive(true);
        }
    }
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        endingsManager = FindObjectOfType<EndingsManager>();
    }
    private void Update()
    {
        if (confirmationRequested)
        {
            if (Input.GetKeyDown(yesButton))
            {
                ResetConfirmation();
                gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(noButton))
            {
                Teleport();
                ResetConfirmation();
            }
        }
    }

    private void ResetConfirmation()
    {
        confirmationRequested = false;
        Time.timeScale = 1; 
        confirmationCanvas.gameObject.SetActive(false);
    }

    private void Teleport()
    {
        Vector3 newPosition = playerMovement.transform.position;
        newPosition.x -= teleportDistance; 
        teleportTarget.position = newPosition;
    }
}
