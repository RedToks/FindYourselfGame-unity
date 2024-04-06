using UnityEngine;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private Canvas confirmationCanvas;
    private KeyCode yesButton = KeyCode.Y; 
    private KeyCode noButton = KeyCode.N; 
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private BoxCollider2D exitCollider;

    private EndingsManager endingsManager;
    private PlayerMovement playerMovement;

    private bool confirmationRequested = false;

    [SerializeField] private float teleportDistance = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement player) && !confirmationRequested)
        {
            confirmationRequested = true;
            Time.timeScale = 0;

            confirmationCanvas.gameObject.SetActive(true);
        }
    }
    private void Start()
    {
        exitCollider.enabled = false;
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
                Teleport(true);
                gameObject.SetActive(false);
                exitCollider.enabled = true;
            }
            else if (Input.GetKeyDown(noButton))
            {
                Teleport(false);
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

    private void Teleport(bool toRight)
    {
        Vector3 newPosition = playerMovement.transform.position;
        float directionMultiplier = toRight ? 1f : -1f; 
        newPosition.x += directionMultiplier * teleportDistance; 
        teleportTarget.position = newPosition;
    }
}
